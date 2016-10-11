using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using JoltNet.DataModel;

namespace JoltNet
{
    /// <summary>
    /// Client to talk to the Game Jolt api.
    /// </summary>
    public partial class GameJoltApiClient
    {
        private const string HOST = "api.gamejolt.com/api/game";
        private const string VERSION = "v1_1";
        private const string FORMAT_JSON = "json";

        private const string FORMAT_REQUEST_URL = "http://{0}/{1}/batch/?game_id={2}&format={3}";

        private readonly string _gameKey;
        
        internal string GameId { get; }
        
        /// <summary>
        /// initializes the api client with the game's id and private key.
        /// These values can be found on the Game Jolt Dashboard page of a game.
        /// </summary>
        /// <param name="gameId">The game's public id.</param>
        /// <param name="gameKey">The game's private key used to compose valid requests.</param>
        public GameJoltApiClient(string gameId, string gameKey)
        {
            GameId = gameId;
            _gameKey = gameKey;
        }
        
        /// <summary>
        /// Executes a single request. The return value indicates the status of the request to the api.
        /// The response to the request passed in can be found in the request object.
        /// </summary>
        public async Task<GameJoltResponse> ExecuteRequestAsync(IGameJoltRequest request)
        {
            return await ExecuteRequestsAsync(new[] { request });
        }

        /// <summary>
        /// Executes multiple requests. Returns the status of the request to the api.
        /// The responses to the individual requests can be found in the respective request objects.
        /// </summary>
        /// <param name="feature">Special feature for the client to execute for the passed in requests.</param>
        public async Task<GameJoltResponse> ExecuteRequestsAsync(IEnumerable<IGameJoltRequest> requests, GameJoltApiRequestFeature feature = GameJoltApiRequestFeature.None)
        {
            var startTime = DateTime.Now;

            try
            {
                var data = await GetRequestData(requests, feature);
                var model = RootResponseModel.FromString(data);
                var endTime = DateTime.Now;

                if (model.BatchResponse.Success)
                {
                    for (int i = 0; i < model.BatchResponse.Responses.Length; i++)
                    {
                        requests.ElementAt(i).SetResponse(model.BatchResponse.Responses[i]);
                        requests.ElementAt(i).SetTimingResult(startTime, endTime);
                    }
                }
                else
                {
                    int startIndex = 0;
                    if (model.BatchResponse != null && model.BatchResponse.Responses != null)
                    {
                        startIndex = model.BatchResponse.Responses.Length;
                        for (int i = 0; i < model.BatchResponse.Responses.Length; i++)
                        {
                            requests.ElementAt(i).SetResponse(model.BatchResponse.Responses[i]);
                            requests.ElementAt(i).SetTimingResult(startTime, endTime);
                        }

                    }

                    for (int i = startIndex; i < requests.Count(); i++)
                    {
                        requests.ElementAt(i).SetResponse(GetSubRequestErrorModel(feature == GameJoltApiRequestFeature.BreakOnError));
                        requests.ElementAt(i).SetTimingResult(startTime, endTime);
                    }
                }

                return new GameJoltResponse(model);
            }
            catch (Exception ex)
            {
                var endTime = DateTime.Now;

                for (int i = 0; i < requests.Count(); i++)
                {
                    requests.ElementAt(i).SetResponse(GetSubRequestErrorModel(false));
                    requests.ElementAt(i).SetTimingResult(startTime, endTime);
                }

                return new GameJoltResponse(ex.Message);
            }
        }

        private static ResponseModel GetSubRequestErrorModel(bool breakOnError)
            => new ResponseModel { Message = "The parent request did not execute successfully." + 
                (breakOnError ? " A previous request failure caused the api to stop executing requests because breakOnError was active." : ""),
                Success = false };

        private async Task<string> GetRequestData(IEnumerable<IGameJoltRequest> requests, GameJoltApiRequestFeature feature)
        {
            if (requests == null || requests.Count() == 0)
                return string.Empty;
            
            // build url from parameters:
            string url = string.Format(FORMAT_REQUEST_URL, HOST, VERSION, GameId, FORMAT_JSON);
            if (feature == GameJoltApiRequestFeature.ParallelProcessing)
                url += "&parallel=true";
            if (feature == GameJoltApiRequestFeature.BreakOnError)
                url += "&break_on_error=true";

            // add signature to url:
            var urlSignature = CreateSignature(url);
            url += "&signature=" + urlSignature;

            // the request will be a POST request, and all actual api requests will be stored in the post data:
            StringBuilder postDataBuilder = new StringBuilder("data=");

            foreach (var gameJoltRequest in requests)
            {
                var requestUrl = gameJoltRequest.ComposeUrl(this);
                var requestUrlSignature = CreateSignature(requestUrl);
                requestUrl += "&signature=" + requestUrlSignature;

                postDataBuilder.Append("&requests[]=" + UrlEncoder.Encode(requestUrl));
            }

            string postData = postDataBuilder.ToString();
            
            var request = ComposeRequest(url, postData);
            using (var sw = new StreamWriter(await request.GetRequestStreamAsync()))
            {
                // write post data to stream:
                await sw.WriteAsync(postData);
                sw.Close();

                // get request response, read from stream:
                var response = await request.GetResponseAsync();
                using (var sr = new StreamReader(response.GetResponseStream()))
                {
                    return await sr.ReadToEndAsync();
                }
            }
        }

        private static HttpWebRequest ComposeRequest(string url, string postData)
        {
            var request = WebRequest.CreateHttp(url);
            request.AllowWriteStreamBuffering = true;
            request.Method = "POST";
            request.ContentLength = postData.Length;
            request.ContentType = "application/x-www-form-urlencoded";
            request.ServicePoint.Expect100Continue = false;
            return request;
        }

        /// <summary>
        /// Creates a signature for an API request url.
        /// </summary>
        private string CreateSignature(string url)
        {
            MD5 md5 = MD5.Create();

            // the string used as hash is the url (without "signature" parameter) and the game's key.
            string hashString = url + _gameKey;

            byte[] bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(hashString));

            StringBuilder byteBuilder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
                byteBuilder.Append(bytes[i].ToString("x2"));

            return byteBuilder.ToString();
        }
    }
}
