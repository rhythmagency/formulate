namespace formulate.app.Managers
{

    // Namespaces.
    using core.Types;
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
        /// The root directory to store submitted files in.
        /// </summary>
        string FileStoreBasePath { get; }


        /// <summary>
        /// The templates (i.e., CSHTML files).
        /// </summary>
        IEnumerable<Template> Templates { get; }


        /// <summary>
        /// The button kinds used when creating button field types.
        /// </summary>
        IEnumerable<string> ButtonKinds { get; }


        /// <summary>
        /// Enable server side validation of form submissions?
        /// </summary>
        bool EnableServerSideValidation { get; }


        /// <summary>
        /// Is the email whitelist enabled?
        /// </summary>
        bool EnableEmailWhitelist { get; }


        /// <summary>
        /// The emails to whitelist.
        /// </summary>
        IEnumerable<AllowEmail> EmailWhitelist { get; }


        /// <summary>
        /// The field categories used for flagging field types.
        /// </summary>
        IEnumerable<FieldCategory> FieldCategories { get; }
        
        #endregion

    }

}