using CustomBuildActions;

foreach(var task in args)
{
    switch (task)
    {
        case "-generate-package-manifest":
            GeneratePackageManifest.Generate();
            break;
        case "-copy-static-assets-to-website":
            CopyStaticAssetsToWebsite.Copy();
            break;
        case "-create-app-settings-json":
            CreateAppSettingsJson.Create();
            break;
    }
}