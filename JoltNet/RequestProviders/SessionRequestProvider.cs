using System.Collections.Generic;

namespace JoltNet
{
    public static partial class RequestProvider
    {
        /// <summary>
        /// Provides requests to api endpoints interacting with sessions.
        /// </summary>
        public static class Sessions
        {
            /// <summary>
            /// Returns a request to open a new sessions for a user.
            /// </summary>
            /// <param name="username">The user name of the session owner.</param>
            /// <param name="user_token">The user token of the session owner.</param>
            public static GameJoltRequest<GameJoltResponse> Open(string username, string user_token)
                => new GameJoltRequest<GameJoltResponse>("sessions/open",
                    new Dictionary<string, string>()
                    {
                        { nameof(username), username },
                        { nameof(user_token), user_token}
                    });

            /// <summary>
            /// Returns a request to check the status of a session opened by a specific user.
            /// </summary>
            /// <param name="username">The user name of the session owner.</param>
            /// <param name="user_token">The user token of the session owner.</param>
            public static GameJoltRequest<GameJoltResponse> Check(string username, string user_token)
                => new GameJoltRequest<GameJoltResponse>("sessions/check",
                    new Dictionary<string, string>()
                    {
                        { nameof(username), username },
                        { nameof(user_token), user_token}
                    });

            /// <summary>
            /// Returns a request to close a session opened by a specific user.
            /// </summary>
            /// <param name="username">The user name of the session owner.</param>
            /// <param name="user_token">The user token of the session owner.</param>
            public static GameJoltRequest<GameJoltResponse> Close(string username, string user_token)
                => new GameJoltRequest<GameJoltResponse>("sessions/close",
                    new Dictionary<string, string>()
                    {
                        { nameof(username), username },
                        { nameof(user_token), user_token}
                    });

            /// <summary>
            /// Returns a request to ping a session opened by a specific user to keep it alive.
            /// </summary>
            /// <param name="status">The status to set the session status to.</param>
            /// <param name="username">The user name of the session owner.</param>
            /// <param name="user_token">The user token of the session owner.</param>
            public static GameJoltRequest<GameJoltResponse> Ping(SessionStatus status, string username, string user_token)
                => new GameJoltRequest<GameJoltResponse>("sessions/ping",
                    new Dictionary<string, string>()
                    {
                        { nameof(status), status.ToString().ToLowerInvariant() },
                        { nameof(username), username },
                        { nameof(user_token), user_token}
                    });

        }
    }
}
