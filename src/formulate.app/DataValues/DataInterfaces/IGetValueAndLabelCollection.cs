namespace formulate.app.DataValues.DataInterfaces
{

    // Namespaces.
    using core.Types;
    using System.Collections.Generic;


    /// <summary>
    /// Any data values implementing this interface can return a collection of value and label items.
    /// </summary>
    public interface IGetValueAndLabelCollection
    {
        IEnumerable<ValueAndLabel> GetValues(string rawData);
    }

}