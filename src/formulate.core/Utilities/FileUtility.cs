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
            var localPath = HostingEnvironment.MapPath(path);
            var exists = File.Exists(localPath);
            if (!exists)
            {
                throw new ViewNotFoundException(
                    "The specified Formulate view could not be located on the file system.",
                    path);
            }
        }

        #endregion

    }

}