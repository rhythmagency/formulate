namespace Formulate.BackOffice.EditorModels
{
    using Formulate.Core.Persistence;
    using System.Collections.Generic;
    using Umbraco.Cms.Core.Models.ContentEditing;

    public interface IEditorModel : IPersistedItem
    {
        IReadOnlyCollection<ContentApp> Apps { get; set;  }

        bool IsNew { get; }

        bool IsLegacy { get; }
    }
}
