using System.Collections.Generic;
using System.Linq;
using JoltNet.DataModel;

namespace JoltNet
{
    public static partial class RequestProvider
    {
        /// <summary>
        /// Provides requests to api endpoints interacting with users.
        /// </summary>
        public static class Users
        {
            /// <summary>
            /// Returns a request to check if a passed in name and token pair is valid credentials.
            /// The result is saved in the <see cref="GameJoltResponse.Success"/> property.
            /// </summary>
            /// <param name="username">The username of the credentials.</param>
            /// <param name="user_token">The token of the credentials.</param>
            public static GameJoltRequest<GameJoltResponse> Authorize(string username, string user_token)
                => new GameJoltRequest<GameJoltResponse>("users/auth",
                    new Dictionary<string, string>()
                    {
                        { nameof(username), username },
                        { nameof(user_token), user_token}
                    });

            /// <summary>
            /// A response for a request to fetch user information from Game Jolt.
            /// </summary>
            public class UserResponse : GameJoltResponse
            {
                /// <summary>
                /// The data of a Game Jolt user.
                /// </summary>
                public struct UserData
                {
                    /// <summary>
                    /// The unique id of the user.
                    /// </summary>
                    public readonly string Id;
                    /// <summary>
                    /// The name of the user. The user can change this name at any time.
                    /// </summary>
                    public readonly string Username;
                    /// <summary>
                    /// The type of user, either normal user or developer.
                    /// </summary>
                    public readonly UserType Type;
                    /// <summary>
                    /// The url to the user's avatar.
                    /// </summary>
                    public readonly string AvatarUrl;
                    /// <summary>
                    /// The display time for how long ago the user's profile creation on Game Jolt is.
                    /// </summary>
                    public readonly string SignedUpDisplayTime;
                    /// <summary>
                    /// The display time for when the user logged in to their Game Jolt profile the last time.
                    /// </summary>
                    public readonly string LastLoggedInDisplayTime;
                    /// <summary>
                    /// The status of the user.
                    /// </summary>
                    public readonly UserStatus Status;
                    /// <summary>
                    /// When the user is a developer, this is the developer's name.
                    /// </summary>
                    public readonly string DeveloperName;
                    /// <summary>
                    /// When the user is a developer, this is the link to their website.
                    /// </summary>
                    public readonly string DeveloperWebsite;
                    /// <summary>
                    /// When the user is a developer, this is their description.
                    /// </summary>
                    public readonly string DeveloperDescription;

                    internal UserData(UserModel model)
                    {
                        Id = model.Id;
                        Username = model.Username;
                        Type = model.Type;
                        AvatarUrl = model.AvatarUrl;
                        SignedUpDisplayTime = model.SignedUp;
                        LastLoggedInDisplayTime = model.LastLoggedIn;
                        Status = model.Status;
                        DeveloperName = model.DeveloperName;
                        DeveloperWebsite = model.DeveloperWebsite;
                        DeveloperDescription = model.DeveloperDescription;
                    }
                }

                /// <summary>
                /// The requested user data.
                /// </summary>
                public UserData[] Users { get; }

                internal UserResponse(ResponseModel responseModel)
                    : base(responseModel)
                {
                    if (Success)
                        Users = responseModel.Users.Select(m => new UserData(m)).ToArray();
                }
            }

            /// <summary>
            /// Returns a request to fetch user data from a single user by username.
            /// </summary>
            /// <param name="username">The name of the user to fetch data from.</param>
            public static GameJoltRequest<UserResponse> Fetch(string username)
                => new GameJoltRequest<UserResponse>("users", nameof(username), username);

            /// <summary>
            /// Returns a request to fetch user data from one or more users by user ids.
            /// </summary>
            /// <param name="user_id">The ids of the users to fetch data from.</param>
            public static GameJoltRequest<UserResponse> Fetch(string[] user_id)
                => new GameJoltRequest<UserResponse>("users", nameof(user_id), string.Join(",", user_id));
        }
    }
}
