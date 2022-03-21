namespace Formulate.BackOffice.ViewModels.Forms
{
    // Namespaces.
    using Newtonsoft.Json.Converters;
    using Persistence;
    using System.Text.Json.Serialization;

    /// <summary>
    /// The response data when requesting a form.
    /// </summary>
    internal class GetFormResponse
    {
        public FormViewModel Entity { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public EntityTypes EntityType { get; set; }

        public string[] TreePath { get; set; }
    }
}