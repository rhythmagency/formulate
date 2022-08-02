namespace Formulate.BackOffice.Controllers
{
    using Formulate.BackOffice.Persistence;
    using System;
    using System.Text.Json.Serialization;

    public abstract class GetScaffoldingRequest
    {
        [JsonPropertyName("entityType")]
        public EntityTypes EntityType { get; set; }

        [JsonPropertyName("parentId")]
        public Guid? ParentId { get; set; }
    }
}
