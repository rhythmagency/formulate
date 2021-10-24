using System;
using Formulate.Core.Types;

namespace Formulate.Core.FormFields
{
    /// <summary>
    /// The default implementation of <see cref="IFormFieldFactory"/> using the <see cref="FormFieldTypeCollection"/>.
    /// </summary>
    public sealed class FormFieldFactory : IFormFieldFactory
    {
        /// <summary>
        /// The form field types.
        /// </summary>
        private readonly FormFieldTypeCollection formFieldTypes;

        /// <summary>
        /// Initializes a new instance of the <see cref="FormFieldFactory"/> class.
        /// </summary>
        /// <param name="formFieldTypes">The form field types.</param>
        public FormFieldFactory(FormFieldTypeCollection formFieldTypes)
        {
            this.formFieldTypes = formFieldTypes;
        }

        /// <inheritdoc />
        /// <exception cref="ArgumentNullException">The provided settings are null.</exception>
        public IFormField CreateField(IFormFieldSettings settings)
        {
            if (settings is null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            var foundFormFieldType = formFieldTypes.FirstOrDefault(settings.TypeId);

            return foundFormFieldType?.CreateField(settings);
        }
    }
}