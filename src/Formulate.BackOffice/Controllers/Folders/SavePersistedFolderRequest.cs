using System;
using Formulate.BackOffice.Persistence;
using Formulate.Core.Folders;

namespace Formulate.BackOffice.Controllers.Folders
{
    public sealed class SavePersistedFolderRequest
    {
        public PersistedFolder Entity { get; set; }

        public TreeRootTypes TreeType { get; set; }

        public Guid? ParentId { get; set; }
    }
}