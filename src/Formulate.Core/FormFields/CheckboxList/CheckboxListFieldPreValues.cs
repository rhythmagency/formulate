namespace Formulate.Core.FormFields.CheckboxList
{
    // Namespaces.
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Configuration pre-values used by <see cref="CheckboxListFieldDefinition"/>
    /// for creating <see cref="CheckboxListField"/>.
    /// </summary>
    [DataContract]
    internal sealed class CheckboxListFieldPreValues
    {
        /// <summary>
        /// Gets or sets the data value.
        /// </summary>
        [DataMember(Name = "dataValue")]
        public Guid DataValue { get; set; }
    }
}
