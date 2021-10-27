using System;
using Formulate.Core.DataValues;
using Formulate.Core.Folders;
using Formulate.Core.Forms;
using Formulate.Core.Layouts;
using Formulate.Core.Utilities;
using Formulate.Core.Validations;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Hosting;

namespace Formulate.Core.Persistence
{
    /// <summary>
    /// The default implementation of <see cref="IPersistenceUtilityFactory"/>.
    /// </summary>
    internal sealed class PersistenceUtilityFactory : IPersistenceUtilityFactory
    {
        /// <summary>
        /// The hosting environment.
        /// </summary>
        private readonly IHostingEnvironment _hostingEnvironment;

        /// <summary>
        /// The json utility.
        /// </summary>
        private readonly IJsonUtility _jsonUtility;

        /// <summary>
        /// The entity cache.
        /// </summary>
        private readonly IPersistedEntityCache _entityCache;
        
        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// The json root path.
        /// </summary>
        private readonly string _jsonRootPath;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersistenceUtilityFactory"/> class.
        /// </summary>
        /// <param name="hostingEnvironment">The hosting environment.</param>
        /// <param name="jsonUtility">The json utility.</param>
        /// <param name="entityCache">The entity cache.</param>
        /// <param name="logger">The logger.</param>
        public PersistenceUtilityFactory(IHostingEnvironment hostingEnvironment, IJsonUtility jsonUtility, IPersistedEntityCache entityCache, ILogger<PersistenceUtilityFactory> logger)
        {
            _hostingEnvironment = hostingEnvironment;
            _jsonUtility = jsonUtility;
            _entityCache = entityCache;
            _logger = logger;

            // TODO: Replace with config value.
            _jsonRootPath = "/App_Data/Formulate/Json/";
        }

        /// <inheritdoc />
        /// <exception cref="NotSupportedException">If the type provided is not supported.</exception>
        public IPersistenceUtility<TEntity> Create<TEntity>() where TEntity : class, IPersistedEntity
        {
            var settings = GetSettings<TEntity>();
            var updatedSettings = UpdateSettings(settings);

            return new PersistenceUtility<TEntity>(updatedSettings, _jsonUtility, _entityCache, _logger);
        }

        /// <summary>
        /// Updates the settings to be rooted in the application.
        /// </summary>
        /// <param name="settings">The current settings.</param>
        /// <returns>A <see cref="IPersistenceUtilitySettings"/>.</returns>
        private IPersistenceUtilitySettings UpdateSettings(IPersistenceUtilitySettings settings)
        {
            var baseVirtualPath = $"{_jsonRootPath.TrimEnd('/')}/{settings.BasePath}";

            return new PersistenceUtilitySettings()
            {
                BasePath = _hostingEnvironment.MapPathWebRoot(baseVirtualPath),
                Extension = settings.Extension,
                Wildcard = settings.Wildcard
            };
        }

        /// <summary>
        /// Gets the settings for the incoming type.
        /// </summary>
        /// <returns>A <see cref="IPersistenceUtilitySettings"/>.</returns>
        /// <exception cref="NotSupportedException">If the type provided is not supported.</exception>
        private static IPersistenceUtilitySettings GetSettings<TEntity>()
        {
            var type = typeof(TEntity);

            if (type == typeof(PersistedForm))
            {
                return PersistenceUtilitySettings.Forms;
            }

            if (type == typeof(PersistedDataValues))
            {
                return PersistenceUtilitySettings.DataValues;
            }

            if (type == typeof(PersistedFolder))
            {
                return PersistenceUtilitySettings.Folders;
            }

            if (type == typeof(PersistedLayout))
            {
                return PersistenceUtilitySettings.Layouts;
            }

            if (type == typeof(PersistedValidation))
            {
                return PersistenceUtilitySettings.Validations;
            }

            throw new NotSupportedException($"Entity type {type} has no matching {typeof(IPersistenceUtilitySettings)} settings.");
        }
    }
}
