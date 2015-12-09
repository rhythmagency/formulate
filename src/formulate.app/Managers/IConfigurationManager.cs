namespace formulate.app.Managers
{

    /// <summary>
    /// Manages Formulate's configuration values.
    /// </summary>
    public interface IConfigurationManager
    {

        /// <summary>
        /// The root directory to store JSON in.
        /// </summary>
        string JsonBasePath { get; }

    }

}