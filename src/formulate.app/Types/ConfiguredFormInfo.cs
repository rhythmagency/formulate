namespace formulate.app.Types
{

    // Namespaces.
    using System;


    /// <summary>
    /// Information about a configured form.
    /// </summary>
    public class ConfiguredFormInfo
    {

        #region Properties

        /// <summary>
        /// The ID of the configured form.
        /// </summary>
        public Guid? Configuration { get; set; }


        /// <summary>
        /// The ID of the form.
        /// </summary>
        public Guid? FormId { get; set; }


        /// <summary>
        /// The ID of the layout.
        /// </summary>
        public Guid? LayoutId { get; set; }


        /// <summary>
        /// The ID of the template.
        /// </summary>
        public Guid? TemplateId { get; set; }

        #endregion

    }

}