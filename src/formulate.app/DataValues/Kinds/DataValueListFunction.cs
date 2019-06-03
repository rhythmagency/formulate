namespace formulate.app.DataValues.Kinds
{

    // Namespaces.
    using core.Types;
    using DataInterfaces;
    using ExtensionMethods;
    using Helpers;
    using Newtonsoft.Json.Linq;
    using Suppliers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Constants = Constants.DataValues.DataValueListFunction;


    /// <summary>
    /// A data value kind that calls other functions that supply a list of data.
    /// </summary>
    public class DataValueListFunction : IDataValueKind, IGetValueAndLabelCollection
    {

        #region Properties

        /// <summary>
        /// The kind ID.
        /// </summary>
        public Guid Id
        {
            get
            {
                return GuidHelper.GetGuid(Constants.Id);
            }
        }


        /// <summary>
        /// The kind name.
        /// </summary>
        public string Name
        {
            get
            {
                return Constants.Name;// LocalizationHelper.GetDataValueName();
            }
        }


        /// <summary>
        /// The kind directive.
        /// </summary>
        public string Directive
        {
            get
            {
                return Constants.Directive;
            }
        }

        #endregion


        #region Methods

        /// <summary>
        /// Extracts the value and label collection from the specified raw data.
        /// </summary>
        /// <param name="rawData">
        /// The raw data for the list.
        /// </param>
        /// <returns>
        /// The collection of value and label items.
        /// </returns>
        public IEnumerable<ValueAndLabel> GetValues(string rawData)
        {

            // Variables.
            var supplierTypes = ReflectionHelper
                .GetTypesImplementingInterface<ISupplyValueAndLabelCollection>();


            // Validate input.
            if (string.IsNullOrWhiteSpace(rawData))
            {
                return Enumerable.Empty<ValueAndLabel>();
            }


            // Deserialize the raw data.
            var configData = JsonHelper.Deserialize<JObject>(rawData);
            var dynamicConfig = configData as dynamic;
            var properties = configData.Properties().Select(x => x.Name);
            var propertySet = new HashSet<string>(properties);


            // Get the values from the supplier.
            if (propertySet.Contains("supplier"))
            {
                var strSupplier = dynamicConfig.supplier.Value as string;
                var supplierType = supplierTypes
                    .FirstOrDefault(x => x.ShortAssemblyQualifiedName() == strSupplier);
                var supplier = default(ISupplyValueAndLabelCollection);
                if (supplierType != null)
                {
                    supplier = Activator.CreateInstance(supplierType) as ISupplyValueAndLabelCollection;
                }
                if (supplier != null)
                {
                    return supplier.GetValues();
                }
            }


            // Return empty collection.
            return Enumerable.Empty<ValueAndLabel>();

        }

        #endregion

    }

}