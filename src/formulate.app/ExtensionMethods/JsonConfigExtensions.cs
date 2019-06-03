namespace formulate.app.ExtensionMethods
{
    using System;
    using System.Configuration;
    using System.IO;

    using Newtonsoft.Json;

    using Umbraco.Core.Configuration;
    using Umbraco.Core.IO;
    using Umbraco.Web.Composing;

    /// <summary>
    /// The collection of config extensions to read &amp; register JSON based configuration.
    /// </summary>
    internal static class JsonConfigExtensions
    {
        /// <summary>
        /// Adds a configuration, provided by a JSON file.
        /// </summary>
        /// <typeparam name="TConfig">
        /// The type used for registering &amp; deserializing the config element.
        /// </typeparam>
        /// <param name="configs">
        /// The configs.
        /// </param>
        /// <param name="filePath">
        /// The file path to the JSON file.
        /// </param>
        public static void AddJsonConfig<TConfig>(this Configs configs, string filePath)
            where TConfig : class
        {
            configs.Add(() => GetJsonConfig<TConfig, TConfig>(filePath));
        }

        /// <summary>
        /// Adds a configuration, provided by a JSON file. This method allows separate types for registering and deserialization.
        /// </summary>
        /// <typeparam name="TConfig">
        /// The type used for registering the config element.
        /// </typeparam>
        /// <typeparam name="TDeserializationConfig">
        /// The type used for deserializing the config element. This type must implement the TConfig type. 
        /// </typeparam>
        /// <param name="configs">
        /// The configs.
        /// </param>
        /// <param name="filePath">
        /// The file path to the JSON file.
        /// </param>
        public static void AddJsonConfig<TConfig, TDeserializationConfig>(this Configs configs, string filePath)
            where TConfig : class
            where TDeserializationConfig : class, TConfig
        {
            configs.Add(() => GetJsonConfig<TConfig, TDeserializationConfig>(filePath));
        }

        /// <summary>
        /// Gets a config object from a given file path.
        /// </summary>
        /// <typeparam name="TConfig">
        /// The type used for registering the config element.
        /// </typeparam>
        /// <typeparam name="TDeserializationConfig">
        /// The type used for deserializing the config element. This type must implement the TConfig type. 
        /// </typeparam>
        /// <param name="filePath">
        /// The file path to the JSON file.
        /// </param>
        /// <returns>The derserialized object from the file path.</returns>
        private static TConfig GetJsonConfig<TConfig, TDeserializationConfig>(string filePath)
            where TConfig : class
            where TDeserializationConfig : class, TConfig
        {
            var mappedPath = IOHelper.MapPath(filePath);

            if (File.Exists(mappedPath))
            {
                try
                {
                    using (var file = File.OpenText(mappedPath))
                    {
                        using (var jsonReader = new JsonTextReader(file))
                        {
                            var serializer = new JsonSerializer();
                            return serializer.Deserialize<TDeserializationConfig>(jsonReader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Current.Logger.Error(typeof(Configs), ex, "Config error");
                    throw ex;
                }
            }

            var configEx = new ConfigurationErrorsException($"Could not get JSON configuration from file path \"{mappedPath}\".");
            Current.Logger.Error(typeof(Configs), configEx, "Config error");
            throw configEx;
        }
    }
}
