using System.Collections.Generic;
using System.Linq;
using JoltNet.DataModel;

namespace JoltNet
{
    /// <summary>
    /// Collection of all available api requests.
    /// </summary>
    public static partial class RequestProvider
    {
        /// <summary>
        /// Provides requests to api endpoints interacting with scores and scoreboards.
        /// </summary>
        public static class Scores
        {
            #region Tables

            /// <summary>
            /// A response for a request to get scoreboard information of a game.
            /// </summary>
            public class TablesResponse : GameJoltResponse
            {
                /// <summary>
                /// The data of a scoretable.
                /// </summary>
                public struct TableData
                {
                    public readonly string Id, Name, Description;
                    /// <summary>
                    /// If this scoretable is the primary scoretable of the game.
                    /// The primary scoretable is the default if no scoretable id is provided for certain requests.
                    /// </summary>
                    public readonly bool IsPrimary;

                    internal TableData(ScoreTableModel model)
                    {
                        Id = model.Id;
                        Name = model.Name;
                        Description = model.Description;
                        IsPrimary = model.Primary == "1";
                    }
                }

                /// <summary>
                /// The scoretables of the game.
                /// </summary>
                public TableData[] Tables { get; }

                internal TablesResponse(ResponseModel responseModel)
                    : base(responseModel)
                {
                    if (responseModel.Success)
                        Tables = responseModel.Tables.Select(m => new TableData(m)).ToArray();
                }
            }

            /// <summary>
            /// Returns a request to fetch information on all scoretables of the game.
            /// </summary>
            public static GameJoltRequest<TablesResponse> FetchTables()
                => new GameJoltRequest<TablesResponse>("scores/tables");

            #endregion

            #region Add

            /// <summary>
            /// Returns a request to add a score to the default scoreboard.
            /// </summary>
            /// <param name="sort">The numeric value of the score that identifies its comparable value.</param>
            /// <param name="score">The description of the score value.</param>
            /// <param name="username">The name of the user that achieved the score.</param>
            /// <param name="user_token">The token of the user that achieved the score.</param>
            /// <param name="extra_data">Additional data that is not publicly visible on Game Jolt. Retrievable via a Fetch request.</param>
            public static GameJoltRequest<GameJoltResponse> Add(int sort, string score, string username, string user_token, string extra_data = "")
                => new GameJoltRequest<GameJoltResponse>("scores/add",
                    new Dictionary<string, string>()
                    {
                        { nameof(sort), sort.ToString() },
                        { nameof(score), score },
                        { nameof(username), username },
                        { nameof(user_token), user_token },
                        { nameof(extra_data), extra_data }
                    });

            /// <summary>
            /// Returns a request to add a score to a scoretable.
            /// </summary>
            /// <param name="table_id">The identifier of the scoreboard to add the score to.</param>
            /// <param name="sort">The numeric value of the score that identifies its comparable value.</param>
            /// <param name="score">The description of the score value.</param>
            /// <param name="username">The name of the user that achieved the score.</param>
            /// <param name="user_token">The token of the user that achieved the score.</param>
            /// <param name="extra_data">Additional data that is not publicly visible on Game Jolt. Retrievable via a Fetch request.</param>
            public static GameJoltRequest<GameJoltResponse> Add(string table_id, int sort, string score, string username, string user_token, string extra_data = "")
                => new GameJoltRequest<GameJoltResponse>("scores/add",
                    new Dictionary<string, string>()
                    {
                        { nameof(sort), sort.ToString() },
                        { nameof(score), score },
                        { nameof(table_id), table_id },
                        { nameof(username), username },
                        { nameof(user_token), user_token },
                        { nameof(extra_data), extra_data }
                    });

            /// <summary>
            /// Returns a request to add a score to the default scoretable as a guest.
            /// </summary>
            /// <param name="sort">The numeric value of the score that identifies its comparable value.</param>
            /// <param name="score">The description of the score value.</param>
            /// <param name="guest">The name of the guest user.</param>
            /// <param name="extra_data">Additional data that is not publicly visible on Game Jolt. Retrievable via a Fetch request.</param>
            public static GameJoltRequest<GameJoltResponse> Add(int sort, string score, string guest, string extra_data = "")
                => new GameJoltRequest<GameJoltResponse>("scores/add",
                    new Dictionary<string, string>()
                    {
                        { nameof(sort), sort.ToString() },
                        { nameof(score), score },
                        { nameof(guest), guest },
                        { nameof(extra_data), extra_data }
                    });

            /// <summary>
            /// Returns a request to add a score to a scoretable as a guest.
            /// </summary>
            /// <param name="table_id">The identifier of the scoreboard to add the score to.</param>
            /// <param name="sort">The numeric value of the score that identifies its comparable value.</param>
            /// <param name="score">The description of the score value.</param>
            /// <param name="guest">The name of the guest user.</param>
            /// <param name="extra_data">Additional data that is not publicly visible on Game Jolt. Retrievable via a Fetch request.</param>
            public static GameJoltRequest<GameJoltResponse> Add(string table_id, int sort, string score, string guest, string extra_data = "")
                => new GameJoltRequest<GameJoltResponse>("scores/add",
                    new Dictionary<string, string>()
                    {
                        { nameof(sort), sort.ToString() },
                        { nameof(score), score },
                        { nameof(table_id), table_id },
                        { nameof(guest), guest },
                        { nameof(extra_data), extra_data }
                    });

            #endregion

            #region FetchScoreRank

            /// <summary>
            /// A response for a request to get the ranking of a score on a scoreboard.
            /// </summary>
            public class ScoreRankResponse : GameJoltResponse
            {
                /// <summary>
                /// The rank of the requested score on the scoreboard.
                /// </summary>
                public int Rank { get; }

                internal ScoreRankResponse(ResponseModel responseModel)
                    : base(responseModel)
                {
                    Rank = responseModel.Rank;
                }
            }

            /// <summary>
            /// Returns a request to fetch the ranking of a score on the default scoreboard.
            /// </summary>
            /// <param name="sort">The numeric value of the score that identifies its comparable value.</param>
            public static GameJoltRequest<ScoreRankResponse> FetchRank(int sort)
                => new GameJoltRequest<ScoreRankResponse>("scores/get-rank", nameof(sort), sort.ToString());

            /// <summary>
            /// Returns a request to fetch the ranking of a score on a scoreboard.
            /// </summary>
            /// <param name="table_id">The identifier of the scoreboard to add the score to.</param>
            /// <param name="sort">The numeric value of the score that identifies its comparable value.</param>
            public static GameJoltRequest<ScoreRankResponse> FetchRank(string table_id, int sort)
                => new GameJoltRequest<ScoreRankResponse>("scores/get-rank",
                    new Dictionary<string, string>()
                    {
                        { nameof(table_id), table_id },
                        { nameof(sort), sort.ToString() }
                    });

            #endregion

            #region FetchScores

            /// <summary>
            /// A response for a request to get data for a score from a scoreboard.
            /// </summary>
            public class ScoresResponse : GameJoltResponse
            {
                /// <summary>
                /// The data for a score on a scoreboard.
                /// </summary>
                public struct ScoreData
                {
                    /// <summary>
                    /// The description of the score value.
                    /// </summary>
                    public readonly string Score;
                    /// <summary>
                    /// Additional data that is not publicly visible on Game Jolt.
                    /// </summary>
                    public readonly string ExtraData;
                    /// <summary>
                    /// Contains the username of this score, or the name of the guest user.
                    /// </summary>
                    public readonly string User;
                    /// <summary>
                    /// Contains the user id of this score. Is <see cref="null"/> when this is a guest score.
                    /// </summary>
                    public readonly string UserId;
                    /// <summary>
                    /// The display time for when this score was stored on the scoreboard.
                    /// </summary>
                    public readonly string StoredDisplayTime;
                    /// <summary>
                    /// If this score was scored by a guest user.
                    /// </summary>
                    public readonly bool IsGuestScore;
                    /// <summary>
                    /// The numeric value of the score that identifies its comparable value.
                    /// </summary>
                    public readonly int Sort;

                    internal ScoreData(ScoreModel scoreModel)
                    {
                        Score = scoreModel.Score;
                        ExtraData = scoreModel.ExtraData;
                        User = scoreModel.User ?? scoreModel.Guest;
                        IsGuestScore = scoreModel.Guest != null;
                        UserId = scoreModel.UserId;
                        StoredDisplayTime = scoreModel.Stored;
                        Sort = scoreModel.Sort;
                    }
                }

                /// <summary>
                /// The scores returned by the request.
                /// </summary>
                public ScoreData[] Scores { get; }

                internal ScoresResponse(ResponseModel responseModel)
                    : base(responseModel)
                {
                    if (Success && responseModel.Scores != null)
                        Scores = responseModel.Scores.Select(s => new ScoreData(s)).ToArray();
                }
            }

            /// <summary>
            /// Returns a request to fetch the first 10 scores from the default scoreboard.
            /// </summary>
            public static GameJoltRequest<ScoresResponse> Fetch()
                => new GameJoltRequest<ScoresResponse>("scores");

            /// <summary>
            /// Returns a request to fetch the first scores from the default scoreboard.
            /// </summary>
            /// <param name="limit">The amount of scores to return.</param>
            public static GameJoltRequest<ScoresResponse> Fetch(int limit)
                => new GameJoltRequest<ScoresResponse>("scores", nameof(limit), GetValidLimit(limit));

            /// <summary>
            /// Returns a request to fetch the first 10 scores from the default scoreboard that were scored by a certain user.
            /// </summary>
            /// <param name="username">The name of the user that achieved the score.</param>
            /// <param name="user_token">The token of the user that achieved the score.</param>
            public static GameJoltRequest<ScoresResponse> Fetch(string username, string user_token)
                => new GameJoltRequest<ScoresResponse>("scores",
                    new Dictionary<string, string>()
                    {
                        { nameof(username), username },
                        { nameof(user_token), user_token }
                    });

            /// <summary>
            /// Returns a request to fetch the first scores from the default scoreboard that were scored by a certain user.
            /// </summary>
            /// <param name="limit">The amount of scores to return.</param>
            /// <param name="username">The name of the user that achieved the score.</param>
            /// <param name="user_token">The token of the user that achieved the score.</param>
            public static GameJoltRequest<ScoresResponse> Fetch(int limit, string username, string user_token)
                => new GameJoltRequest<ScoresResponse>("scores",
                    new Dictionary<string, string>()
                    {
                        { nameof(limit), GetValidLimit(limit) },
                        { nameof(username), username },
                        { nameof(user_token), user_token }
                    });

            /// <summary>
            /// Returns a request to fetch the first 10 scores from a scoreboard.
            /// </summary>
            /// <param name="table_id">The identifier of the scoreboard to fetch the score from.</param>
            public static GameJoltRequest<ScoresResponse> Fetch(string table_id)
                => new GameJoltRequest<ScoresResponse>("scores", nameof(table_id), table_id);

            /// <summary>
            /// Returns a request to fetch the first 10 scores from a scoreboard that were scored by a certain user.
            /// </summary>
            /// <param name="table_id">The identifier of the scoreboard to fetch the score from.</param>
            /// <param name="username">The name of the user that achieved the score.</param>
            /// <param name="user_token">The token of the user that achieved the score.</param>
            public static GameJoltRequest<ScoresResponse> Fetch(string table_id, string username, string user_token)
                => new GameJoltRequest<ScoresResponse>("scores",
                    new Dictionary<string, string>()
                    {
                        { nameof(table_id), table_id },
                        { nameof(username), username },
                        { nameof(user_token), user_token }
                    });

            /// <summary>
            /// Returns a request to fetch the first scores from a scoreboard.
            /// </summary>
            /// <param name="table_id">The identifier of the scoreboard to fetch the score from.</param>
            /// <param name="limit">The amount of scores to return.</param>
            public static GameJoltRequest<ScoresResponse> Fetch(string table_id, int limit)
                => new GameJoltRequest<ScoresResponse>("scores",
                    new Dictionary<string, string>()
                    {
                        { nameof(table_id), table_id },
                        { nameof(limit), GetValidLimit(limit) }
                    });

            /// <summary>
            /// Returns a request to fetch the first scores from a scoreboard that were scored by a certain user.
            /// </summary>
            /// <param name="table_id">The identifier of the scoreboard to fetch the score from.</param>
            /// <param name="limit">The amount of scores to return.</param>
            /// <param name="username">The name of the user that achieved the score.</param>
            /// <param name="user_token">The token of the user that achieved the score.</param>
            public static GameJoltRequest<ScoresResponse> Fetch(string table_id, int limit, string username, string user_token)
                => new GameJoltRequest<ScoresResponse>("scores",
                    new Dictionary<string, string>()
                    {
                        { nameof(table_id), table_id },
                        { nameof(limit), GetValidLimit(limit) },
                        { nameof(username), username },
                        { nameof(user_token), user_token }
                    });

            private static string GetValidLimit(int limit)
            {
                if (limit < 0) // when the input limit is < 0, return "10" as the defalt. "0" would result in the same outcome.
                    return "10";
                if (limit > 100) // The maximum allowed score limit is 100.
                    return "100";
                return limit.ToString();
            }

            #endregion
        }
    }
}
