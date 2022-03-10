namespace CustomBuildActions;

/// <summary>
/// Copies the static assets to the website.
/// </summary>
internal class CopyStaticAssetsToWebsite
{
    /// <summary>
    /// Watches for file system changes.
    /// </summary>
    /// <remarks>
    /// This is static so it doesn't get garbage collected.
    /// </remarks>
    private static FileSystemWatcher? Watcher { get; set; }

    /// <summary>
    /// Should the copy occur?
    /// </summary>
    private static bool ShouldCopy { get; set; }

    /// <summary>
    /// Copies the static assets to the website.
    /// </summary>
    /// <param name="watch">
    /// Should the source directory be monitored for changes?
    /// </param>
    public static void Copy(bool watch)
    {
        // Variables.
        var source = PathUtils.NormalizePath("../Formulate.BackOffice.StaticAssets/App_Plugins/FormulateBackOffice");
        var destination = PathUtils.NormalizePath("../Website/App_Plugins/FormulateBackOffice");

        // Clear the old folder and copy the new files.
        ClearAndCopy(source, destination);

        // Watch for changes?
        if (watch)
        {
            AddWatcher(source, destination);
        }
    }

    /// <summary>
    /// Adds a file system watcher to copy the files any time the source
    /// files change.
    /// </summary>
    /// <param name="source">
    /// The source directory.
    /// </param>
    /// <param name="destination">
    /// The destination directory.
    /// </param>
    private static void AddWatcher(string source, string destination)
    {
        Console.WriteLine($"Adding watcher for: {source}");
        Watcher = new FileSystemWatcher();
        Watcher.Path = source;
        Watcher.NotifyFilter = NotifyFilters.LastWrite
            | NotifyFilters.FileName
            | NotifyFilters.DirectoryName
            | NotifyFilters.Attributes;
        Watcher.Filter = "*.*";
        Watcher.IncludeSubdirectories = true;
        Watcher.Changed += new FileSystemEventHandler((_, _) =>
        {
            ScheduleClearAndCopy(source, destination);
        });
        Watcher.EnableRaisingEvents = true;
    }

    /// <summary>
    /// Waits a second and then performs a copy (if another hasn't already
    /// been performed in that time). This acts as a way of
    /// debouncing/throttling.
    /// </summary>
    /// <param name="source">
    /// The source directory.
    /// </param>
    /// <param name="destination">
    /// The destination directory.
    /// </param>
    private static async void ScheduleClearAndCopy(string source, string destination)
    {
        ShouldCopy = true;
        await Task.Delay(TimeSpan.FromSeconds(1));
        if (ShouldCopy)
        {
            ShouldCopy = false;
            ClearAndCopy(source, destination);
            Console.WriteLine(Constants.PressEnterToStop);
        }
    }

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
    private static void ClearAndCopy(string source, string destination)
    {
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