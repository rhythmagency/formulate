namespace formulate.app.Forms
{

    // Namespaces.
    using System;


    /// <summary>
    /// Stores information about a form field.
    /// </summary>
    /// <typeparam name="T">The type of data stored by this form field.</typeparam>
    public class FormField<T> : IFormField
        where T : IFormFieldType,
        new()
    {

        #region Properties

        /// <summary>
        /// The unique ID of the field.
        /// </summary>
        public Guid Id { get; set; }


        /// <summary>
        /// The alias of the field.
        /// </summary>
        public string Alias { get; set; }


        /// <summary>
        /// The name of the field.
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// The label for the field.
        /// </summary>
        public string Label { get; set; }


        /// <summary>
        /// The validations for this field.
        /// </summary>
        public Guid[] Validations { get; set; }


        /// <summary>
        /// Information about the field.
        /// </summary>
        public IFormFieldMetaInfo[] MetaInfo { get; set; }


        /// <summary>
        /// The configuration data stored by the field.
        /// </summary>
        public string FieldConfiguration { get; set; }


        /// <summary>
        /// Gets the directive to use for this form field.
        /// </summary>
        /// <returns>The directive.</returns>
        public string GetDirective()
        {
            var instance = new T();
            return instance.Directive;
        }


        /// <summary>
        /// Gets the type label to use for this form field.
        /// </summary>
        /// <returns>The type label.</returns>
        public string GetTypeLabel()
        {
            var instance = new T();
            return instance.TypeLabel;
        }


        /// <summary>
        /// Gets the icon to use for this form field.
        /// </summary>
        /// <returns>The icon.</returns>
        public string GetIcon()
        {
            var instance = new T();
            return instance.Icon;
        }


        /// <summary>
        /// Returns the type of field.
        /// </summary>
        /// <returns>
        /// The field type.
        /// </returns>
        public Type GetFieldType()
        {
            return typeof(T);
        }


        /// <summary>
        /// Deserializes the field configuration into a .NET object instance.
        /// </summary>
        /// <returns>
        /// The deserialized field configuration.
        /// </returns>
        public object DeserializeConfiguration()
        {
            var instance = new T();
            return instance.DeserializeConfiguration(FieldConfiguration);
        }


        /// <summary>
        /// The ID of the field type.
        /// </summary>
        public Guid TypeId
        {
            get
            {
                var instance = new T();
                return instance.TypeId;
            }
            set
            {
            }
        }

        #endregion

    }

}