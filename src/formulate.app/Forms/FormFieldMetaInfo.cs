namespace formulate.app.Forms
{

    // Namespaces.
    using System;

    using Newtonsoft.Json;

    /// <summary>
    /// Information attached to a form field.
    /// </summary>
    /// <typeparam name="T">The type of data used by this field meta information.</typeparam>
    public class FormFieldMetaInfo<T> : IFormFieldMetaInfo
    {
        #region Properties

        /// <summary>
        /// Gets or sets the alias of this field meta information.
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        /// Gets or sets the name of this field meta information.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the data stored by this form field meta information.
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// Gets or sets the type of data stored by this field meta information.
        /// </summary>
        [JsonIgnore]
        public Type ValueType => typeof(T);

        #endregion
    }
}
