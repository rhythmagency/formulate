namespace formulate.core.Types
{
    
    //  Namespaces.
    using System.Collections.Generic;
    
    /// <summary>
    /// Used to pass validation error field and messages.
    /// </summary>
    public class ValidationError
    {
        public string FieldName { get; set; }
        public string FieldId { get; set; }
        public List<string> Messages { get; set; }
    }
}
