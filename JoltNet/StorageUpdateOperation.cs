namespace JoltNet
{
    /// <summary>
    /// The type of operation to be performed on a storage item.
    /// </summary>
    public enum StorageUpdateOperation
    {
        /// <summary>
        /// Sums the input and the storage value.
        /// </summary>
        Add,
        /// <summary>
        /// Appends the input to the end of the storage value.
        /// </summary>
        Append,
        /// <summary>
        /// Divides the storage value by the input.
        /// </summary>
        Divide,
        /// <summary>
        /// Multiplies the storage value with the input.
        /// </summary>
        Multiply,
        /// <summary>
        /// Adds the input to the front of the storage value.
        /// </summary>
        Prepend,
        /// <summary>
        /// Subtracts the input from the storage value.
        /// </summary>
        Subtract
    }
}
