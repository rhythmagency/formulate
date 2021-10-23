using System;
using Formulate.Core.Types;

namespace Formulate.Core.FormHandlers
{
    /// <summary>
    /// The default implementation of <see cref="IFormHandlerFactory"/> using the <see cref="FormHandlerTypeCollection"/>.
    /// </summary>
    internal sealed class FormHandlerFactory : IFormHandlerFactory
    {
        /// <summary>
        /// The form handler types.
        /// </summary>
        private readonly FormHandlerTypeCollection formHandlerTypes;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="formHandlerTypes">The form handler types.</param>
        public FormHandlerFactory(FormHandlerTypeCollection formHandlerTypes)
        {
            this.formHandlerTypes = formHandlerTypes;
        }

        /// <inheritdoc />
        /// <exception cref="ArgumentNullException">The provided settings are null.</exception>
        /// <exception cref="NotSupportedException">The matched form type handler is not supported.</exception>
        public IFormHandler CreateHandler(IFormHandlerSettings settings)
        {
            if (settings is null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            var foundFormHandlerType = formHandlerTypes.FirstOrDefault(settings.TypeId);

            if (foundFormHandlerType is null)
            {
                return default;
            }

            if (foundFormHandlerType is AsyncFormHandlerType asyncFormHandlerType)
            {
                return asyncFormHandlerType.CreateAsyncHandler(settings);
            }

            if (foundFormHandlerType is FormHandlerType formHandlerType)
            {
                return formHandlerType.CreateHandler(settings);
            }

            throw new NotSupportedException($"{foundFormHandlerType} does not match a valid form handler type.");
        }
    }
}
