using System;
using formulate.app.Entities;

namespace formulate.app.Helpers
{
    public interface IEntityHelper
    {
        string[] GetClientPath(Guid[] path);
        string GetGroupIconByRoot(Guid id);
        string GetIconForRoot(Guid id);
        string GetNameForRoot(Guid id);
        string GetString(EntityKind kind);
        bool IsRoot(Guid id);
    }
}