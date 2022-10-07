namespace CustomBuildActions;

/// <summary>
/// Generates the CSS for the SVG icons.
/// </summary>
internal class GenerateCssForSvgIcons
{
    /// <summary>
    /// Generates the CSS that contains the styles that load SVGs based on
    /// class names that match the SVG's filename.
    /// </summary>
    /// <param name="executionCount">
    /// The number of times the tasks have been executed (starts at 1).
    /// </param>
    public static void Generate(long executionCount)
    {
        // Variables.
        var svgPattern = "*.svg";
        var basePath = "../Formulate.BackOffice.StaticAssets";
        var basePathLength = PathUtils.NormalizePath(basePath).Length;
        var rootFolder = PathUtils
            .NormalizePath($"{basePath}/App_Plugins/Formulate/backoffice/icons/");
        var allFolders = SearchOption.AllDirectories;
        var cssOutputPath = PathUtils
            .NormalizePath($"{basePath}/App_Plugins/Formulate/styles/generated-svg-icons.css");

        // Get all the files, then format their paths.
        var svgFiles = Directory.GetFiles(rootFolder, svgPattern, allFolders)
            .Select(x => x.Substring(basePathLength))
            .Select(x => x.Replace(@"\", @"/"));

        // Generate CSS rules.
        var cssRules = svgFiles
            .Select(x => new
            {
                Name = Path.GetFileNameWithoutExtension(x).ToLower(),
                Path = x,
            })
            .Select(x => new
            {
                Selector = $".formulate-icon.{x.Name}",
                Declaration = $@"background-image: url(""{x.Path}"")",
            })
            .Select(x => $"{x.Selector} {{{x.Declaration}}}")
            .ToArray();

        // Generate the file contents.
        var contents = "/* This file is auto-generated (don't modify manually). */" +
            Environment.NewLine + Environment.NewLine +
            string.Join(Environment.NewLine, cssRules);

        // Store the CSS.
        File.WriteAllText(cssOutputPath, contents);

        // Inform user of success.
        Console.WriteLine($@"#{executionCount}: Generated SVG CSS file at ""{cssOutputPath}"".");
    }
}