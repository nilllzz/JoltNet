using System.Runtime.Serialization;

// Disable Code Analysis for warning CS0649: Field is never assigned to, and will always have its default value.
#pragma warning disable 0649

namespace JoltNet.DataModel
{
    [DataContract(Namespace = "")]
    internal class TrophyModel : DataModel<TrophyModel>
    {
        [DataMember(Name = "id")]
        public string Id;

        [DataMember(Name = "title")]
        public string Title;

        [DataMember(Name = "description")]
        public string Description;

        [DataMember(Name = "difficulty")]
        private string _difficulty;

        public TrophyDifficulty Difficulty
        {
            get { return ConvertStringToEnum<TrophyDifficulty>(_difficulty); }
            set { _difficulty = value.ToString(); }
        }

        [DataMember(Name = "image_url")]
        public string ImageUrl;
        
        [DataMember(Name = "achieved")]
        public string Achieved;
    }
}
