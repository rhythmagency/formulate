using CustomBuildActions;

// Variables.
var shouldWatch = args.Contains("-watch");

// Process each task specified by the command line arguments.
foreach(var task in args)
{
    switch (task)
    {
        case "-generate-css-for-svg-icons":
            GenerateCssForSvgIcons.Generate();
            break;
        case "-generate-package-manifest":
            GeneratePackageManifest.Generate();
            break;
        case "-copy-static-assets-to-website":
            CopyStaticAssetsToWebsite.Copy(shouldWatch);
            break;
        case "-create-app-settings-json":
            CreateAppSettingsJson.Create();
            break;
        case "-watch":
            break;
    }
}

// If we're watching for file system changes, don't quit.
if (shouldWatch)
{
    Console.WriteLine(Constants.PressEnterToStop);
    Console.ReadLine();
}