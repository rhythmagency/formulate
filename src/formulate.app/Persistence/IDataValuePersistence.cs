namespace formulate.app.Persistence
{

    // Namespaces.
    using DataValues;
    using System;
    using System.Collections.Generic;
    
    /// <summary>
    /// Interface for persistence of Data Values.
    /// </summary>
    public interface IDataValuePersistence
    {
        /// <summary>
        /// Persist a Data Value.
        /// </summary>
        /// <param name="dataValue">
        /// The Data Value.
        /// </param>
        void Persist(DataValue dataValue);

        /// <summary>
        /// Delete a Data Value by ID.
        /// </summary>
        /// <param name="dataValueId">
        /// The Data Value id.
        /// </param>
        void Delete(Guid dataValueId);

        /// <summary>
        /// Delete a Data Value by alias.
        /// </summary>
        /// <param name="dataValueAlias">
        /// The Data Value alias.
        /// </param>
        void Delete(string dataValueAlias);

        /// <summary>
        /// Retrieve a Data Value by ID.
        /// </summary>
        /// <param name="dataValueId">
        /// The Data Value id.
        /// </param>
        /// <returns>
        /// A <see cref="DataValue"/>.
        /// </returns>
        DataValue Retrieve(Guid dataValueId);

        /// <summary>
        /// Retrieve a Data Value by alias.
        /// </summary>
        /// <param name="dataValueAlias">
        /// The Data Value alias.
        /// </param>
        /// <returns>
        /// A <see cref="DataValue"/>.
        /// </returns>
        DataValue Retrieve(string dataValueAlias);

        /// <summary>
        /// Retrieve children by their parent ID.
        /// </summary>
        /// <param name="parentId">
        /// The parent id.
        /// </param>
        /// <returns>
        /// If found a collection of <see cref="DataValue"/>s.
        /// </returns>
        IEnumerable<DataValue> RetrieveChildren(Guid? parentId);
    }
}
