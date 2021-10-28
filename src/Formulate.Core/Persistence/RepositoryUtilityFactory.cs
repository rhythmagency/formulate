using System;
using Formulate.Core.ConfiguredForms;
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
    /// The default implementation of <see cref="IRepositoryUtilityFactory"/>.
    /// </summary>
    internal sealed class RepositoryUtilityFactory : IRepositoryUtilityFactory
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
        /// Initializes a new instance of the <see cref="RepositoryUtilityFactory"/> class.
        /// </summary>
        /// <param name="hostingEnvironment">The hosting environment.</param>
        /// <param name="jsonUtility">The json utility.</param>
        /// <param name="entityCache">The entity cache.</param>
        /// <param name="logger">The logger.</param>
        public RepositoryUtilityFactory(IHostingEnvironment hostingEnvironment, IJsonUtility jsonUtility, IPersistedEntityCache entityCache, ILogger<RepositoryUtilityFactory> logger)
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
        public IRepositoryUtility<TEntity> Create<TEntity>() where TEntity : class, IPersistedEntity
        {
            var settings = GetSettings<TEntity>();
            var updatedSettings = UpdateSettings(settings);

            return new RepositoryUtility<TEntity>(updatedSettings, _jsonUtility, _entityCache, _logger);
        }

        /// <summary>
        /// Updates the settings to be rooted in the application.
        /// </summary>
        /// <param name="settings">The current settings.</param>
        /// <returns>A <see cref="IRepositoryUtilitySettings"/>.</returns>
        private IRepositoryUtilitySettings UpdateSettings(IRepositoryUtilitySettings settings)
        {
            var baseVirtualPath = $"{_jsonRootPath.TrimEnd('/')}/{settings.BasePath}";

            return new RepositoryUtilitySettings()
            {
                BasePath = _hostingEnvironment.MapPathWebRoot(baseVirtualPath),
                Extension = settings.Extension,
                Wildcard = settings.Wildcard
            };
        }

        /// <summary>
        /// Gets the settings for the incoming type.
        /// </summary>
        /// <returns>A <see cref="IRepositoryUtilitySettings"/>.</returns>
        /// <exception cref="NotSupportedException">If the type provided is not supported.</exception>
        private static IRepositoryUtilitySettings GetSettings<TEntity>()
        {
            var type = typeof(TEntity);
            
            if (type == typeof(PersistedConfiguredForm))
            {
                return RepositoryUtilitySettings.ConfiguredForms;
            }

            if (type == typeof(PersistedDataValues))
            {
                return RepositoryUtilitySettings.DataValues;
            }

            if (type == typeof(PersistedForm))
            {
                return RepositoryUtilitySettings.Forms;
            }

            if (type == typeof(PersistedFolder))
            {
                return RepositoryUtilitySettings.Folders;
            }

            if (type == typeof(PersistedLayout))
            {
                return RepositoryUtilitySettings.Layouts;
            }

            if (type == typeof(PersistedValidation))
            {
                return RepositoryUtilitySettings.Validations;
            }

            throw new NotSupportedException($"Entity type {type} has no matching {typeof(IRepositoryUtilitySettings)} settings.");
        }
    }
}
