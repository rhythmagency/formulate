namespace Formulate.BackOffice.Controllers
{
    using System.Text.Json.Serialization;
    using Formulate.BackOffice.EditorModels;
    using Formulate.BackOffice.Persistence;
    using Newtonsoft.Json.Converters;

    public sealed class GetEditorModelResponse
    {
        public IEditorModel Entity { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public EntityTypes EntityType { get; set; }

        public string[] TreePath { get; set; }
    }
}
