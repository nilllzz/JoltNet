using System.Runtime.Serialization;

// Disable Code Analysis for warning CS0649: Field is never assigned to, and will always have its default value.
#pragma warning disable 0649

namespace JoltNet.DataModel
{
    /// <summary>
    /// The data model that contains all responses for a batch of requests.
    /// </summary>
    [DataContract(Namespace = "")]
    internal sealed class RootResponseModel : DataModel<RootResponseModel>
    {
        [DataMember(Name = "response", Order = 0)]
        public BatchResponseModel BatchResponse;
    }
}
