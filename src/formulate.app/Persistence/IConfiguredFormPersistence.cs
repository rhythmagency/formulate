namespace formulate.app.Persistence
{

    // Namespaces.
    using Forms;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Interface for persistence of Configured Forms.
    /// </summary>
    public interface IConfiguredFormPersistence
    {
        /// <summary>
        /// Persist a Configured Form.
        /// </summary>
        /// <param name="configuredForm">The Configured Form.</param>
        void Persist(ConfiguredForm configuredForm);

        /// <summary>
        /// Delete a Configured Form by ID.
        /// </summary>
        /// <param name="configuredFormId">The Configured Form ID.</param>
        void Delete(Guid configuredFormId);

        /// <summary>
        /// Delete a Configured Form by alias.
        /// </summary>
        /// <param name="configuredFormAlias">The Configured Form alias.</param>
        void Delete(string configuredFormAlias);

        /// <summary>
        /// Retrieve a Configured Form by ID.
        /// </summary>
        /// <param name="configuredFormId">The Configured Form ID.</param>
        /// <returns>If found a <see cref="ConfiguredForm"/>.</returns>
        ConfiguredForm Retrieve(Guid configuredFormId);

        /// <summary>
        /// Retrieve a Configured Form by alias.
        /// </summary>
        /// <param name="configuredFormAlias">The Configured Form alias.</param>
        /// <returns>If found a <see cref="ConfiguredForm"/>.</returns>
        ConfiguredForm Retrieve(string configuredFormAlias);

        /// <summary>
        /// Retrieve children by their parent ID.
        /// </summary>
        /// <param name="parentId">
        /// The parent id.
        /// </param>
        /// <returns>
        /// If found a collection of <see cref="ConfiguredForm"/>s.
        /// </returns>
        IEnumerable<ConfiguredForm> RetrieveChildren(Guid parentId);
    }
}
