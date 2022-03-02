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
    /// <remarks>
    /// This is so we can have an appsettings.json without it showing
    /// up as changed in Git after running the Umbraco installer.
    /// </remarks>
    public static void Create()
    {
        var source = "./appsettings.sample.json";
        var destination = "./appsettings.json";
        if (!File.Exists(destination))
        {
            File.Copy(source, destination, false);
        }
    }
}