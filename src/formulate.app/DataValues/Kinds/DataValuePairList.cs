namespace formulate.app.DataValues.Kinds
{

    // Namespaces.
    using core.Types;
    using DataInterfaces;
    using Helpers;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Constants = Constants.DataValues.DataValuePairList;


    /// <summary>
    /// A data value kind that stores a list of string pairs.
    /// </summary>
    public class DataValuePairList : IDataValueKind, IGetStringCollection, IGetValueAndLabelCollection
    {

        #region Properties

        public Guid Id => GuidHelper.GetGuid(Constants.Id);

        public string Name => Constants.Name; //LocalizationHelper.GetDataValueName(Constants.Name);
        public string Directive => Constants.Directive;

        #endregion


        #region Methods

        /// <summary>
        /// Extracts the string collection from the specified raw data.
        /// </summary>
        /// <param name="rawData">
        /// The raw data for the list.
        /// </param>
        /// <returns>
        /// The collection of strings.
        /// </returns>
        IEnumerable<string> IGetStringCollection.GetValues(string rawData)
        {
            return (this as IGetValueAndLabelCollection)
                .GetValues(rawData)
                .Select(x => x.Value);
        }


        /// <summary>
        /// Extracts the value and label collection from the specified raw data.
        /// </summary>
        /// <param name="rawData">
        /// The raw data for the list.
        /// </param>
        /// <returns>
        /// The collection of value and label items.
        /// </returns>
        IEnumerable<ValueAndLabel> IGetValueAndLabelCollection.GetValues(string rawData)
        {

            // Variables.
            var items = new List<ValueAndLabel>();


            // Validate input.
            if (string.IsNullOrWhiteSpace(rawData))
            {
                return items;
            }


            // Deserialize the raw data.
            var configData = JsonHelper.Deserialize<JObject>(rawData);
            var dynamicConfig = configData as dynamic;
            var properties = configData.Properties().Select(x => x.Name);
            var propertySet = new HashSet<string>(properties);


            // Exract each string value.
            if (propertySet.Contains("items"))
            {
                foreach (var item in dynamicConfig.items)
                {
                    var primary = item.primary.Value as string;
                    var secondary = item.secondary.Value as string;
                    items.Add(new ValueAndLabel()
                    {
                        Value = primary,
                        Label = secondary
                    });
                }
            }


            // Return string pair collection.
            return items;

        }

        #endregion

    }

}