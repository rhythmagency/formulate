using Formulate.Core.Forms;

namespace Formulate.BackOffice.Definitions.Forms
{
    using System;

    public abstract class FormDefinition : IFormDefinition
    {
        public virtual string Icon => Constants.Icons.Entities.Form; 

        public virtual string Description => string.Empty;

        public abstract Guid KindId { get; }

        public abstract string Name { get; }

        public string Directive => string.Empty;

        public virtual bool IsLegacy => false;

        public virtual int SortOrder => 0;

        public virtual void PostSave(PersistedForm form)
        {
        }
    }
}