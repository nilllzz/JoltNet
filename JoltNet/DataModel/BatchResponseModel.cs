using System.Runtime.Serialization;

// Disable Code Analysis for warning CS0649: Field is never assigned to, and will always have its default value.
#pragma warning disable 0649

namespace JoltNet.DataModel
{
    /// <summary>
    /// The data model that contains all responses for a batch of requests.
    /// </summary>
    [DataContract(Namespace = "")]
    internal sealed class BatchResponseModel : DataModel<BatchResponseModel>
    {
        /// <summary>
        /// Indicates wether the entire query was successful.
        /// </summary>
        [DataMember(Order = 0, Name = "success")]
        public bool Success;

        /// <summary>
        /// Only filled if the call returned unsuccessfully. This contains the error message.
        /// </summary>
        [DataMember(Order = 1, Name = "message")]
        public string Message;

        /// <summary>
        /// The list of responses.
        /// </summary>
        [DataMember(Order = 2, Name = "responses")]
        public ResponseModel[] Responses;
    }
}
