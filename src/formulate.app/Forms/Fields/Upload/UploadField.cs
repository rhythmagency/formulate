namespace formulate.app.Forms.Fields.Text
{
    using System;
    using System.Collections.Generic;
    public class UploadField : IFormFieldType
    {
        public string Directive => "formulate-upload-field";
        public string TypeLabel => "Upload";
        public string Icon => "icon-formulate-upload";
        public Guid TypeId => new Guid("DFEFA5EC02004806A2AB0AB22058021D");
        public object DeserializeConfiguration(string configuration)
        {
            return null;
        }
        public string FormatValue(IEnumerable<string> values, FieldPresentationFormats format)
        {
            return null;
        }
    }
}