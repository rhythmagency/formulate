using System;
using System.Text.Json.Serialization;
using Formulate.BackOffice.Persistence;
using Formulate.Core.Persistence;
using Newtonsoft.Json.Converters;

namespace Formulate.BackOffice.Controllers
{
    public sealed class GetEntityResponse
    {
        public IPersistedEntity Entity { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public EntityTypes EntityType { get; set; }

        public string[] TreePath { get; set; }
    }
}
