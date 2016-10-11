using System;
using System.ComponentModel;

namespace JoltNet
{
    /// <summary>
    /// A request to be sent to the Game Jolt web api.
    /// </summary>
    public interface IGameJoltRequest
    {
        /// <summary>
        /// Composes the url associated with this request.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        string ComposeUrl(GameJoltApiClient api);

        /// <summary>
        /// Once the api gets a response for a response, it calls this method so the request can store the response.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        void SetResponse(object model);

        /// <summary>
        /// Sets the timing results for this request.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        void SetTimingResult(DateTime start, DateTime end);
    }
}
