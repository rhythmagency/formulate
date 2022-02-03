using System;
using Formulate.Core.DataValues;

namespace Formulate.BackOffice.Controllers.DataValues
{
    public sealed class SavePersistedDataValuesRequest
    {
        public PersistedDataValues Entity { get; set; }
        public Guid? ParentId { get; set; }
    }
}