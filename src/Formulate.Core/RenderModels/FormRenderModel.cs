namespace Formulate.Web.RenderModels
{
    using System;
    using System.Collections.Generic;

    public sealed class FormRenderModel
    {
        public FormRenderModel(Guid id, string name, string alias)
        {
            Id = id;
            Name = name;
            Alias = alias;
        }

        public Guid Id { get; set; }

        public string Name { get; init; }

        public string Alias { get; init; }


        public IReadOnlyCollection<FormFieldRenderModel> Fields { get; init; } = Array.Empty<FormFieldRenderModel>();
    }
}