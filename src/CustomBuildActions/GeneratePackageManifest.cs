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
    /// <param name="executionCount">
    /// The number of times the tasks have been executed (starts at 1).
    /// </param>
    public static void Generate(long executionCount)
    {
        // Variables.
        var jsPattern = "*.js";
        var cssPattern = "*.css";
        var basePath = "../Formulate.BackOffice.StaticAssets";
        var basePathLength = PathUtils.NormalizePath(basePath).Length;
        var rootFolder = PathUtils
            .NormalizePath($"{basePath}/App_Plugins/FormulateBackOffice/");
        var allFolders = SearchOption.AllDirectories;
        var manifestPath = PathUtils
            .NormalizePath($"{basePath}/App_Plugins/FormulateBackOffice/package.manifest");

        // Get all the files, then format their paths.
        var jsFiles = Directory.GetFiles(rootFolder, jsPattern, allFolders)
            .Select(x => x.Substring(basePathLength))
            .Select(x => x.Replace(@"\", @"/"));
        var cssFiles = Directory.GetFiles(rootFolder, cssPattern, allFolders)
            .Select(x => x.Substring(basePathLength))
            .Select(x => x.Replace(@"\", @"/"));

        // Serialize the JSON stores in the package.manifest file.
        var contents = new
        {
            javascript = jsFiles,
            css = cssFiles,
        };
        var options = new JsonSerializerOptions()
        {
            WriteIndented = true,
        };
        var serialized = JsonSerializer.Serialize(contents, options);

        // Store the JSON to the package.manifest file.
        File.WriteAllText(manifestPath, serialized);

        // Inform user of success.
        Console.WriteLine($@"#{executionCount}: Generated package manifest at ""{manifestPath}"".");
    }
}