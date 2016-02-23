namespace formulate.app.Persistence
{

    // Namespaces.
    using Forms;
    using System;
    using System.Collections.Generic;


    /// <summary>
    /// Interface for persistence of configured forms.
    /// </summary>
    public interface IConfiguredFormPersistence
    {
        void Persist(ConfiguredForm configuredForm);
        void Delete(Guid configuredFormId);
        void Delete(string configuredFormAlias);
        ConfiguredForm Retrieve(Guid configuredFormId);
        ConfiguredForm Retrieve(string configuredFormAlias);
        IEnumerable<ConfiguredForm> RetrieveChildren(Guid parentId);
    }

}