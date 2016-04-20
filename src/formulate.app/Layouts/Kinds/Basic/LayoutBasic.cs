namespace formulate.app.Layouts.Kinds.Basic
{

    // Namespaces.
    using Helpers;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Constants = Constants.Layouts.LayoutBasic;


    /// <summary>
    /// A layout kind that gives basic layout options.
    /// </summary>
    public class LayoutBasic : ILayoutKind
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
                return LocalizationHelper.GetLayoutName(Constants.Name);
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
        /// Deserializes the layout.
        /// </summary>
        /// <param name="configuration">
        /// The serialized layout data.
        /// </param>
        /// <returns>
        /// The deserialized layout.
        /// </returns>
        public object DeserializeConfiguration(string configuration)
        {

            // Variables.
            var layout = new LayoutBasicConfiguration();
            var configData = JsonHelper.Deserialize<JObject>(configuration);
            var dynamicConfig = configData as dynamic;
            var properties = configData.Properties().Select(x => x.Name);
            var propertySet = new HashSet<string>(properties);
            var rows = new List<LayoutRow>();
            layout.Rows = rows;


            // Process each row?
            if (propertySet.Contains("rows"))
            {
                foreach (var rowData in dynamicConfig.rows)
                {

                    // Variables.
                    var row = new LayoutRow();
                    var cells = new List<LayoutCell>();
                    row.Cells = cells;
                    rows.Add(row);


                    // Process each cell.
                    foreach (var cellData in rowData.cells)
                    {

                        // Variables.
                        var cell = new LayoutCell();
                        var fields = new List<LayoutField>();
                        cell.Fields = fields;
                        cells.Add(cell);


                        // Process each field.
                        foreach (var fieldData in cellData.fields)
                        {
                            var field = new LayoutField();
                            fields.Add(field);
                            var fieldId = GuidHelper.GetGuid(fieldData.id.Value as string);
                            field.FieldId = fieldId;
                        }

                    }


                    // Set column span for each cell.
                    var columnSpan = 12 / cells.Count;
                    foreach(var cell in cells)
                    {
                        cell.ColumnSpan = columnSpan;
                    }

                }
            }


            // Return deserialized layout configuration.
            return layout;

        }

        #endregion

    }

}