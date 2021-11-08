using System;
using Formulate.BackOffice.Persistence;

namespace Formulate.BackOffice.Controllers
{
    public sealed class MoveEntityRequest
    {
        public Guid EntityId { get; set; }

        public Guid? ParentId { get; set; }

        public TreeRootTypes TreeType { get; set; }
    }
}