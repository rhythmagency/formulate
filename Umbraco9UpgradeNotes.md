# Build

To build, open a PowerShell command prompt and enter a command like this:

```
build.ps1 -v "2.1.1" -s "beta"
```

Be sure you've created the folder "c:\nuget\local".

You can then install that NuGet package into another website you're running on your local.

# Notes - Package.manifest

The package.manifest in Formulate.BackOffice.StaticAssets is a bit verbose. We should consider some way to avoid maintaining this file manually, such as by autogenerating it or by using C# to register the assets (supposing that is possible).

# Notes - Frontend Template

We may want to consider moving the plain JavaScript frontend template to an NPM package. Would be easier for others to customize the frontend that way. We may also want to restructure the require statements to instead be import statements (that way, it can be used in the browser without bundling).