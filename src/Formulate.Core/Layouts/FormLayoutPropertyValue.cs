namespace Formulate.Core.Layouts
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// The property value stored by a Form Layout Picker.
    /// </summary>
    [DataContract]
    public sealed class FormLayoutPropertyValue
    {
        [DataMember(Name = "id")]
        public Guid Id { get; set; }
    }
}
