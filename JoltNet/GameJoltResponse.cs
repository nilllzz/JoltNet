using JoltNet.DataModel;

namespace JoltNet
{
    /// <summary>
    /// A response from the Game Jolt api.
    /// </summary>
    public class GameJoltResponse
    {
        /// <summary>
        /// If the request for this response completed successfully. For some requests, this is also used as the return value.
        /// To check if the request failed due to an error, check for the <see cref="RequestFailed"/> property.
        /// </summary>
        public bool Success { get; }

        /// <summary>
        /// If the request failed due to an error.
        /// </summary>
        public bool RequestFailed => !Success && ErrorMessage != null;

        /// <summary>
        /// Contains the error message to a failed request. <see cref="null"/> if the request was successful.
        /// </summary>
        public string ErrorMessage { get; }

        internal GameJoltResponse(ResponseModel model)
        {
            Success = model.Success;
            ErrorMessage = model.Success ? null : model.Message;
        }

        internal GameJoltResponse(RootResponseModel model)
        {
            Success = model.BatchResponse.Success;
            ErrorMessage = model.BatchResponse.Success ? null : model.BatchResponse.Message;
        }

        internal GameJoltResponse(string errorMessage)
        {
            ErrorMessage = errorMessage;
            Success = false;
        }
    }
}
