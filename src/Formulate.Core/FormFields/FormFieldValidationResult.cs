namespace Formulate.Core.FormFields
{
    using System;
    using System.Collections.Generic;

    public sealed class FormFieldValidationResult
    {
        public IReadOnlyCollection<string> ErrorMessages { get; init; } = Array.Empty<string>();
    }
}