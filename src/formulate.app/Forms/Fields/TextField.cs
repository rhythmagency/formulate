namespace formulate.app.Forms.Fields
{
    using System;
    public class TextField : IFormFieldType
    {
        public string Directive => "formulate-text-field";
        public string TypeLabel => "Text";
        public string Icon => "icon-document-dashed-line";
        public Guid TypeId => new Guid("1790658086EA440BBC309E1B099F803B");
    }
}