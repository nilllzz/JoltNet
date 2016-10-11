using System;
using System.IO;
using System.Runtime.Serialization.Json;

namespace JoltNet.DataModel.Serialization
{
    /// <summary>
    /// Serializes and deserializes JavaScript Object Notation (json) data.
    /// </summary>
    internal sealed class JsonDataSerializer<T> where T : DataModel<T>
    {
        internal T FromString(string data)
        {
            // We create a new Json serializer of the given type and a corresponding memory stream here.
            var serializer = new DataContractJsonSerializer(typeof(T), new DataContractJsonSerializerSettings() { SerializeReadOnlyTypes = true });
            
            try
            {
                using (var memStream = new MemoryStream())
                {
                    using (var sw = new StreamWriter(memStream))
                    {
                        // Create StreamWriter to the memory stream, which writes the input string to the stream.
                        sw.Write(data);
                        sw.Flush();

                        // Reset the stream's position to the beginning:
                        memStream.Position = 0;

                        return serializer.ReadObject(memStream) as T;
                    }
                }
            }
            catch (Exception ex)
            {
                // Exception occurs while loading the object due to malformed Json.
                // Throw exception and move up to handler class.
                throw new InvalidDataException("The data returned from the Game Jolt endpoint had an invalid format.", ex);
            }
        }
    }
}
