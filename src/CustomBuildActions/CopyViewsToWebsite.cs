namespace CustomBuildActions;

/// <summary>
/// Copies the view files to the website.
/// </summary>
internal class CopyViewsToWebsite
{
    /// <summary>
    /// Copies the views to the website.
    /// </summary>
    /// <param name="executionCount">
    /// The number of times the tasks have been executed (starts at 1).
    /// </param>
    public static void Copy(long executionCount)
    {
        // Variables.
        var source = PathUtils.NormalizePath("../Formulate.Extensions.PlainJavaScriptTemplate/Views/Shared");
        var destination = PathUtils.NormalizePath("../Website/Views/Shared");

        // Clear the old folder and copy the new files.
        FileUtils.ClearAndCopy(source, destination, executionCount);
    }
}