namespace formulate.app.Persistence
{

    // Namespaces.
    using System;
    using System.Collections.Generic;
    using Validations;


    /// <summary>
    /// Interface for persistence of validations.
    /// </summary>
    public interface IValidationPersistence
    {
        void Persist(Validation validation);
        void Delete(Guid validationId);
        void Delete(string validationAlias);
        Validation Retrieve(Guid validationId);
        Validation Retrieve(string validationAlias);
        IEnumerable<Validation> RetrieveChildren(Guid? parentId);
    }

}