namespace formulate.app.Managers
{

    // Namespaces.
    using System.Collections.Generic;
    using Templates;


    /// <summary>
    /// Manages Formulate's configuration values.
    /// </summary>
    public interface IConfigurationManager
    {

        #region Properties

        /// <summary>
        /// The root directory to store JSON in.
        /// </summary>
        string JsonBasePath { get; }


        /// <summary>
        /// The templates (i.e., CSHTML files).
        /// </summary>
        IEnumerable<Template> Templates { get; }


        /// <summary>
        /// Enable server side validation of form submissions?
        /// </summary>
        bool EnableServerSideValidation { get; }

        #endregion

    }

}