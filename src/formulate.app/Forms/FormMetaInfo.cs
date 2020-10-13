namespace formulate.app.Forms
{

    // Namespaces.
    using System;

    using Newtonsoft.Json;

    /// <summary>
    /// Information attached to a form.
    /// </summary>
    /// <typeparam name="T">The type of data used by this form meta information.</typeparam>
    public class FormMetaInfo<T> : IFormMetaInfo
    {
        #region Properties

        /// <summary>
        /// Gets or sets the alias of this form meta information.
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        /// Gets or sets the name of this form meta information.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the data stored by this form meta information.
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// Gets or sets the type of data stored by this form meta information.
        /// </summary>
        [JsonIgnore]
        public Type ValueType => typeof(T);

        #endregion
    }
}
