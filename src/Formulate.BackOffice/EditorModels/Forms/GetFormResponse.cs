namespace Formulate.BackOffice.EditorModels.Forms
{
    // Namespaces.
    using Newtonsoft.Json.Converters;
    using Persistence;
    using System.Text.Json.Serialization;

    /// <summary>
    /// The response data when requesting a form.
    /// </summary>
    public sealed class GetFormResponse
    {
        public FormEditorModel Entity { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public EntityTypes EntityType { get; set; }

        public string[] TreePath { get; set; }
    }
}