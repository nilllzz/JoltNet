using System.Runtime.Serialization;

// Disable Code Analysis for warning CS0649: Field is never assigned to, and will always have its default value.
#pragma warning disable 0649

namespace JoltNet.DataModel
{
    [DataContract(Namespace = "")]
    internal sealed class ScoreModel : DataModel<ScoreModel>
    {
        /// <summary>
        /// The score string returned by a scoreboard fetch operation.
        /// </summary>
        [DataMember(Name = "score")]
        public string Score;

        /// <summary>
        /// The score's sort value returned by a scoreboard fetch operation.
        /// </summary>
        [DataMember(Name = "sort")]
        public int Sort;

        /// <summary>
        /// Any extra data associated with the score returned by a scoreboard fetch operation.
        /// </summary>
        [DataMember(Name = "extra_data")]
        public string ExtraData;

        /// <summary>
        /// Contains the user name.
        /// Used by the scoreboard fetch operation, where it contains the user name of a score (if it is a user score).
        /// </summary>
        [DataMember(Name = "user")]
        public string User;

        /// <summary>
        /// Contains the user id.
        /// Used by the scoreboard fetch operation, where it contains the user id of a score (if it is a user score).
        /// </summary>
        [DataMember(Name = "user_id")]
        public string UserId;

        /// <summary>
        /// Contains a guest's name.
        /// Used by the scoreboard fetch operation, where it contains the guest's name, if the score is not a user score.
        /// </summary>
        [DataMember(Name = "guest")]
        public string Guest;

        /// <summary>
        /// The data a score was stored on the scoreboard.
        /// </summary>
        [DataMember(Name = "stored")]
        public string Stored;
    }
}
