using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formulate.app.Components
{
    using System.IO;

    using formulate.app.Configuration;
    using formulate.app.Constants.Configuration;

    using Newtonsoft.Json;

    using Umbraco.Core.Composing;
    using Umbraco.Core.Configuration;
    using Umbraco.Core.IO;

    /// <summary>
    /// The update version component.
    /// </summary>
    internal sealed class UpdateVersionComponent : IComponent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateVersionComponent"/> class.
        /// </summary>
        /// <param name="config">
        /// The config.
        /// </param>
        public UpdateVersionComponent(IFormulateConfig config)
        {
            Config = config;
        }

        /// <summary>
        /// Gets or sets the config.
        /// </summary>
        private IFormulateConfig Config { get; set; }

        /// <summary>
        /// The code which will update the Formulate version number.
        /// </summary>
        public void Initialize()
        {
            var jsonConfig = Config as FormulateConfig;

            if (jsonConfig == null)
            {
                return;
            }

            jsonConfig.Version = meta.Constants.Version;

            SaveToFileSystem(jsonConfig, ConfigFilePaths.FormulateConfigPath);
        }

        /// <summary>
        /// Run during component termination.
        /// </summary>
        public void Terminate()
        {
        }

        /// <summary>
        /// Updates the JSON file system with a newer version of the config.
        /// </summary>
        /// <param name="config">
        /// The config.
        /// </param>
        /// <param name="filePath">
        /// The file path.
        /// </param>
        /// <exception cref="Exception">In case of any issues saving to file.</exception>
        private void SaveToFileSystem(FormulateConfig config, string filePath)
        {
            var mappedPath = IOHelper.MapPath(filePath);

            if (File.Exists(mappedPath))
            {
                try
                {
                    using (var file = File.CreateText(mappedPath))
                    {
                        var serializer = new JsonSerializer();
                        serializer.Serialize(file, config);
                    }
                }
                catch (Exception ex)
                {
                    Current.Logger.Error(typeof(Configs), ex, "Config error");
                    throw ex;
                }
            }
        }
    }
}
