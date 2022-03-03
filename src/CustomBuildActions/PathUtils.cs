namespace CustomBuildActions
{
    /// <summary>
    /// Utilities for working with file system paths.
    /// </summary>
    internal class PathUtils
    {
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
        public static string NormalizePath(string path)
        {
            return Path.GetFullPath(path)
                .Replace(@"/", @"\")
                .TrimEnd(@"\".ToCharArray());
        }
    }
}