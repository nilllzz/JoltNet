using JoltNet.DataModel;

namespace JoltNet
{
    public static partial class RequestProvider
    {
        /// <summary>
        /// Provides requests to api endpoints to the time and date on the Game Jolt server.
        /// </summary>
        public static class Time
        {
            /// <summary>
            /// A response for a request to get time information of the Game jolt server.
            /// </summary>
            public class TimeResponse : GameJoltResponse
            {
                /// <summary>
                /// The UNIX timestamp.
                /// </summary>
                public int Timestamp { get; }
                /// <summary>
                /// The name of the timezone of the server.
                /// </summary>
                public string Timezone { get; }
                public int Year { get; }
                public int Month { get; }
                public int Day { get; }
                public int Hour { get; }
                public int Minute { get; }
                public int Second { get; }

                internal TimeResponse(ResponseModel responseModel)
                    : base(responseModel)
                {
                    Timestamp = responseModel.Timestamp;
                    Timezone = responseModel.Timezone;
                    Year = responseModel.Year;
                    Month = responseModel.Month;
                    Day = responseModel.Day;
                    Hour = responseModel.Hour;
                    Minute = responseModel.Minute;
                    Second = responseModel.Second;
                }
            }

            /// <summary>
            /// Returns a request to get the time and date information of the Game Jolt server.
            /// </summary>
            public static GameJoltRequest<TimeResponse> Get()
                => new GameJoltRequest<TimeResponse>("time");
        }
    }
}
