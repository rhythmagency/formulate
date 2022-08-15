namespace Formulate.Core.Layouts.Basic
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// A field in a basic layout row cell.
    /// </summary>
    [DataContract]
    public sealed class BasicLayoutField
    {
        /// <summary>
        /// Gets the field ID.
        /// </summary>
        [DataMember(Name = "id")]
        public Guid Id { get; set; }
    }
}
