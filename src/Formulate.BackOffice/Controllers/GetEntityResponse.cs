namespace Formulate.BackOffice.Controllers
{
    using System.Text.Json.Serialization;
    using Formulate.BackOffice.Persistence;
    using Formulate.Core.Persistence;
    using Newtonsoft.Json.Converters;

    public sealed class GetEntityResponse
    {
        public IPersistedEntity Entity { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public EntityTypes EntityType { get; set; }

        public string[] TreePath { get; set; }
    }
}
