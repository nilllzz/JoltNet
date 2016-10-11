using System.Runtime.Serialization;

// Disable Code Analysis for warning CS0649: Field is never assigned to, and will always have its default value.
#pragma warning disable 0649

namespace JoltNet.DataModel
{
    [DataContract(Namespace = "")]
    internal class FriendRequestModel : DataModel<FriendRequestModel>
    {
        [DataMember(Name = "user_id")]
        public string UserId;

        [DataMember(Name = "requested_on")]
        public string RequestedOn;
    }
}
