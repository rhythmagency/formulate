namespace Formulate.BackOffice.Controllers
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public sealed class DeleteEntityResponse
    {
        [DataMember(Name = "deletedEntityIds")]
        public string[] DeletedEntityIds { get; set; } = Array.Empty<string>();

        [DataMember(Name = "parentPath")]
        public string[] ParentPath { get; set; } = Array.Empty<string>();
    }
}