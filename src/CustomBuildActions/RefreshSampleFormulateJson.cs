namespace CustomBuildActions;

/// <summary>
/// Creates the "Formulate/Json" directory based on the "Formulate/Json.Sample"
/// directory, if both already exist.
/// </summary>
/// <remarks>
/// This is to speed up development by refreshing the JSON files with a
/// known good set of JSON files.
/// </remarks>
internal class RefreshSampleFormulateJson
{
    /// <summary>
    /// Deletes the Json folder and recreates it based on the "Json.Sample" folder.
    /// </summary>
    /// <param name="executionCount">
    /// The number of times the tasks have been executed (starts at 1).
    /// </param>
    public static void Refresh(long executionCount)
    {
        // Variables.
        var source = "./wwwroot/App_Data/Formulate/Json.Sample";
        var destination = "./wwwroot/App_Data/Formulate/Json";

        // Copy the directory if both exist.
        if (Directory.Exists(source) && Directory.Exists(destination))
        {
            Directory.Delete(destination, true);
            CopyDirectory(source, destination);
            Console.WriteLine($"#{executionCount}: Restored sample Formulate JSON.");
        }
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
        foreach (var path in Directory.GetDirectories(source, "*", SearchOption.AllDirectories))
        {
            Directory.CreateDirectory(path.Replace(source, destination));
        }

        // Copy all the files.
        var allFilenames = new List<string>();
        foreach (var path in Directory.GetFiles(source, "*", SearchOption.AllDirectories))
        {
            var destinationPath = path.Replace(source, destination);
            allFilenames.Add(Path.GetFileName(destinationPath));
            File.Copy(path, destinationPath, true);
        }
    }
}