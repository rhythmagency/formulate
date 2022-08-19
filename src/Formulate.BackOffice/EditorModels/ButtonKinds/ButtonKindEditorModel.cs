namespace Formulate.BackOffice.EditorModels.ButtonKinds
{
    using System.Runtime.Serialization;

    [DataContract]
    public sealed class ButtonKindEditorModel
    {
        public ButtonKindEditorModel(string kind)
        {
            Kind = kind;
        }

        [DataMember(Name = "kind")]
        public string Kind { get; set; }
    }
}
