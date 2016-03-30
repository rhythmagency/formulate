namespace formulate.app.Forms.Fields.Text
{
    using System;
    public class TextAreaField : IFormFieldType
    {
        public string Directive => "formulate-text-area-field";
        public string TypeLabel => "Text Area";
        public string Icon => "icon-document-dashed-line";
        public Guid TypeId => new Guid("9DA843594D0B494491449F8CCAE7A4DA");
        public object DeserializeConfiguration(string configuration)
        {
            return null;
        }
    }
}