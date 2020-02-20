using Newtonsoft.Json;
using RSync.AppResources.Localization;
using System;

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
            string json = JsonConvert.SerializeObject(value, Formatting.Indented);

            return json;
        }

        /// <summary>
        /// Deserialize string to object(T).
        /// </summary>
        /// <typeparam name="T">Type of expected object.</typeparam>
        /// <param name="json">Text to deserialize.</param>
        /// <exception cref="JsonReaderException">The exception thrown on deserialization reading.</exception>
        /// <exception cref="JsonSerializationException">The exception thrown when cannot serialize to expected object.</exception>
        /// <exception cref="ArgumentNullException">The exception thrown when argument is null.</exception>
        /// <returns>Expected object(T) or default value of T if text contains errors.</returns>
        public static T Deserialize<T>(this string json)
        {

            T value;
            value = JsonConvert.DeserializeObject<T>(json);

            return value;
        }
    }
}