using System;
using Formulate.Core.Persistence;

namespace Formulate.Core.Folders
{
    public sealed class PersistedFolder : IPersistedEntity
    {
        public Guid Id { get; set; }
        public Guid[] Path { get; set; }
        public string Name { get; set; }
    }
}
