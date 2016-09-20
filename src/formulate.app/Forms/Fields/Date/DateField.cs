namespace formulate.app.Forms.Fields.Date
{
    using System;
    public class DateField : IFormFieldType
    {
        public string Directive => "formulate-date-field";
        public string TypeLabel => "Date";
        public string Icon => "icon-formulate-date";
        public Guid TypeId => new Guid("E70CB78CCF52461198B4556382F23119");
        public object DeserializeConfiguration(string configuration)
        {
            return null;
        }
    }
}