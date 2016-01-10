namespace formulate.app.Persistence
{

    // Namespaces.
    using DataValues;
    using System;
    using System.Collections.Generic;


    /// <summary>
    /// Interface for persistence of data values.
    /// </summary>
    public interface IDataValuePersistence
    {
        void Persist(DataValue dataValue);
        void Delete(Guid dataValueId);
        void Delete(string dataValueAlias);
        DataValue Retrieve(Guid dataValueId);
        DataValue Retrieve(string dataValueAlias);
        IEnumerable<DataValue> RetrieveChildren(Guid? parentId);
    }

}