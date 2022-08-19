namespace Formulate.BackOffice.Controllers
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public abstract class GetScaffoldingRequest
    {
        [DataMember(Name = "entityType")]
        public EntityTypes EntityType { get; set; }

        [DataMember(Name = "parentId")]
        public Guid? ParentId { get; set; }
    }
}
