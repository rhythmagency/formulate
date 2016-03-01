namespace formulate.app.Forms.Fields
{
    using System;
    public class DropDownField : IFormFieldType
    {
        public string Directive => "formulate-drop-down-field";
        public string TypeLabel => "Drop Down";
        public string Icon => "icon-formulate-drop-down";
        public Guid TypeId => new Guid("6D3DF1571BC44FCFB2B70A94FE719B47");
        public object DeserializeConfiguration(string configuration)
        {
            //TODO: This should return a list of strings (or of a value/label pair).
            return null;
        }
    }
}