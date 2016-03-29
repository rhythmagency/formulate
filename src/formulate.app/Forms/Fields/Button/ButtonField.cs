namespace formulate.app.Forms.Fields.Text
{
    using System;
    public class ButtonField : IFormFieldType
    {
        public string Directive => "formulate-button-field";
        public string TypeLabel => "Button";
        public string Icon => "icon-formulate-button";
        public Guid TypeId => new Guid("CDE8565C5E9241129A1F7FFA1940C53C");
        public object DeserializeConfiguration(string configuration)
        {
            return null;
        }
    }
}