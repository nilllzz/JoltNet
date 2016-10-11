using JoltNet.DataModel;

namespace JoltNet
{
    public static partial class RequestProvider
    {
        public static class Time
        {
            public class TimeResponse : GameJoltResponse
            {
                public int Timestamp { get; }
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

            public static GameJoltRequest<TimeResponse> Get()
                => new GameJoltRequest<TimeResponse>("get-time");
        }
    }
}
