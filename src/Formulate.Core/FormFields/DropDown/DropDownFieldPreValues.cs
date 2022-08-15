namespace Formulate.Core.FormFields.DropDown
{
    // Namespaces.
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Configuration pre-values used by <see cref="DropDownFieldDefinition"/>
    /// for creating <see cref="DropDownField"/>.
    /// </summary>
    [DataContract]
    internal sealed class DropDownFieldPreValues
    {
        /// <summary>
        /// Gets or sets the data value.
        /// </summary>
        [DataMember(Name = "dataValue")]
        public Guid DataValue { get; set; }
    }
}
