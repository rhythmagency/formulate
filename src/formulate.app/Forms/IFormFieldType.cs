namespace formulate.app.Forms
{
    using System;
    using System.Collections.Generic;

    using Umbraco.Core.Composing;

    /// <summary>
    /// A contract for creating a Form field.
    /// </summary>
    public interface IFormFieldType : IDiscoverable
    {
        /// <summary>
        /// Gets the AngularJS directive for this field type.
        /// </summary>
        string Directive { get; }

        /// <summary>
        /// Gets the label to show in the edtior UI.
        /// </summary>
        string TypeLabel { get; }

        /// <summary>
        /// Gets the icon to show in the editor UI.
        /// </summary>
        string Icon { get; }

        /// <summary>
        /// Gets the unique ID (useful for serialization).
        /// </summary>
        Guid TypeId { get; }

        /// <summary>
        /// Deserializes a provided serialized configuration.
        /// </summary>
        /// <param name="configuration">A serialized configuration.</param>
        /// <returns>A strongly typed deserialized configuration.</returns>
        object DeserializeConfiguration(string configuration);

        /// <summary>
        /// Formats the value.
        /// </summary>
        /// <param name="values">The values</param>
        /// <param name="format">The format.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns>A formatted value.</returns>
        string FormatValue(IEnumerable<string> values, FieldPresentationFormats format, object configuration);
    }
}