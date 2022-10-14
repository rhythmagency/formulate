namespace Formulate.Templates.PlainJavaScript
{
    using Formulate.Core.Packaging;
    using System.Collections.Generic;
    using Umbraco.Cms.Core.Manifest;

    public sealed class PackageManifestFilter : IManifestFilter
    {
        public void Filter(List<PackageManifest> manifests)
        {
            var version = typeof(PackageManifestFilter).GetAssemblyVersionOrDefault();

            manifests.Add(new PackageManifest()
            {
                PackageName = "Formulate.Templates.PlainJavaScript",
                AllowPackageTelemetry = true,
                Version = version.ToString(3),
                BundleOptions = BundleOptions.None
            });
        }
    }
}