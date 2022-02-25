foreach(var task in args)
{
    switch (task)
    {
        case "-generate-package-manifest":
            CustomBuildActions.GeneratePackageManifest.Generate();
            break;
    }
}