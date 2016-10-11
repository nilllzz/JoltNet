namespace JoltNet
{
    /// <summary>
    /// Feature toggles of the Game Jolt api for requests.
    /// </summary>
    public enum GameJoltApiRequestFeature
    {
        None,
        /// <summary>
        /// Executes requests simulatinously on the server. Set to <see cref="false"/> if the order of execution is important.
        /// </summary>
        ParallelProcessing,
        /// <summary>
        /// If set to <see cref="true"/>, the first failing request will stop the execution and return a failure.
        /// </summary>
        BreakOnError
    }
}
