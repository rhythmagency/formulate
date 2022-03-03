namespace CustomBuildActions;

using System.Text.Json;

/// <summary>
/// Generates the package.manifest.
/// </summary>
internal class GeneratePackageManifest
{
    /// <summary>
    /// Generates the package.manifest that contains the frontend resources used
    /// by Formulate in the Umbraco back office.
    /// </summary>
    public static void Generate()
    {
        // Variables.
        var searchPattern = "*.js";
        var basePath = "../Formulate.BackOffice.StaticAssets";
        var basePathLength = PathUtils.NormalizePath(basePath).Length;
        var rootFolder = PathUtils
            .NormalizePath($"{basePath}/App_Plugins/FormulateBackOffice/");
        var allFolders = SearchOption.AllDirectories;
        var manifestPath = PathUtils
            .NormalizePath($"{basePath}/App_Plugins/FormulateBackOffice/package.manifest");

        // Get all the files, then format their paths.
        var files = Directory.GetFiles(rootFolder, searchPattern, allFolders)
            .Select(x => x.Substring(basePathLength))
            .Select(x => x.Replace(@"\", @"/"));

        // Serialize the JSON stores in the package.manifest file.
        var contents = new
        {
            javascript = files,
        };
        var options = new JsonSerializerOptions()
        {
            WriteIndented = true,
        };
        var serialized = JsonSerializer.Serialize(contents, options);

        // Store the JSON to the package.manifest file.
        File.WriteAllText(manifestPath, serialized);

        // Inform user of success.
        Console.WriteLine($@"Generated package manifest at ""{manifestPath}"".");
    }
}