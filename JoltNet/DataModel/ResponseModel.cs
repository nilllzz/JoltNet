using System.Runtime.Serialization;

// Disable Code Analysis for warning CS0649: Field is never assigned to, and will always have its default value.
#pragma warning disable 0649

namespace JoltNet.DataModel
{
    /// <summary>
    /// The data model containing the response data for a request.
    /// </summary>
    [DataContract(Namespace = "")]
    internal sealed class ResponseModel : DataModel<ResponseModel>
    {
        /// <summary>
        /// Used by all API endpoints.
        /// Indicates wether the call was successful or not.
        /// </summary>
        [DataMember(Name = "success")]
        public bool Success;

        /// <summary>
        /// Only filled if the call returned unsuccessfully. This contains the error message.
        /// </summary>
        [DataMember(Name = "message")]
        public string Message;

        /// <summary>
        /// Contains data returned from data storage operations (get and update).
        /// </summary>
        [DataMember(Name = "data")]
        public string Data;

        /// <summary>
        /// The list of keys returned from data storage key get operations.
        /// </summary>
        [DataMember(Name = "keys")]
        public KeyModel[] Keys;

        #region GetTime

        /// <summary>
        /// Returned from get-time. The Game Jolt server's UNIX time stamp.
        /// </summary>
        [DataMember(Name = "timestamp")]
        public int Timestamp;

        /// <summary>
        /// Returned from get-time. The Game Jolt server's timezone.
        /// </summary>
        [DataMember(Name = "timezone")]
        public string Timezone;

        /// <summary>
        /// Returned from get-time. The Game Jolt server's current year.
        /// </summary>
        [DataMember(Name = "year")]
        public int Year;

        /// <summary>
        /// Returned from get-time. The Game Jolt server's current month.
        /// </summary>
        [DataMember(Name = "month")]
        public int Month;

        /// <summary>
        /// Returned from get-time. The Game Jolt server's current day.
        /// </summary>
        [DataMember(Name = "day")]
        public int Day;

        /// <summary>
        /// Returned from get-time. The Game Jolt server's current hour.
        /// </summary>
        [DataMember(Name = "hour")]
        public int Hour;

        /// <summary>
        /// Returned from get-time. The Game Jolt server's current minute.
        /// </summary>
        [DataMember(Name = "minute")]
        public int Minute;

        /// <summary>
        /// Returned from get-time. The Game Jolt server's current second.
        /// </summary>
        [DataMember(Name = "seconds")]
        public int Second;

        #endregion

        #region Scoreboards

        /// <summary>
        /// The rank of a score on a scoreboard returned by get-rank.
        /// </summary>
        [DataMember(Name = "rank")]
        public int Rank;

        /// <summary>
        /// The score tables for a game returned by a scoreboard tables operation.
        /// </summary>
        [DataMember(Name = "tables")]
        public ScoreTableModel[] Tables;

        /// <summary>
        /// A list of scores returned from a scoreboard fetch scores operation.
        /// </summary>
        [DataMember(Name = "scores")]
        public ScoreModel[] Scores;

        #endregion

        /// <summary>
        /// The list of trophies returned from a trophies fetch operation.
        /// </summary>
        [DataMember(Name = "trophies")]
        public TrophyModel[] Trophies;

        /// <summary>
        /// The list of users returned by a user fetch call.
        /// </summary>
        [DataMember(Name = "users")]
        public UserModel[] Users;

        #region Friends

        /// <summary>
        /// The friend list for vairous friend calls.
        /// </summary>
        [DataMember(Name = "friends")]
        public FriendModel[] Friends;

        /// <summary>
        /// The friend request list for various friend calls.
        /// </summary>
        [DataMember(Name = "requests")]
        public FriendRequestModel[] FriendRequests;

        #endregion
    }
}
