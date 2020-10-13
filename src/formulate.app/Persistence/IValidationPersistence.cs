namespace formulate.app.Persistence
{

    // Namespaces.
    using System;
    using System.Collections.Generic;
    using Validations;

    /// <summary>
    /// Interface for persistence of Validations.
    /// </summary>
    public interface IValidationPersistence
    {
        /// <summary>
        /// Persist a Validation.
        /// </summary>
        /// <param name="validation">
        /// The Validation.
        /// </param>
        void Persist(Validation validation);

        /// <summary>
        /// Delete a Validation by ID.
        /// </summary>
        /// <param name="validationId">
        /// The Validation id.
        /// </param>
        void Delete(Guid validationId);

        /// <summary>
        /// Delete a Validation by alias.
        /// </summary>
        /// <param name="validationAlias">
        /// The Validation alias.
        /// </param>
        void Delete(string validationAlias);

        /// <summary>
        /// Retrieve a Validation by ID.
        /// </summary>
        /// <param name="validationId">
        /// The Validation id.
        /// </param>
        /// <returns>
        /// A <see cref="Validation"/>.
        /// </returns>
        Validation Retrieve(Guid validationId);

        /// <summary>
        /// Retrieve a Validation by alias.
        /// </summary>
        /// <param name="validationAlias">
        /// The Validation alias.
        /// </param>
        /// <returns>
        /// A <see cref="Validation"/>.
        /// </returns>
        Validation Retrieve(string validationAlias);

        /// <summary>
        /// Retrieve children by their parent ID.
        /// </summary>
        /// <param name="parentId">
        /// The parent id.
        /// </param>
        /// <returns>
        /// If found, a collection of <see cref="Validation"/>.
        /// </returns>
        IEnumerable<Validation> RetrieveChildren(Guid? parentId);
    }
}
