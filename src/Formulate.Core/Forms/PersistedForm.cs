using System;
using Formulate.Core.FormFields;
using Formulate.Core.FormHandlers;
using Formulate.Core.Persistence;

namespace Formulate.Core.Forms
{
    public sealed class PersistedForm : IPersistedEntity
    {
        public Guid Id { get; set; }

        public Guid[] Path { get; set; }
        
        public string Name { get; set; }

        public string Alias { get; set; }
        
        public PersistedFormField[] Fields { get; set; }
        
        public PersistedFormHandler[] Handlers { get; set; }
    }
}
