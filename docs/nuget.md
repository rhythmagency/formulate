If you are an owner of the NuGet package, you can deploy to NuGet with commands that look like this:

```text
nuget push Formulate.Binaries.2.1.0.nupkg some-long-key-here -Source https://api.nuget.org/v3/index.json
nuget push Formulate.2.1.0.nupkg some-long-key-here -Source https://api.nuget.org/v3/index.json
nuget push Formulate.Deploy.2.1.0.nupkg some-long-key-here -Source https://api.nuget.org/v3/index.json
```

Just replace the version number in the filename and the API key (it's your API key you see when you log into your NuGet.org account).

Note that the files get generated in the "dist" folder when you build with `grunt package-full`.
I find it's easiest to simply move the `nupkg` files to the `"src/node_modules/grunt-nuget/libs"` folder and run the command line from there.

Also, be sure to increment the version number before creating new releases. This is done in the C# file for `formulate.meta.Constants`.