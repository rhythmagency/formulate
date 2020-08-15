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
                return Constants.Name; //LocalizationHelper.GetLayoutName(Constants.Name);
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


        #region Public Methods

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
            var configData = JsonHelper.Deserialize<JObject>(configuration);
            var dynamicConfig = configData as dynamic;
            var properties = configData.Properties().Select(x => x.Name);
            var propertySet = new HashSet<string>(properties);
            var rows = new List<LayoutRow>();
            var layout = new LayoutBasicConfiguration()
            {
                Rows = rows
            };


            // Get the autopopulate value.
            if (propertySet.Contains("autopopulate"))
            {
                var autopopulate = dynamicConfig.autopopulate.Value as bool?;
                layout.Autopopulate = autopopulate.GetValueOrDefault();
            }


            // Get the form ID.
            if (propertySet.Contains("formId"))
            {
                layout.FormId = GuidHelper.GetGuid(dynamicConfig.formId.Value as string);
            }


            // Process each row?
            if (propertySet.Contains("rows"))
            {
                foreach (var rowData in dynamicConfig.rows)
                {

                    // Variables.
                    var castedRowData = rowData as JObject;
                    var rowProperties = castedRowData.Properties().Select(x => x.Name);
                    var rowPropertySet = new HashSet<string>(rowProperties);
                    var row = new LayoutRow();
                    var cells = new List<LayoutCell>();
                    row.Cells = cells;
                    rows.Add(row);


                    // Is this a row that indicates a new step has started?
                    if (rowPropertySet.Contains("isStep"))
                    {
                        row.IsStep = (rowData.isStep.Value as bool?).GetValueOrDefault();
                    }


                    // Process each cell.
                    foreach (var cellData in rowData.cells)
                    {

                        // Variables.
                        var cell = new LayoutCell();
                        var fields = new List<LayoutField>();
                        cell.Fields = fields;
                        cell.ColumnSpan = TryGetColumnSpan(cellData);
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


                    // Set column span for each cell that doesn't have one specified explicitly.
                    // This is for forms created in older versions of Formulate.
                    SetFallbackColumnSpans(cells);

                }
            }


            // Return deserialized layout configuration.
            return layout;

        }


        /// <summary>
        /// Returns a copy of the specified configuration, with the form ID replaced.
        /// </summary>
        /// <param name="configuration">
        /// The original serialized configuration.
        /// </param>
        /// <param name="formId">
        /// The new form ID to use.
        /// </param>
        /// <returns>
        /// The new configuration, serialized and with the new form ID.
        /// </returns>
        public string DuplicateConfigurationForForm(string configuration, Guid formId)
        {
            var newConfig = JsonHelper.Deserialize<dynamic>(configuration);
            newConfig.formId = GuidHelper.GetString(formId);
            var serialized = JsonHelper.Serialize(newConfig);
            return serialized;
        }

        #endregion


        #region Private Methods

        /// <summary>
        /// Attempts to extract the column span from cell data.
        /// </summary>
        /// <param name="cellData">
        /// The cell data to attempt to extract the column span from.
        /// </param>
        /// <remarks>
        /// This method will eventually be deleted.
        /// </remarks>
        [Obsolete("This will stick around for a while for forms created in older versions of Formulate.")]
        private int TryGetColumnSpan(dynamic cellData)
        {
            var hasColumnSpan = cellData["columnSpan"] != null;
            return hasColumnSpan
                ? (int)cellData.columnSpan
                : 0;
        }


        /// <summary>
        /// Fallback to a calculated column span when one is not otherwise specified.
        /// </summary>
        /// <param name="cells">
        /// The cells to set fallback column spans on.
        /// </param>
        /// <remarks>
        /// This method will eventually be deleted.
        /// </remarks>
        [Obsolete("This will stick around for a while for forms created in older versions of Formulate.")]
        private void SetFallbackColumnSpans(List<LayoutCell> cells)
        {
            if (cells.Count == 0)
            {
                return;
            }
            var columnSpan = 12 / cells.Count;
            foreach (var cell in cells)
            {
                if (cell.ColumnSpan == 0)
                {
                    cell.ColumnSpan = columnSpan;
                }
            }
        }

        #endregion

    }

}