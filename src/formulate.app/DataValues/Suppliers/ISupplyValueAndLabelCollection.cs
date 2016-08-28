namespace formulate.app.DataValues.Suppliers
{

    // Namespaces.
    using core.Types;
    using System.Collections.Generic;


    /// <summary>
    /// Any classes implementing this interface can return a collection of value and label items.
    /// </summary>
    public interface ISupplyValueAndLabelCollection
    {

        /// <summary>
        /// The name of this supplier (to be displayed when selecting suppliers in the back office.
        /// </summary>
        string Name { get; }


        /// <summary>
        /// Returns the values for this supplier.
        /// </summary>
        /// <returns>
        /// The values.
        /// </returns>
        IEnumerable<ValueAndLabel> GetValues();

    }

}