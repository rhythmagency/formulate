﻿using System.Threading;
using System.Threading.Tasks;
using Formulate.Core.Types;

namespace Formulate.Core.FormFields
{
    /// <summary>
    /// A contract for implementing a form field definition.
    /// </summary>
    public interface IFormFieldDefinition : IDefinition
    {
        /// <summary>
        /// Gets the icon.
        /// </summary>
        string Icon { get; }
        
        /// <summary>
        /// Gets a value indicating whether this field definition is transitory.
        /// </summary>
        bool IsTransitory { get; }

        /// <summary>
        /// Gets a value indicating whether this field definition is server side only.
        /// </summary>
        bool IsServerSideOnly { get; }

        /// <summary>
        /// Gets a value indicating whether this field definition is hidden.
        /// </summary>
        bool IsHidden { get; }

        /// <summary>
        /// Gets a value indicating whether this field definition is stored.
        /// </summary>
        bool IsStored { get; }

        /// <summary>
        /// Asynchronously creates a new instance of a <see cref="IFormField"/>.
        /// </summary>
        /// <param name="settings">The current form field settings.</param>
        /// <param name="cancellationToken">The optional cancellation token to cancel the operation.</param>
        /// <returns>A <see cref="IFormField"/>.</returns>
        Task<IFormField> CreateFieldAsync(IFormFieldSettings settings, CancellationToken cancellationToken = default);
    }
}