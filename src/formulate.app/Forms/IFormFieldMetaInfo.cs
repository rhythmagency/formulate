namespace formulate.app.Forms
{

    // Namespaces.
    using System;

    /// <summary>
    /// The interface for all form field meta information.
    /// </summary>
    public interface IFormFieldMetaInfo
    {
        /// <summary>
        /// Gets or sets the alias.
        /// </summary>
        string Alias { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets the value type.
        /// </summary>
        Type ValueType { get; }
    }
}
