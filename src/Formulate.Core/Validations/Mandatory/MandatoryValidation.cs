namespace Formulate.Core.Validations.Mandatory
{
    using Formulate.Core.Submissions.Requests;
    using Microsoft.Extensions.Primitives;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A mandatory validation.
    /// </summary>
    public sealed class MandatoryValidation : Validation<MandatoryValidationConfiguration>
    {
        /// <inheritdoc />
        public MandatoryValidation(IValidationSettings settings, MandatoryValidationConfiguration configuration) : base(settings, configuration)
        {
        }

        public override IReadOnlyCollection<string> ValidateStrings(StringValues values)
        {
            if (Configuration.ClientSideOnly)
            {
                return Array.Empty<string>();
            }

            if (string.IsNullOrWhiteSpace(values))
            {
                return new[] { Configuration.Message };
            }

            return Array.Empty<string>();
        }

        public override IReadOnlyCollection<string> ValidateFiles(IReadOnlyCollection<FormFileValue> values)
        {
            if (Configuration.ClientSideOnly)
            {
                return Array.Empty<string>();
            }

            if (values is null || values.Count == 0)
            {
                return new[] { Configuration.Message };
            }

            return Array.Empty<string>();
        }
    }
}
