namespace CustomBuildActions;

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
        var source = PathUtils.NormalizePath("../Formulate.BackOffice.StaticAssets/App_Plugins/FormulateBackOffice");
        var destination = PathUtils.NormalizePath("../Website/App_Plugins/FormulateBackOffice");

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
        source = PathUtils.NormalizePath(source);
        destination = PathUtils.NormalizePath(destination);

        // Ensure the destination directories exist.
        foreach(var path in Directory.GetDirectories(source, "*", SearchOption.AllDirectories))
        {
            Directory.CreateDirectory(path.Replace(source, destination));
        }

        // Copy all the files.
        var allFilenames = new List<string>();
        foreach(var path in Directory.GetFiles(source, "*", SearchOption.AllDirectories))
        {
            var destinationPath = path.Replace(source, destination);
            allFilenames.Add(Path.GetFileName(destinationPath));
            File.Copy(path, destinationPath, true);
        }

        // Inform user of success.
        var count = allFilenames.Count;
        Console.WriteLine($@"Copied {count} files to ""{destination}"".");
    }
}