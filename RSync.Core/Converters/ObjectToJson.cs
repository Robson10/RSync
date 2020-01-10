using Newtonsoft.Json;

namespace RSync.Core.Converters
{
    /// <summary>
    /// This class serialize and deserialize any object to json
    /// </summary>
    public static class ObjectToJson
    {
        /// <summary>
        /// Serialize any type object to json format.
        /// </summary>
        /// <typeparam name="T">Type of serialized object.</typeparam>
        /// <param name="value">Object to serialize.</param>
        /// <returns>Serialized object to json format as string</returns>
        public static string Serialize<T>(this T value)
        {
            if (value == null)
            {
                return string.Empty;
            }

            string json = JsonConvert.SerializeObject(value, Formatting.Indented);
            return json;
        }

        /// <summary>
        /// Deserialize string to object(T).
        /// </summary>
        /// <typeparam name="T">Type of expected object.</typeparam>
        /// <param name="json">Text to deserialize.</param>
        /// <returns>Expected object(T) or default value of T if text contains errors.</returns>
        public static T Deserialize<T>(string json)
        {
            T value;
            try
            {
                value = JsonConvert.DeserializeObject<T>(json);
            }
            catch (JsonReaderException)
            {
                value = default;
            }

            return value;
        }
    }
}