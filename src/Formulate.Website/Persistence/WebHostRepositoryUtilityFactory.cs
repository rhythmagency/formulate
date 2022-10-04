using Formulate.Core.DataValues;
using Formulate.Core.Folders;
using Formulate.Core.Forms;
using Formulate.Core.Layouts;
using Formulate.Core.Persistence;
using Formulate.Core.Utilities;
using Formulate.Core.Validations;
using Microsoft.Extensions.Logging;

using Microsoft.AspNetCore.Hosting;

using Umbraco.Extensions;

namespace Formulate.Website.Persistence
{
    /// <summary>
    /// The default implementation of <see cref="IRepositoryUtilityFactory"/> which uses the <see cref="IWebHostEnvironment"/>.
    /// </summary>
    internal sealed class WebHostRepositoryUtilityFactory : IRepositoryUtilityFactory
    {
        /// <summary>
        /// The web host environment.
        /// </summary>
        private readonly IWebHostEnvironment _webHostEnvironment;

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
        /// Initializes a new instance of the <see cref="WebHostRepositoryUtilityFactory"/> class.
        /// </summary>
        /// <param name="webHostEnvironment">The web host environment.</param>
        /// <param name="jsonUtility">The json utility.</param>
        /// <param name="entityCache">The entity cache.</param>
        /// <param name="logger">The logger.</param>
        public WebHostRepositoryUtilityFactory(IWebHostEnvironment webHostEnvironment, IJsonUtility jsonUtility, IPersistedEntityCache entityCache, ILogger<WebHostRepositoryUtilityFactory> logger)
        {
            _webHostEnvironment = webHostEnvironment;
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

            return new WebHostRepositoryUtility<TEntity>(updatedSettings, _jsonUtility, _entityCache, _logger);
        }

        /// <summary>
        /// Updates the settings to be rooted in the application.
        /// </summary>
        /// <param name="settings">The current settings.</param>
        /// <returns>A <see cref="WebHostRepositoryUtilitySettings"/>.</returns>
        private WebHostRepositoryUtilitySettings UpdateSettings(WebHostRepositoryUtilitySettings settings)
        {
            var baseVirtualPath = $"{_jsonRootPath.TrimEnd('/')}/{settings.BasePath}";
            var mappedBasePath = _webHostEnvironment.MapPathWebRoot(baseVirtualPath.ToString());

            return new WebHostRepositoryUtilitySettings(mappedBasePath, settings.Extension, settings.Wildcard);
        }

        /// <summary>
        /// Gets the settings for the incoming type.
        /// </summary>
        /// <returns>A <see cref="WebHostRepositoryUtilitySettings"/>.</returns>
        /// <exception cref="NotSupportedException">If the type provided is not supported.</exception>
        private static WebHostRepositoryUtilitySettings GetSettings<TEntity>()
        {
            var type = typeof(TEntity);
            
            if (type == typeof(PersistedDataValues))
            {
                return WebHostRepositoryUtilitySettings.DataValues;
            }

            if (type == typeof(PersistedForm))
            {
                return WebHostRepositoryUtilitySettings.Forms;
            }

            if (type == typeof(PersistedFolder))
            {
                return WebHostRepositoryUtilitySettings.Folders;
            }

            if (type == typeof(PersistedLayout))
            {
                return WebHostRepositoryUtilitySettings.Layouts;
            }

            if (type == typeof(PersistedValidation))
            {
                return WebHostRepositoryUtilitySettings.Validations;
            }

            throw new NotSupportedException($"Entity type {type} has no matching {typeof(WebHostRepositoryUtilitySettings)} settings.");
        }
    }
}
