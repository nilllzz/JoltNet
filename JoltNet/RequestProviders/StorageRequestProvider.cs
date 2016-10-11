using System.Collections.Generic;
using System.Linq;
using JoltNet.DataModel;

namespace JoltNet
{
    public static partial class RequestProvider
    {
        /// <summary>
        /// Provides requests to api endpoints interacting with the data storage.
        /// </summary>
        public static class Storage
        {
            #region Set

            /// <summary>
            /// Returns a request to set the data of a global storage item.
            /// </summary>
            /// <param name="key">The key that identifies the storage item.</param>
            /// <param name="data">The data to the set the storage item to.</param>
            public static GameJoltRequest<GameJoltResponse> Set(string key, string data)
                => new GameJoltRequest<GameJoltResponse>("data-store/set",
                    new Dictionary<string, string>()
                    {
                        { nameof(key), key },
                        { nameof(data), data}
                    });

            /// <summary>
            /// Returns a request to set the data of a user storage item.
            /// </summary>
            /// <param name="key">The key that identifies the storage item.</param>
            /// <param name="data">The data to the set the storage item to.</param>
            /// <param name="username">The name of the user who owns the storage item.</param>
            /// <param name="user_token">The token of the user who owns the storage item.</param>
            public static GameJoltRequest<GameJoltResponse> Set(string key, string data, string username, string user_token)
                => new GameJoltRequest<GameJoltResponse>("data-store/set",
                    new Dictionary<string, string>()
                    {
                        { nameof(key), key },
                        { nameof(data), data},
                        { nameof(username), username },
                        { nameof(user_token), user_token}
                    });

            /// <summary>
            /// Returns a request to create a global storage item with restricted access.
            /// </summary>
            /// <param name="key">The key that identifies the storage item.</param>
            /// <param name="data">The data to the set the storage item to.</param>
            /// <param name="restriction_username">The name of the user who owns the storage item. This is the only user who will be able to write to that storage item once its created.</param>
            /// <param name="restriction_user_token">The token of the user who owns the storage item.</param>
            public static GameJoltRequest<GameJoltResponse> SetRestricted(string key, string data, string restriction_username, string restriction_user_token)
                => new GameJoltRequest<GameJoltResponse>("data-store/set",
                    new Dictionary<string, string>()
                    {
                        { nameof(key), key },
                        { nameof(data), data},
                        { nameof(restriction_username), restriction_username },
                        { nameof(restriction_user_token), restriction_user_token}
                    });

            #endregion

            #region Remove

            /// <summary>
            /// Returns a request to remove a storage item from the global data storage.
            /// </summary>
            /// <param name="key">The key that identifies the storage item.</param>
            public static GameJoltRequest<GameJoltResponse> Remove(string key)
                => new GameJoltRequest<GameJoltResponse>("data-store/remove", nameof(key), key);

            /// <summary>
            /// Returns a request to remove a storage item from a user's data storage.
            /// </summary>
            /// <param name="key">The key that identifies the storage item.</param>
            /// <param name="username">The name of the user who owns the storage item.</param>
            /// <param name="user_token">The token of the user who owns the storage item.</param>
            public static GameJoltRequest<GameJoltResponse> Remove(string key, string username, string user_token)
                => new GameJoltRequest<GameJoltResponse>("data-store/remove",
                    new Dictionary<string, string>()
                    {
                        { nameof(key), key },
                        { nameof(username), username },
                        { nameof(user_token), user_token}
                    });

            #endregion

            #region Update

            /// <summary>
            /// Returns a request to update the contents of a global storage item.
            /// </summary>
            /// <param name="key">The key that identifies the storage item.</param>
            /// <param name="operation">The operation to perform on the storage item's content.</param>
            /// <param name="value">The value to change the storage item content with.</param>
            public static GameJoltRequest<FetchResponse> Update(string key, StorageUpdateOperation operation, string value)
                => new GameJoltRequest<FetchResponse>("data-store/update",
                    new Dictionary<string, string>()
                    {
                        { nameof(key), key },
                        { nameof(value), value},
                        { nameof(operation), operation.ToString().ToLowerInvariant() }
                    });

            /// <summary>
            /// Returns a request to update the contents of a user's storage item.
            /// </summary>
            /// <param name="key">The key that identifies the storage item.</param>
            /// <param name="operation">The operation to perform on the storage item's content.</param>
            /// <param name="value">The value to change the storage item content with.</param>
            /// <param name="username">The name of the user who owns the storage item.</param>
            /// <param name="user_token">The token of the user who owns the storage item.</param>
            public static GameJoltRequest<FetchResponse> Update(string key, StorageUpdateOperation operation, string value, string username, string user_token)
                => new GameJoltRequest<FetchResponse>("data-store/update",
                    new Dictionary<string, string>()
                    {
                        { nameof(key), key },
                        { nameof(value), value},
                        { nameof(operation), operation.ToString().ToLowerInvariant() },
                        { nameof(username), username },
                        { nameof(user_token), user_token}
                    });

            #endregion

            #region Fetch

            /// <summary>
            /// A response for a request to fetch storage item data from the data storage.
            /// </summary>
            public class FetchResponse : GameJoltResponse
            {
                /// <summary>
                /// The data from the storage item.
                /// </summary>
                public string Data { get; }

                internal FetchResponse(ResponseModel responseModel)
                    : base(responseModel)
                {
                    Data = responseModel.Data;
                }
            }

            /// <summary>
            /// Returns a request to fetch the contents of a global storage item.
            /// </summary>
            /// <param name="key">The key that identifies the storage item.</param>
            public static GameJoltRequest<FetchResponse> Fetch(string key)
                => new GameJoltRequest<FetchResponse>("data-store", nameof(key), key);

            /// <summary>
            /// Returns a request to fetch the contents of a global storage item.
            /// </summary>
            /// <param name="key">The key that identifies the storage item.</param>
            /// <param name="username">The name of the user who owns the storage item.</param>
            /// <param name="user_token">The token of the user who owns the storage item.</param>
            public static GameJoltRequest<FetchResponse> Fetch(string key, string username, string user_token)
                => new GameJoltRequest<FetchResponse>("data-store",
                    new Dictionary<string, string>()
                    {
                        { nameof(key), key },
                        { nameof(username), username },
                        { nameof(user_token), user_token}
                    });

            #endregion

            #region Keys

            /// <summary>
            /// A response for a request to get keys for storage item from the data storage.
            /// </summary>
            public class GetKeysResponse : GameJoltResponse
            {
                /// <summary>
                /// The requested keys.
                /// </summary>
                public string[] Keys { get; }

                internal GetKeysResponse(ResponseModel responseModel)
                    : base(responseModel)
                {
                    if (Success)
                        Keys = responseModel.Keys.Select(k => k.Key).ToArray();
                }
            }

            /// <summary>
            /// Returns a request to get keys for all public data storage items.
            /// </summary>
            public static GameJoltRequest<GetKeysResponse> GetKeys()
                => new GameJoltRequest<GetKeysResponse>("data-store/get-keys");

            /// <summary>
            /// Returns a request to get keys that match the pattern for public data storage items.
            /// </summary>
            /// <param name="pattern">The pattern that keys have to match. Use * as a wildcard.</param>
            public static GameJoltRequest<GetKeysResponse> GetKeys(string pattern)
                => new GameJoltRequest<GetKeysResponse>("data-store/get-keys", nameof(pattern), pattern);

            /// <summary>
            /// Returns a request to get keys for all data storage items of a single user.
            /// </summary>
            /// <param name="username">The name of the user who owns the storage item.</param>
            /// <param name="user_token">The token of the user who owns the storage item.</param>
            public static GameJoltRequest<GetKeysResponse> GetKeys(string username, string user_token)
                => new GameJoltRequest<GetKeysResponse>("data-store/get-keys",
                    new Dictionary<string, string>()
                    {
                        { nameof(username), username },
                        { nameof(user_token), user_token}
                    });

            /// <summary>
            /// Returns a request to get keys that match te pattern for data storage items of a single user.
            /// </summary>
            /// <param name="pattern">The pattern that keys have to match. Use * as a wildcard.</param>
            /// <param name="username">The name of the user who owns the storage item.</param>
            /// <param name="user_token">The token of the user who owns the storage item.</param>
            public static GameJoltRequest<GetKeysResponse> GetKeys(string pattern, string username, string user_token)
                => new GameJoltRequest<GetKeysResponse>("data-store/get-keys",
                    new Dictionary<string, string>()
                    {
                        { nameof(pattern), pattern },
                        { nameof(username), username },
                        { nameof(user_token), user_token}
                    });

            #endregion
        }
    }
}
