namespace formulate.app.Forms.Fields.DropDown
{

    // Namespaces.
    using DataValues.Kinds;
    using Helpers;
    using Newtonsoft.Json.Linq;
    using Persistence;
    using Resolvers;
    using System;
    using System.Collections.Generic;
    using System.Linq;


    /// <summary>
    /// A drop down field type.
    /// </summary>
    public class DropDownField : IFormFieldType
    {

        #region Private Properties

        private IDataValuePersistence DataValues { get; set; }

        #endregion


        #region Public Properties

        /// <summary>
        /// The Angular directive for this field type.
        /// </summary>
        public string Directive => "formulate-drop-down-field";


        /// <summary>
        /// The label to show in the UI for this field type.
        /// </summary>
        public string TypeLabel => "Drop Down";


        /// <summary>
        /// The icon to display in the selection screen for this field type.
        /// </summary>
        public string Icon => "icon-formulate-drop-down";


        /// <summary>
        /// The GUID that uniquely identifies this field type (useful for serialization).
        /// </summary>
        public Guid TypeId => new Guid("6D3DF1571BC44FCFB2B70A94FE719B47");

        #endregion


        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public DropDownField()
        {
            DataValues = DataValuePersistence.Current.Manager;
        }

        #endregion


        #region Public Methods

        /// <summary>
        /// Deserialize the configuration for a drop down field.
        /// </summary>
        /// <param name="configuration">
        /// The serialized configuration.
        /// </param>
        /// <returns>
        /// The deserialized configuration.
        /// </returns>
        public object DeserializeConfiguration(string configuration)
        {

            // Variables.
            var items = new List<DropDownItem>();
            var config = new DropDownConfiguration()
            {
                Items = items
            };
            var configData = JsonHelper.Deserialize<JObject>(configuration);
            var dynamicConfig = configData as dynamic;
            var properties = configData.Properties().Select(x => x.Name);
            var propertySet = new HashSet<string>(properties);


            // A data value is selected?
            if (propertySet.Contains("dataValue"))
            {

                // Get info about the data value.
                var dataValueId = GuidHelper.GetGuid(dynamicConfig.dataValue.Value as string);
                var dataValue = DataValues.Retrieve(dataValueId);
                if (dataValue != null)
                {

                    // Extract list items from the data value.
                    var kinds = DataValueHelper.GetAllDataValueKinds();
                    var kind = kinds.FirstOrDefault(x => x.Id == dataValue.KindId);
                    var strItems = kind is DataValueList ? ExtractList(dataValue.Data) : new List<string>();
                    items.AddRange(strItems.Select(x => new DropDownItem()
                    {
                        Label = x,
                        Selected = false,
                        Value = x
                    }));

                }

            }


            // Return the data value configuration.
            return config;

        }

        #endregion


        #region Private Methods

        /// <summary>
        /// Extracts a list of strings from a data value list.
        /// </summary>
        /// <param name="listDataValue">
        /// The serialized list data.
        /// </param>
        /// <returns>
        /// The list of strings.
        /// </returns>
        private List<string> ExtractList(string listDataValue)
        {
            var items = new List<string>();
            var configData = JsonHelper.Deserialize<JObject>(listDataValue);
            var dynamicConfig = configData as dynamic;
            var properties = configData.Properties().Select(x => x.Name);
            var propertySet = new HashSet<string>(properties);
            if (propertySet.Contains("items"))
            {
                foreach (var item in dynamicConfig.items)
                {
                    var strItem = item.value.Value as string;
                    items.Add(strItem);
                }
            }
            return items;
        }

        #endregion

    }

}