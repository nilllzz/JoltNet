using System.Runtime.Serialization;

// Disable Code Analysis for warning CS0649: Field is never assigned to, and will always have its default value.
#pragma warning disable 0649

namespace JoltNet.DataModel
{
    /// <summary>
    /// A model to store a key of the Game Jolt data storage.
    /// </summary>
    [DataContract(Namespace = "")]
    internal class KeyModel : DataModel<KeyModel>
    {
        [DataMember(Name = "key")]
        public string Key;
    }
}
