﻿using System;
using Formulate.Core.Types;

namespace Formulate.Core.FormHandlers
{
    /// <summary>
    /// The default implementation of <see cref="IFormHandlerFactory"/> using the <see cref="FormHandlerDefinitionCollection"/>.
    /// </summary>
    internal sealed class FormHandlerFactory : IFormHandlerFactory
    {
        /// <summary>
        /// The form handler definitions.
        /// </summary>
        private readonly FormHandlerDefinitionCollection _formHandlerDefinitions;

        /// <summary>
        /// Initializes a new instance of the <see cref="FormHandlerFactory"/> class.
        /// </summary>
        /// <param name="formHandlerDefinitions">The form handler definitions.</param>
        public FormHandlerFactory(FormHandlerDefinitionCollection formHandlerDefinitions)
        {
            _formHandlerDefinitions = formHandlerDefinitions;
        }

        /// <inheritdoc />
        /// <exception cref="ArgumentNullException">The provided settings are null.</exception>
        /// <exception cref="NotSupportedException">The matched form definition handler is not supported.</exception>
        public IFormHandler Create(IFormHandlerSettings settings)
        {
            if (settings is null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            var foundFormHandlerDefinition = _formHandlerDefinitions.FirstOrDefault(settings.KindId);

            if (foundFormHandlerDefinition is null)
            {
                return default;
            }

            // Create an instance of the form handler.
            var handler = foundFormHandlerDefinition.CreateHandler(settings);

            // Set the attributes on the form handler that can be obtained from
            // the form handler definition (namely, icon and directive.
            handler.Icon = foundFormHandlerDefinition.Icon;
            handler.Directive = foundFormHandlerDefinition.Directive;

            // Return the form handler instance.
            return handler;
        }
    }
}
