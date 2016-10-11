using System;
using System.Runtime.Serialization;
using JoltNet.DataModel.Serialization;

namespace JoltNet.DataModel
{
    /// <summary>
    /// The base data model class.
    /// </summary>
    [DataContract(Namespace = "")]
    internal abstract class DataModel<T> where T : DataModel<T>
    {
        /// <summary>
        /// Creates a data model of type <see cref="T"/>.
        /// </summary>
        /// <typeparam name="T">The return type of the data model.</typeparam>
        /// <param name="data">The data input string.</param>
        internal static T FromString(string data)
            => new JsonDataSerializer<T>().FromString(data);

        /// <summary>
        /// Converts a <see cref="string"/> to a member of the given enum type.
        /// </summary>
        protected static TEnum ConvertStringToEnum<TEnum>(string enumString) where TEnum : struct, IComparable
        {
            TEnum result;
            if (Enum.TryParse(enumString, true, out result)) return result;

            return default(TEnum);
        }
    }
}
