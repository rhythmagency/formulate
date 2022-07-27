namespace Formulate.Core.PropertyValueConverters
{
    using Formulate.Core.ConfiguredForms;
    using Formulate.Core.Utilities;
    using System;
    using Umbraco.Cms.Core.Models.PublishedContent;
    using Umbraco.Cms.Core.PropertyEditors;
    using Umbraco.Extensions;

    public sealed class ConfiguredFormPropertyValueConverter : PropertyValueConverterBase
    {
        private readonly IConfiguredFormEntityRepository _configuredFormEntityRepository;

        private readonly IJsonUtility _jsonUtility;

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ConfiguredFormPropertyValueConverter(IConfiguredFormEntityRepository configuredFormEntityRepository, IJsonUtility jsonUtility)
        {
            _configuredFormEntityRepository = configuredFormEntityRepository;
            _jsonUtility = jsonUtility;
        }

        #endregion


        #region Methods

        public override Type GetPropertyValueType(IPublishedPropertyType propertyType)
        {
            return typeof(ConfiguredForm);
        }

        public override object ConvertIntermediateToObject(IPublishedElement owner, IPublishedPropertyType propertyType, PropertyCacheLevel cacheLevel, object source, bool preview)
        {
            if (source is string stringSource)
            {
                var deserializedSource = _jsonUtility.Deserialize<ConfiguredFormPropertyValue>(stringSource);

                return ConvertPickedConfiguredFormToObject(deserializedSource);
            }

            if (source is ConfiguredFormPropertyValue pickedSource)
            {
                return ConvertPickedConfiguredFormToObject(pickedSource);
            }

            return default;
        }

        private ConfiguredForm ConvertPickedConfiguredFormToObject(ConfiguredFormPropertyValue propertyValue)
        {
            if (propertyValue is null)
            {
                return default;
            }

            var entity = _configuredFormEntityRepository.Get(propertyValue.Id);

            if (entity is null)
            {
                return default;
            }

            // Return configuration.
            return new ConfiguredForm()
            {
                Configuration = entity.Path[^1],
                FormId = entity.Path[^2],
                LayoutId = entity.LayoutId,
                TemplateId = entity.TemplateId
            };
        }

        public override bool IsConverter(IPublishedPropertyType propertyType)
        {
            return "Formulate.ConfiguredFormPicker".InvariantEquals(propertyType.EditorAlias);
        }

        #endregion
    }
}
