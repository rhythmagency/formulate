namespace formulate.app.Templates
{

    // Namespaces.
    using System;


    /// <summary>
    /// A template (i.e., a CSHTML view).
    /// </summary>
    public class Template
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public Guid Id { get; set; }
    }

}