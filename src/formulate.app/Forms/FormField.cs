namespace formulate.app.Forms
{

    // Namespaces.
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Stores information about a form field.
    /// </summary>
    public sealed class FormField : IFormField
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormField"/> class.
        /// </summary>
        /// <param name="formFieldType">
        /// The form field type.
        /// </param>
        public FormField(IFormFieldType formFieldType)
        {
            FormFieldType = formFieldType;
        }

        #region Properties

        /// <summary>
        /// Gets or sets the unique ID of the field.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the alias of the field.
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        /// Gets or sets the name of the field.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the label for the field.
        /// </summary>
        public string Label { get; set; }
        
        /// <summary>
        /// Gets or sets the category of the field.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the validations for this field.
        /// </summary>
        public Guid[] Validations { get; set; }

        /// <summary>
        /// Gets or sets information about the field.
        /// </summary>
        public IFormFieldMetaInfo[] MetaInfo { get; set; }

        /// <summary>
        /// Gets or sets the configuration data stored by the field.
        /// </summary>
        public string FieldConfiguration { get; set; }

        /// <summary>
        /// Gets or sets the ID of the field type.
        /// </summary>
        public Guid TypeId
        {
            get
            {
                return FormFieldType.TypeId;
            }
            set
            {
            }
        }

        /// <summary>
        /// Gets a value indicating whether is this type of field persistent or transitory?
        /// </summary>
        public bool IsTransitory
        {
            get
            {
                var casted = FormFieldType as IFormFieldTypeExtended;
                return casted == null
                    ? false
                    : casted.IsTransitory;
            }
        }

        /// <summary>
        /// Gets a value indicating whether is this type of field server-side only (i.e., not a frontend field)?
        /// </summary>
        public bool IsServerSideOnly
        {
            get
            {
                var casted = FormFieldType as IFormFieldTypeExtended;
                return casted == null
                    ? false
                    : casted.IsServerSideOnly;
            }
        }

        /// <summary>
        /// Gets a value indicating whether is this type of field hidden?
        /// </summary>
        public bool IsHidden
        {
            get
            {
                var casted = FormFieldType as IFormFieldTypeExtended;
                return casted == null
                    ? false
                    : casted.IsHidden;
            }
        }

        /// <summary>
        /// Gets a value indicating whether is this type of field stored?
        /// </summary>
        public bool IsStored
        {
            get
            {
                var casted = FormFieldType as IFormFieldTypeExtended;
                return casted == null
                    ? true
                    : casted.IsStored;
            }
        }

        /// <summary>
        /// Gets or sets the form field type.
        /// </summary>
        private IFormFieldType FormFieldType { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the directive to use for this form field.
        /// </summary>
        /// <returns>The directive.</returns>
        public string GetDirective()
        {
            return FormFieldType.Directive;
        }

        /// <summary>
        /// Gets the type label to use for this form field.
        /// </summary>
        /// <returns>The type label.</returns>
        public string GetTypeLabel()
        {
            return FormFieldType.TypeLabel;
        }

        /// <summary>
        /// Gets the icon to use for this form field.
        /// </summary>
        /// <returns>The icon.</returns>
        public string GetIcon()
        {
            return FormFieldType.Icon;
        }

        /// <summary>
        /// Returns the type of field.
        /// </summary>
        /// <returns>
        /// The field type.
        /// </returns>
        public Type GetFieldType()
        {
            return FormFieldType.GetType();
        }

        /// <summary>
        /// Deserializes the field configuration into a .NET object instance.
        /// </summary>
        /// <returns>
        /// The deserialized field configuration.
        /// </returns>
        public object DeserializeConfiguration()
        {
            return FormFieldType.DeserializeConfiguration(FieldConfiguration);
        }

        /// <summary>
        /// Formats a value in the specified field presentation format.
        /// </summary>
        /// <param name="values">
        /// The values to format.
        /// </param>
        /// <param name="format">
        /// The format to present the value in.
        /// </param>
        /// <returns>
        /// The formatted value.
        /// </returns>
        public string FormatValue(IEnumerable<string> values, FieldPresentationFormats format)
        {
            var configuration = FormFieldType.DeserializeConfiguration(FieldConfiguration);
            return FormFieldType.FormatValue(values ?? new string[0], format, configuration);
        }

        /// <summary>
        /// Is the field value valid?
        /// </summary>
        /// <param name="value">
        /// The value submitted with the form.
        /// </param>
        /// <returns>
        /// True, if the value is valid; otherwise, false.
        /// </returns>
        public bool IsValid(IEnumerable<string> value)
        {
            if (FormFieldType is IFormFieldTypeExtended)
            {
                var casted = FormFieldType as IFormFieldTypeExtended;
                return casted.IsValid(value);
            }
            else
            {
                return true;
            }
        }

        #endregion
    }
}