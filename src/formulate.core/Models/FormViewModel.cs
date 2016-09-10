namespace formulate.core.Models
{

    // Namespaces.
    using Types;


    /// <summary>
    /// The view model that contains everything necessary to render a form.
    /// </summary>
    public class FormViewModel
    {
        public FormDefinition FormDefinition { get; set; }
        public LayoutDefinition LayoutDefinition { get; set; }
        public string TemplatePath { get; set; }
        public int PageId { get; set; }
    }

}