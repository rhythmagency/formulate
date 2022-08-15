namespace Formulate.Core.ConfiguredForms
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// The property value stored by a Confirmed Form Picker.
    /// </summary>
    [DataContract]
    public sealed class ConfiguredFormPropertyValue
    {
        [DataMember(Name = "id")]
        public Guid Id { get; set; }
    }
}
