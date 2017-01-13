namespace formulate.app.Forms.Fields.Text
{
    using System;
    using System.Collections.Generic;
    public class TextField : IFormFieldType
    {
        public string Directive => "formulate-text-field";
        public string TypeLabel => "Text";
        public string Icon => "icon-document-dashed-line";
        public Guid TypeId => new Guid("1790658086EA440BBC309E1B099F803B");
        public object DeserializeConfiguration(string configuration)
        {
            return null;
        }
        public string FormatValue(IEnumerable<string> values, FieldPresentationFormats format,
            object configuration)
        {
            return string.Join(", ", values);
        }
    }
}