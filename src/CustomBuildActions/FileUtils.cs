namespace CustomBuildActions;

/// <summary>
/// Utilities for working with files.
/// </summary>
internal class FileUtils
{
    /// <summary>
    /// Clears out the destination directory and copies the source directory
    /// into the destination directory.
    /// </summary>
    /// <param name="source">
    /// The source directory.
    /// </param>
    /// <param name="destination">
    /// The destination directory.
    /// </param>
    /// <param name="executionCount">
    /// The number of times the tasks have been executed (starts at 1).
    /// </param>
    public static async void ClearAndCopy(string source, string destination,
        long executionCount)
    {
        try
        {
            // Clear out the old directory first.
            if (Directory.Exists(destination))
            {
                try
                {
                    Directory.Delete(destination, true);
                }
                catch
                {
                    // If an error happens, wait a moment and try again.
                    await Task.Delay(TimeSpan.FromSeconds(.5));
                    Directory.Delete(destination, true);
                }
            }

            /// Copy the files.
            CopyDirectory(source, destination, executionCount);
        }
        catch
        {
            Console.WriteLine($"{executionCount}: Encountered an error when attempting to copy the files.");
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
    /// <param name="executionCount">
    /// The number of times the tasks have been executed (starts at 1).
    /// </param>
    public static void CopyDirectory(string source, string destination,
        long executionCount)
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

        // Inform user of success.
        var count = allFilenames.Count;
        Console.WriteLine($@"#{executionCount}: Copied {count} files to ""{destination}"".");
    }
}