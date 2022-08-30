namespace Formulate.Core.FormFields.RadioButtonList
{
    // Namespaces.
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Configuration pre-values used by <see cref="RadioButtonListFieldDefinition"/>
    /// for creating <see cref="RadioButtonListField"/>.
    /// </summary>
    [DataContract]
    internal sealed class RadioButtonListFieldPreValues
    {
        /// <summary>
        /// Gets or sets the data value.
        /// </summary>
        [DataMember(Name = "dataValue")]
        public Guid DataValue { get; set; }

        /// <summary>
        /// Gets or sets the orientation.
        /// </summary>
        [DataMember(Name = "orientation")]
        public string Orientation { get; set; }
    }
}
