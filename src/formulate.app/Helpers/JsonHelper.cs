namespace formulate.app.Helpers
{

    // Namespaces.
    using Newtonsoft.Json;


    /// <summary>
    /// Helps with JSON operations.
    /// </summary>
    internal class JsonHelper
    {

        #region Methods

        /// <summary>
        /// Serializes an object as JSON.
        /// </summary>
        /// <param name="item">The object to serialize.</param>
        /// <returns>
        /// The serialized object, or null.
        /// </returns>
        public static string Serialize(object item)
        {
            if (item == null)
            {
                return null;
            }
            var indented = Formatting.Indented;
            var settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.None
            };
            var result = JsonConvert.SerializeObject(item, indented, settings);
            return result;
        }


        /// <summary>
        /// Deserializes a JSON string into an instance.
        /// </summary>
        /// <typeparam name="T">The type to deserialize to.</typeparam>
        /// <param name="value">The JSON value.</param>
        /// <returns>The instance, or the default for the type.</returns>
        public static T Deserialize<T>(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return default(T);
            }
            var settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.None
            };
            var result = JsonConvert.DeserializeObject<T>(value, settings);
            return result;
        }

        #endregion

    }

}

//TODO: Get rid of static functions.