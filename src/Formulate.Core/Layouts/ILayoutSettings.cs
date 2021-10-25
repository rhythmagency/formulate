using System;
using Formulate.Core.Types;

namespace Formulate.Core.Layouts
{
    public interface ILayoutSettings : IEntitySettings
    {
        string Directive { get; }
    }
}
