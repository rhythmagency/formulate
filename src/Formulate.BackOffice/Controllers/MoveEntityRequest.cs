namespace Formulate.BackOffice.Controllers
{
    using System;

    public sealed class MoveEntityRequest
    {
        public Guid EntityId { get; set; }

        public Guid? ParentId { get; set; }

        public TreeRootTypes TreeType { get; set; }
    }
}