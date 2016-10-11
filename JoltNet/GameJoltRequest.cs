using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using JoltNet.DataModel;

namespace JoltNet
{
    /// <summary>
    /// A request to the Game Jolt api. Contains the response once executed.
    /// </summary>
    /// <typeparam name="T">The response type.</typeparam>
    public sealed class GameJoltRequest<T> : IGameJoltRequest where T : GameJoltResponse
    {
        private const string FORMAT_URL = "/{0}/?game_id={1}";
        private const string FORMAT_PARAMETER = "&{0}={1}";

        private readonly Dictionary<string, string> _parameters;
        private readonly string _endPoint;
        private T _response = default(T);

        /// <summary>
        /// The time this request got executed.
        /// </summary>
        public DateTime? StartedTime { get; internal set; } = null;
        /// <summary>
        /// The time this request finished execution.
        /// </summary>
        public DateTime? FinishedTime { get; internal set; } = null;

        /// <summary>
        /// The approximate time it took for this request to execute.
        /// </summary>
        public TimeSpan ExecutionTime
        {
            get
            {
                if (FinishedTime == null)
                    throw new InvalidOperationException("This request has not been executed. Use GameJoltApi.ExecuteRequestAsync to retrieve a response to this request.");

                return FinishedTime.Value - StartedTime.Value;
            }
        }

        /// <summary>
        /// The response from the Game Jolt api once this request got executed.
        /// </summary>
        public T Response
        {
            get
            {
                if (_response == null)
                    throw new InvalidOperationException("This request has not been executed. Use GameJoltApi.ExecuteRequestAsync to retrieve a response to this request.");

                return _response;
            }
        }

        internal GameJoltRequest(string endpoint)
            : this(endpoint, new Dictionary<string, string>())
        { }

        internal GameJoltRequest(string endpoint, string key, string value)
            : this(endpoint, new Dictionary<string, string>() { { key, value } })
        { }

        internal GameJoltRequest(string endpoint, Dictionary<string, string> parameters)
        {
            _endPoint = endpoint.Trim('/');
            _parameters = parameters;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void SetTimingResult(DateTime start, DateTime end)
        {
            StartedTime = start;
            FinishedTime = end;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void SetResponse(object model)
        {
            // prevent accidental setting from outside.
            if (model == null || !(model is ResponseModel))
                throw new InvalidOperationException("The response cannot be set from outside.\nUse GameJoltApi.ExecuteRequestAsync to retrieve a response to a request.");

            // access the internal ctor of the T response type via reflection.
            var ctor = typeof(T).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ResponseModel) }, null);
            _response = (T)ctor.Invoke(new[] { model });
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public string ComposeUrl(GameJoltApiClient api)
        {
            string baseUrl = string.Format(FORMAT_URL, _endPoint, UrlEncoder.Encode(api.GameId));

            if (_parameters == null)
                return baseUrl;

            var urlBuilder = new StringBuilder(baseUrl);

            foreach (var parameter in _parameters)
                urlBuilder.Append(string.Format(FORMAT_PARAMETER, parameter.Key, UrlEncoder.Encode(parameter.Value)));

            return urlBuilder.ToString();
        }
    }
}
