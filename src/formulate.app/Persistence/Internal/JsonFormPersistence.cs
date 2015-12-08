namespace formulate.app.Persistence.Internal
{

    // Namespaces.
    using System;
    using Forms;


    //TODO: Need to implement this class.
    /// <summary>
    /// Handles persistence of forms to JSON on the file system.
    /// </summary>
    internal class JsonFormPersistence : IFormPersistence
    {
        public void Persist(Form form)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid formId)
        {
            throw new NotImplementedException();
        }

        public void Delete(string formAlias)
        {
            throw new NotImplementedException();
        }

        public Form Retrieve(Guid formId)
        {
            throw new NotImplementedException();
        }

        public Form Retrieve(string formAlias)
        {
            throw new NotImplementedException();
        }
    }

}