using System;
using System.Runtime.Serialization;
using Formulate.BackOffice.Persistence;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Formulate.BackOffice.Controllers
{
    [DataContract]
    public sealed class CreateChildEntityOption
    {
        [DataMember(Name = "entityType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public EntityTypes EntityType { get; set; }

        [DataMember(Name = "icon")]
        public string Icon { get; set; }

        [DataMember(Name = "kindId")]
        public Guid? KindId { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}