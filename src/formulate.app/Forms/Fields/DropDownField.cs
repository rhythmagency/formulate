namespace formulate.app.Forms.Fields
{
    public class DropDownField : IFormFieldType
    {
        public string Directive
        {
            get
            {
                return "formulate-drop-down-field";
            }
        }
        public string TypeLabel
        {
            get
            {
                return "Drop Down";
            }
        }
        public string Icon
        {
            get
            {
                return "icon-formulate-drop-down";
            }
        }
    }
}