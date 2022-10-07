namespace CustomBuildActions;

/// <summary>
/// Copies the static assets to the website.
/// </summary>
internal class CopyStaticAssetsToWebsite
{
    /// <summary>
    /// Copies the static assets to the website.
    /// </summary>
    /// <param name="executionCount">
    /// The number of times the tasks have been executed (starts at 1).
    /// </param>
    public static void Copy(long executionCount)
    {
        // Variables.
        var source = PathUtils.NormalizePath("../Formulate.BackOffice.StaticAssets/App_Plugins/Formulate");
        var destination = PathUtils.NormalizePath("../Website/App_Plugins/Formulate");

        // Clear the old folder and copy the new files.
        FileUtils.ClearAndCopy(source, destination, executionCount);
    }
}