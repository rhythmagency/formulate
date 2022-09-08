namespace Formulate.BackOffice.EditorModels.Templates
{
    using Formulate.Core.Templates;
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public sealed class TemplateEditorModel
    {
        public TemplateEditorModel(ITemplateDefinition definition) : this(definition.KindId, definition.Name)
        {
        }

        public TemplateEditorModel(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        [DataMember(Name = "id")]
        public Guid Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}
