namespace formulate.app.Persistence.Internal
{

    // Namespaces.
    using Forms;
    using System;
    using System.Collections.Generic;


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
            //TODO: Testing.
            return new Form()
            {
                Fields = new List<IFormField>()
                {
                    new FormField<string>()
                    {
                        Alias = "firstName",
                        Id = Guid.NewGuid(),
                        Name = "First Name"
                    },
                    new FormField<string>()
                    {
                        Alias = "lastName",
                        Id = Guid.NewGuid(),
                        Name = "Last Name"
                    }
                }
            };
        }

        public Form Retrieve(string formAlias)
        {
            throw new NotImplementedException();
        }
    }

}