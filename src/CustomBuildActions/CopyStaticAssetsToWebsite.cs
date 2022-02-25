namespace CustomBuildActions
{
    /// <summary>
    /// Copies the static assets to the website.
    /// </summary>
    internal class CopyStaticAssetsToWebsite
    {
        /// <summary>
        /// Copies the static assets to the website.
        /// </summary>
        public static void Copy()
        {
            // Variables.
            var source = NormalizePath("./App_Plugins/FormulateBackOffice");
            var destination = NormalizePath("../Website/App_Plugins/FormulateBackOffice");

            // Clear out the old directory first.
            if (Directory.Exists(destination))
            {
                Directory.Delete(destination, true);
            }

            /// Copy the files.
            CopyDirectory(source, destination);
        }

        /// <summary>
        /// Copies the source directory to the target directory.
        /// </summary>
        /// <param name="source">
        /// The source directory.
        /// </param>
        /// <param name="destination">
        /// The destination directory.
        /// </param>
        private static void CopyDirectory(string source, string destination)
        {
            // Normalize paths.
            source = NormalizePath(source);
            destination = NormalizePath(destination);

            // Ensure the destination directories exist.
            foreach(var path in Directory.GetDirectories(source, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(path.Replace(source, destination));
            }

            // Copy all the files.
            foreach(var path in Directory.GetFiles(source, "*", SearchOption.AllDirectories))
            {
                File.Copy(path, path.Replace(source, destination), true);
            }
        }

        /// <summary>
        /// Normalizes the path to ensure it uses a standard format (backslash
        /// separator, no trailing slash, no dot directories).
        /// </summary>
        /// <param name="path">
        /// The unnormalized path.
        /// </param>
        /// <returns>
        /// The normalized path.
        /// </returns>
        private static string NormalizePath(string path)
        {
            return Path.GetFullPath(path)
                .Replace(@"/", @"\")
                .TrimEnd(@"\".ToCharArray());
        }
    }
}