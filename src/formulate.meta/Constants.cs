namespace formulate.meta
{

    /// <summary>
    /// Constants relating to Formulate itself (i.e., does not
    /// include constants used by Formulate).
    /// </summary>
    public class Constants
    {

        /// <summary>
        /// This is the version of Formulate. It is used on
        /// assemblies and during the creation of the
        /// installer package.
        /// </summary>
        /// <remarks>
        /// Do not reformat this code. A grunt task reads this
        /// version number with a regular expression.
        /// </remarks>
        public const string Version = "3.1.0";


        /// <summary>
        /// The name of the Formulate package.
        /// </summary>
        public const string PackageName = "Formulate";


        /// <summary>
        /// The name of the Formulate package, in camel case.
        /// </summary>
        public const string PackageNameCamelCase = "formulate";

    }

}