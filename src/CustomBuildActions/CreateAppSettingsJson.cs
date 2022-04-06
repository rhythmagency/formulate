namespace CustomBuildActions;

/// <summary>
/// Creates the appsettings.json file.
/// </summary>
internal class CreateAppSettingsJson
{
    /// <summary>
    /// Copies the appsettings.sample.json to create the appsettings.json
    /// file if it does not already exist.
    /// </summary>
    /// <param name="executionCount">
    /// The number of times the tasks have been executed (starts at 1).
    /// </param>
    /// <remarks>
    /// This is so we can have an appsettings.json without it showing
    /// up as changed in Git after running the Umbraco installer.
    /// </remarks>
    public static void Create(long executionCount)
    {
        // This only needs to be done once. Exit early if a subsequent
        // execution.
        if (executionCount > 1)
        {
            return;
        }

        // Variables.
        var source = "./appsettings.sample.json";
        var destination = "./appsettings.json";

        // Create the file if it doesn't exist.
        if (!File.Exists(destination))
        {
            File.Copy(source, destination, false);
            Console.WriteLine("Created appsettings.json file.");
        }
    }
}