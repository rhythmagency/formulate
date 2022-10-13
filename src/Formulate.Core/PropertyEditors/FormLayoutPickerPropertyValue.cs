namespace Formulate.Core.PropertyEditors
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// The property value stored by a Form Layout Picker.
    /// </summary>
    [DataContract]
    public sealed class FormLayoutPickerPropertyValue
    {
        [DataMember(Name = "id")]
        public Guid Id { get; set; }
    }
}
