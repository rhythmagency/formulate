using System;
using Formulate.Core.Validations;

namespace Formulate.BackOffice.Controllers.Validations
{
    public sealed class SavePersistedValidationRequest
    {
        public PersistedValidation Entity { get; set; }
        public Guid? ParentId { get; set; }
    }
}