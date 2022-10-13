namespace Formulate.BackOffice.StaticAssets
{
    using Microsoft.AspNetCore.Hosting;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using Umbraco.Cms.Core.Extensions;
    using Umbraco.Cms.Core.Manifest;
    using Umbraco.Extensions;

    public sealed class PackageManifestFilter : IManifestFilter
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PackageManifestFilter(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public void Filter(List<PackageManifest> manifests)
        {
            var path = _webHostEnvironment.MapPathContentRoot(Constants.Package.PluginPath);
            var assembly = typeof(PackageManifestFilter).Assembly;
            var fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);

            manifests.Add(new PackageManifest()
            {
                PackageName = Constants.Package.Name,
                AllowPackageTelemetry = true,
                Version = assembly.GetName().Version.ToString(3),
                BundleOptions = BundleOptions.None,
                Scripts = GetPackageFilePaths(path, "*.js"),
                Stylesheets = GetPackageFilePaths(path, "*.css"),
            });
        }

        private string[] GetPackageFilePaths(string pluginPath, string wildcard)
        {
            var paths = new List<string>();

            foreach (var path in Directory.GetFiles(pluginPath, wildcard, SearchOption.AllDirectories))
            {
                var fixedPath = path.Replace(_webHostEnvironment.ContentRootPath, string.Empty).Replace("\\", "/").EnsureStartsWith('/');

                paths.Add(fixedPath);
            }

            return paths.ToArray();
        }
    }
}
