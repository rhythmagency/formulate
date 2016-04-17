namespace formulate.core.Utilities
{

    // Namespaces.
    using Exceptions;
    using System.IO;
    using System.Web.Hosting;


    /// <summary>
    /// Assists with operations related to files.
    /// </summary>
    public static class FileUtility
    {

        #region Constants

        private const string ViewNotFound = "The specified Formulate view could not be located on the file system.";

        #endregion


        #region Methods

        /// <summary>
        /// Validates that a view at the specified path exists.
        /// </summary>
        /// <param name="path">
        /// The path (e.g., "~/Views/Partials/Formulate/SomeView.cshtml").
        /// </param>
        /// <remarks>
        /// An exception will be thrown if the view does not exist.
        /// </remarks>
        public static void ValidateView(string path)
        {
            var valid = !string.IsNullOrWhiteSpace(path);
            var localPath = valid
                ? HostingEnvironment.MapPath(path)
                : null;
            valid = valid && File.Exists(localPath);
            if (!valid)
            {
                throw new ViewNotFoundException(ViewNotFound, path);
            }
        }

        #endregion

    }

}