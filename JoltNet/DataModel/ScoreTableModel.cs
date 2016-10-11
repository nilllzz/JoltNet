using System.Runtime.Serialization;

// Disable Code Analysis for warning CS0649: Field is never assigned to, and will always have its default value.
#pragma warning disable 0649

namespace JoltNet.DataModel
{
    /// <summary>
    /// A data model describing a Game Jolt score table.
    /// </summary>
    [DataContract(Namespace = "")]
    internal class ScoreTableModel : DataModel<ScoreTableModel>
    {
        /// <summary>
        /// The identifier of the score table that can be used to fetch scores from it.
        /// </summary>
        [DataMember(Order = 0, Name = "id")]
        public string Id;

        [DataMember(Order = 0, Name = "name")]
        public string Name;

        [DataMember(Order = 0, Name = "description")]
        public string Description;

        /// <summary>
        /// If this is the game's primary score table.
        /// </summary>
        [DataMember(Order = 0, Name = "primary")]
        public string Primary;
    }
}
