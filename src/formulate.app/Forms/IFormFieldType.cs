namespace formulate.app.Forms
{
    using System;
    using System.Collections.Generic;
    public interface IFormFieldType
    {
        string Directive { get; }
        string TypeLabel { get; }
        string Icon { get; }
        Guid TypeId { get; }
        object DeserializeConfiguration(string configuration);
        string FormatValue(IEnumerable<string> values, FieldPresentationFormats format);
    }
}