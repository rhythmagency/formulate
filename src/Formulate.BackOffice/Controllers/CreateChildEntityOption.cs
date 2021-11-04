using System;
using System.Text.Json.Serialization;
using Formulate.BackOffice.Persistence;
using Newtonsoft.Json.Converters;

namespace Formulate.BackOffice.Controllers
{
    public sealed class CreateChildEntityOption
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public EntityTypes EntityType { get; set; }

        public string Icon { get; set; }

        public Guid? DefinitionId { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }
    }
}