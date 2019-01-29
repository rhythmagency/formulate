namespace formulate.app.Helpers
{

    // Namespaces.
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;


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


        /// <summary>
        /// Reserializes a JSON string that log4net doesn't complain about (brackets are escaped).
        /// It also adds indenting for readability.
        /// </summary>
        /// <param name="input">
        /// The JSON string to reserialize.
        /// </param>
        /// <returns>
        /// Escaped, indented, camel-cased JSON string for logging.
        /// </returns>
        public static string FormatJsonForLogging(string input)
        {
            
            // Convert JSON string to object.
            var generic = JsonConvert.DeserializeObject<dynamic>(input);


            // Convert back to JSON string with specific settings (e.g., indenting, camel casing).
            // Then, escape curly braces.
            return JsonConvert.SerializeObject(generic,
                Formatting.Indented,
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                })
                .Replace("{", "{{")
                .Replace("}", "}}");

        }

        #endregion

    }

}

//TODO: Get rid of static functions.
