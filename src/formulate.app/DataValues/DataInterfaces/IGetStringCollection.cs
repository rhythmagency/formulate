namespace formulate.app.DataValues.DataInterfaces
{

    // Namespaces.
    using System.Collections.Generic;


    /// <summary>
    /// Any data values implementing this interface can return a collection of strings.
    /// </summary>
    public interface IGetStringCollection
    {
        IEnumerable<string> GetValues(string rawData);
    }

}