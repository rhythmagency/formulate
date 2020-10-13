namespace formulate.app.Persistence
{

    // Namespaces.
    using Forms;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Interface for persistence of Forms.
    /// </summary>
    public interface IFormPersistence
    {
        /// <summary>
        /// Persist a Form.
        /// </summary>
        /// <param name="form">
        /// The Form.
        /// </param>
        void Persist(Form form);

        /// <summary>
        /// Delete a Form by ID.
        /// </summary>
        /// <param name="formId">
        /// The Form id.
        /// </param>
        void Delete(Guid formId);

        /// <summary>
        /// Delete a Form by alias.
        /// </summary>
        /// <param name="formAlias">
        /// The Form alias.
        /// </param>
        void Delete(string formAlias);

        /// <summary>
        /// Retrieve a Form by ID.
        /// </summary>
        /// <param name="formId">
        /// The Form id.
        /// </param>
        /// <returns>
        /// A <see cref="Form"/>.
        /// </returns>
        Form Retrieve(Guid formId);

        /// <summary>
        /// Retrieve a Form by alias.
        /// </summary>
        /// <param name="formAlias">
        /// The Form alias.
        /// </param>
        /// <returns>
        /// A <see cref="Form"/>.
        /// </returns>
        Form Retrieve(string formAlias);

        /// <summary>
        /// Retrieve children by their parent ID.
        /// </summary>
        /// <param name="parentId">
        /// The parent id.
        /// </param>
        /// <returns>
        /// If found, a collection of <see cref="Form"/>.
        /// </returns>
        IEnumerable<Form> RetrieveChildren(Guid? parentId);
    }
}
