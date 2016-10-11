using System.Collections.Generic;
using System.Linq;
using JoltNet.DataModel;

namespace JoltNet
{
    public static partial class RequestProvider
    {
        /// <summary>
        /// Provides requests to api endpoints interacting with trophies.
        /// </summary>
        public static class Trophies
        {
            #region Fetch

            /// <summary>
            /// A response for a request to get trophy data for a game.
            /// </summary>
            public class TrophiesResponse : GameJoltResponse
            {
                /// <summary>
                /// The data of a trophy.
                /// </summary>
                public struct TrophyData
                {
                    /// <summary>
                    /// The id of the trophy. The trophy id is unique on all of Game Jolt, not only a single game.
                    /// </summary>
                    public readonly string Id;
                    /// <summary>
                    /// The name of the trophy.
                    /// </summary>
                    public readonly string Title;
                    /// <summary>
                    /// The description of the trophy.
                    /// </summary>
                    public readonly string Description;
                    /// <summary>
                    /// The Url to the image of the trophy, which is a 75x75 px .jpg file.
                    /// </summary>
                    public readonly string ImageUrl;

                    /// <summary>
                    /// If the user has achieved this trophy.
                    /// </summary>
                    public readonly bool IsAchieved;
                    /// <summary>
                    /// How long ago the user has achieved this trophy.
                    /// </summary>
                    public readonly string AchievedDisplayTime;

                    /// <summary>
                    /// How difficult it is to achieve this trophy.
                    /// </summary>
                    public readonly TrophyDifficulty Difficulty;

                    internal TrophyData(TrophyModel model)
                    {
                        Id = model.Id;
                        Title = model.Title;
                        ImageUrl = model.ImageUrl;
                        Title = model.Title;
                        Description = model.Description;
                        Difficulty = model.Difficulty;

                        if (!bool.TryParse(model.Achieved, out IsAchieved))
                        {
                            IsAchieved = true;
                            AchievedDisplayTime = model.Achieved;
                        }
                        else
                        {
                            AchievedDisplayTime = null;
                        }
                    }
                }

                /// <summary>
                /// The requested trophy data.
                /// </summary>
                public TrophyData[] Trophies { get; }

                internal TrophiesResponse(ResponseModel responseModel)
                    : base(responseModel)
                {
                    if (responseModel.Success)
                        Trophies = responseModel.Trophies.Select(m => new TrophyData(m)).ToArray();
                }
            }

            /// <summary>
            /// Returns a request to fetch all trophy data of the game.
            /// </summary>
            /// <param name="username">The name of the user to return trophy information for.</param>
            /// <param name="user_token">The token of the user to return trophy information for.</param>
            public static GameJoltRequest<TrophiesResponse> Fetch(string username, string user_token)
                => new GameJoltRequest<TrophiesResponse>("trophies",
                    new Dictionary<string, string>()
                    {
                        { nameof(username), username },
                        { nameof(user_token), user_token}
                    });

            /// <summary>
            /// Returns a request to fetch the trophy data for all trophies with the set achieved state.
            /// </summary>
            /// <param name="achieved"><see cref="true"/> for all achieved trophies, <see cref="false"/> for not achieved trophies.</param>
            /// <param name="username">The name of the user to return trophy information for.</param>
            /// <param name="user_token">The token of the user to return trophy information for.</param>
            public static GameJoltRequest<TrophiesResponse> Fetch(bool achieved, string username, string user_token)
                => new GameJoltRequest<TrophiesResponse>("trophies",
                    new Dictionary<string, string>()
                    {
                        { nameof(achieved), achieved.ToString().ToLowerInvariant() },
                        { nameof(username), username },
                        { nameof(user_token), user_token}
                    });

            /// <summary>
            /// Returns a request to fetch the trophy data for a single trophy.
            /// </summary>
            /// <param name="trophy_id">The id of the trophy to return data for.</param>
            /// <param name="username">The name of the user to return trophy information for.</param>
            /// <param name="user_token">The token of the user to return trophy information for.</param>
            public static GameJoltRequest<TrophiesResponse> Fetch(int trophy_id, string username, string user_token)
                => new GameJoltRequest<TrophiesResponse>("trophies",
                    new Dictionary<string, string>()
                    {
                        { nameof(trophy_id), trophy_id.ToString() },
                        { nameof(username), username },
                        { nameof(user_token), user_token}
                    });

            /// <summary>
            /// Returns a request to fetch the trophy data for a multiple trophies.
            /// </summary>
            /// <param name="trophy_id">The ids of the trophies to return data for.</param>
            /// <param name="username">The name of the user to return trophy information for.</param>
            /// <param name="user_token">The token of the user to return trophy information for.</param>
            public static GameJoltRequest<TrophiesResponse> Fetch(int[] trophy_id, string username, string user_token)
                => new GameJoltRequest<TrophiesResponse>("trophies",
                    new Dictionary<string, string>()
                    {
                        { nameof(trophy_id), string.Join(",", trophy_id) },
                        { nameof(username), username },
                        { nameof(user_token), user_token}
                    });

            #endregion

            #region SetAchieved

            /// <summary>
            /// Returns a request to set the achieved status to <see cref="true"/> for a trophy.
            /// </summary>
            /// <param name="trophy_id">The id of the trophy that should get set to achieved.</param>
            /// <param name="username">The name of the user to set trophy information for.</param>
            /// <param name="user_token">The token of the user to set trophy information for.</param>
            public static GameJoltRequest<GameJoltResponse> SetAchieved(int trophy_id, string username, string user_token)
                => new GameJoltRequest<GameJoltResponse>("trophies/add-achieved",
                    new Dictionary<string, string>()
                    {
                        { nameof(trophy_id), trophy_id.ToString() },
                        { nameof(username), username },
                        { nameof(user_token), user_token}
                    });

            #endregion
        }
    }
}
