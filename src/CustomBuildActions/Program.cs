using CustomBuildActions;

// Variables.
var shouldWatch = args.Contains("-watch");

// Processes each task specified by the command line arguments.
var executeTasks = new Action<long>(count =>
{
    foreach (var task in args)
    {
        switch (task)
        {
            case "-everything":
                // Shorthand flag that does everything to avoid needing to
                // specify every other flag.
                CreateAppSettingsJson.Create(count);
                GenerateCssForSvgIcons.Generate(count);
                //GeneratePackageManifest.Generate(count);
                CopyStaticAssetsToWebsite.Copy(count);
                CopyViewsToWebsite.Copy(count);
                RefreshSampleFormulateJson.Refresh(count);
                break;
            case "-generate-css-for-svg-icons":
                GenerateCssForSvgIcons.Generate(count);
                break;
            case "-generate-package-manifest":
                //GeneratePackageManifest.Generate(count);
                break;
            case "-copy-static-assets-to-website":
                CopyStaticAssetsToWebsite.Copy(count);
                break;
            case "-copy-views-to-website":
                CopyViewsToWebsite.Copy(count);
                break;
            case "-create-app-settings-json":
                CreateAppSettingsJson.Create(count);
                break;
            case "-refresh-sample-formulate-data":
                RefreshSampleFormulateJson.Refresh(count);
                break;
            case "-watch":
                break;
        }
    }
});

// Execute the tasks.
executeTasks(1);

// Should we watch for changes and execute the tasks when they occur?
if (shouldWatch)
{
    // Start watching.
    FrontendChangesWatcher.ExecutionCount = 1;
    FrontendChangesWatcher.AddWatcher(executeTasks);

    // Don't quit while watching for changes.
    Console.WriteLine(Constants.PressEnterToStop);
    Console.ReadLine();
}