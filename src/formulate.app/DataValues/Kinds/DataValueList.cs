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

    using Umbraco.Core.Services.Implement;

    using Constants = Constants.DataValues.DataValueList;


    /// <summary>
    /// A data value kind that stores a list of strings.
    /// </summary>
    public class DataValueList : IDataValueKind, IGetStringCollection, IGetValueAndLabelCollection
    {
        public DataValueList(ILocalizationHelper localizationHelper)
        {
            LocalizationHelper = localizationHelper;
        }


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
                return LocalizationHelper.GetDataValueName(Constants.Name);
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

        private ILocalizationHelper LocalizationHelper { get; set; }

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

            // Variables.
            var items = new List<string>();


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
                    var strItem = item.value.Value as string;
                    items.Add(strItem);
                }
            }


            // Return string collection.
            return items;

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
            return (this as IGetStringCollection)
                .GetValues(rawData)
                .Select(x => new ValueAndLabel()
                {
                    Value = x,
                    Label = x
                });
        }

        #endregion

    }

}