using System.Collections.Generic;
using Formulate.Core.Validations;

namespace Formulate.Core.FormFields.Upload
{
    /// <summary>
    /// An upload field.
    /// </summary>
    public sealed class UploadField : FormField
    {
        /// <inheritdoc />
        public UploadField(IFormFieldSettings settings) : base(settings)
        {
        }

        /// <inheritdoc />
        public UploadField(IFormFieldSettings settings, IReadOnlyCollection<IValidation> validations) : base(settings, validations)
        {
        }
    }
}
