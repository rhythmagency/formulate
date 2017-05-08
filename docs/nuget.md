If you are an owner of the NuGet package, you can deploy to NuGet with commands that look like this:

```text
nuget push Formulate.Binaries.1.2.12.0.nupkg some-long-guid-here -Source https://www.nuget.org/api/v2/package
nuget push Formulate.1.2.12.0.nupkg some-long-guid-here -Source https://www.nuget.org/api/v2/package
```

Just replace the version number in the filename and the GUID (it's your API key you see when you log into your NuGet.org account).

Note that the files get generated in the "dist" folder when you build with `grunt package-full`.
I find it's easiest to simply move the `nupkg` files to the `"src/node_modules/grunt-nuget/libs"` folder and run the command line from there.

Also, be sure to increment the version number before creating new releases. This is done in the C# file for `formulate.meta.Constants`.