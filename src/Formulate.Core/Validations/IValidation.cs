namespace Formulate.Core.Validations
{
    using Formulate.Core.FormFields;
    using Formulate.Core.Submissions.Requests;
    using Microsoft.Extensions.Primitives;
    using System.Collections.Generic;
    // Namespaces.
    using Types;

    /// <summary>
    /// A contract for creating a validation.
    /// </summary>
    public interface IValidation : IEntity
    {
        /// <summary>
        /// Gets or sets the name for this validation.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the raw configuration for this validation.
        /// </summary>
        public string RawConfiguration { get; }

        public IReadOnlyCollection<string> ValidateStrings(StringValues values);

        public IReadOnlyCollection<string> ValidateFiles(IReadOnlyCollection<FormFileValue> values);
    }
}