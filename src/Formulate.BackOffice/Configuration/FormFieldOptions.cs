namespace Formulate.BackOffice.Configuration
{
    using System;

    public sealed class FormFieldOptions
    {
        public const string SectionName = "Formulate:Fields";

        public FormFieldCategory[] Categories { get; set; } = Array.Empty<FormFieldCategory>();
    }
}
