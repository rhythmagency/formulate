namespace formulate.app.Helpers
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A contract for creating a helper for getting data values.
    /// </summary>
    public interface IGetDataValuesHelper
    {
        /// <summary>
        /// Gets the Data Values for a given ID.
        /// </summary>
        /// <param name="id">The ID.</param>
        /// <returns>A collection of <see cref="KeyValuePair{TKey,TValue}"/>.</returns>
        IEnumerable<KeyValuePair<string, string>> GetById(Guid id);
    }
}
