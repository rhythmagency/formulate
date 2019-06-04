namespace formulate.app.Forms
{
    using System;
    using System.Collections.Generic;

    using Umbraco.Core.Composing;

    public interface IFormFieldType : IDiscoverable
    {
        string Directive { get; }
        string TypeLabel { get; }
        string Icon { get; }
        Guid TypeId { get; }
        object DeserializeConfiguration(string configuration);
        string FormatValue(IEnumerable<string> values, FieldPresentationFormats format, object configuration);
    }
}