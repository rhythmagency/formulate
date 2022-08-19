namespace Formulate.BackOffice.EditorModels.Forms
{
    using System.Runtime.Serialization;

    [DataContract]
    public sealed class FormFieldCategoryEditorModel
    {
        public FormFieldCategoryEditorModel(string kind, string group)
        {
            Label = kind;
            Value = kind;
            Group = group;
        }

        [DataMember(Name = "label")]
        public string Label { get; set; }


        [DataMember(Name = "value")]
        public string Value { get; set; }


        [DataMember(Name = "group")]
        public string Group { get; set; }
    }
}
