namespace formulate.app.Forms.Fields
{
    public class CheckboxField : IFormFieldType
    {
        public string Directive
        {
            get
            {
                return "formulate-checkbox-field";
            }
        }
        public string TypeLabel
        {
            get
            {
                return "Checkbox";
            }
        }
        public string Icon
        {
            get
            {
                return "icon-document-dashed-line";
            }
        }
    }
}