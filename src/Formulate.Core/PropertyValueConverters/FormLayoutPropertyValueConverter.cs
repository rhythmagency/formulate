namespace Formulate.Core.PropertyValueConverters
{
    using Formulate.Core.Layouts;
    using Formulate.Core.Persistence;
    using Formulate.Core.Utilities;
    using System;
    using Umbraco.Cms.Core.Models.PublishedContent;
    using Umbraco.Cms.Core.PropertyEditors;
    using Umbraco.Extensions;

    public sealed class FormLayoutPropertyValueConverter : PropertyValueConverterBase
    {
        private readonly IJsonUtility _jsonUtility;

        private readonly ILayoutEntityRepository _layoutEntityRepository;

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public FormLayoutPropertyValueConverter(ILayoutEntityRepository layoutEntityRepository, IJsonUtility jsonUtility)
        {
            _layoutEntityRepository = layoutEntityRepository;
            _jsonUtility = jsonUtility;
        }

        #endregion


        #region Methods

        public override Type GetPropertyValueType(IPublishedPropertyType propertyType)
        {
            return typeof(FormLayout);
        }

        public override object ConvertIntermediateToObject(IPublishedElement owner, IPublishedPropertyType propertyType, PropertyCacheLevel cacheLevel, object source, bool preview)
        {
            if (source is string stringSource)
            {
                var deserializedSource = _jsonUtility.Deserialize<FormLayoutPropertyValue>(stringSource);

                return ConvertPickedFormLayoutToObject(deserializedSource);
            }

            if (source is FormLayoutPropertyValue pickedSource)
            {
                return ConvertPickedFormLayoutToObject(pickedSource);
            }

            return default;
        }

        private FormLayout ConvertPickedFormLayoutToObject(FormLayoutPropertyValue propertyValue)
        {
            if (propertyValue is null)
            {
                return default;
            }

            var entity = _layoutEntityRepository.Get(propertyValue.Id);

            if (entity is null)
            {
                return default;
            }

            var formId = entity.ParentId();

            if (formId.HasValue == false)
            {
                return default;
            }

            // Return configuration.
            return new FormLayout()
            {
                FormId = formId.Value,
                LayoutId = entity.Id,
            };
        }

        public override bool IsConverter(IPublishedPropertyType propertyType)
        {
            return "Formulate.FormLayoutPicker".InvariantEquals(propertyType.EditorAlias);
        }

        #endregion
    }
}
