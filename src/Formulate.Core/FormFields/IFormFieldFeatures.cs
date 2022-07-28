namespace Formulate.Core.FormFields
{
    /// <summary>
    /// A contract to implement features for a Form Field.
    /// </summary>
    /// <remarks>Any feature should be readonly and implemented inline.</remarks>
    public interface IFormFieldFeatures
    {
        /// <summary>
        /// Gets a value indicating whether this field definition is transitory.
        /// </summary>
        bool IsTransitory { get; }

        /// <summary>
        /// Gets a value indicating whether this field definition is server side only.
        /// </summary>
        bool IsServerSideOnly { get; }

        /// <summary>
        /// Gets a value indicating whether this field definition is hidden.
        /// </summary>
        bool IsHidden { get; }

        /// <summary>
        /// Gets a value indicating whether this field definition is stored.
        /// </summary>
        bool IsStored { get; }
    }
}
