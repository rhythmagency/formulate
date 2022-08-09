namespace Formulate.BackOffice.Controllers
{
    using Formulate.BackOffice.Persistence;
    using Newtonsoft.Json;
    using System;
    using System.Text.Json.Serialization;

    public abstract class GetScaffoldingRequest
    {
        [JsonProperty("entityType")]
        public EntityTypes EntityType { get; set; }

        [JsonProperty("parentId")]
        public Guid? ParentId { get; set; }
    }
}
