namespace formulate.app.Persistence
{

    // Namespaces.
    using Forms;
    using System;


    /// <summary>
    /// Interface for persistence of forms.
    /// </summary>
    public interface IFormPersistence
    {
        void Persist(Form form);
        void Delete(Guid formId);
        void Delete(string formAlias);
        void Retrieve(Guid formId);
        void Retrieve(string formAlias);
    }

}