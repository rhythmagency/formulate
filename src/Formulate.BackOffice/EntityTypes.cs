namespace Formulate.BackOffice
{
    /// <summary>
    /// The entity types used by Formulate in the back office.
    /// </summary>
    public enum EntityTypes
    {
        /// <summary>
        /// An unknown tree entity type.
        /// </summary>
        /// <remarks>
        /// This should never be directly used, and is for unsupported exceptions when something goes wrong.
        /// </remarks>
        Unknown,

        /// <summary>
        /// A data values entity type.
        /// </summary>
        DataValues,

        /// <summary>
        /// A form entity type.
        /// </summary>
        Form,

        /// <summary>
        /// A folder entity type.
        /// </summary>
        Folder,

        /// <summary>
        /// A layout entity type.
        /// </summary>
        Layout,

        /// <summary>
        /// A validation entity type.
        /// </summary>
        Validation
    }
}
