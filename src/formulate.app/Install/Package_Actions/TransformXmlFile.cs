namespace formulate.app.Install.Package_Actions
{

    // Namespaces.
    using Microsoft.Web.XmlTransform;
    using System.Web.Hosting;
    using System.Xml;
    using umbraco.cms.businesslogic.packager.standardPackageActions;
    using umbraco.interfaces;
    using MetaConstants = meta.Constants;
    using Resources = formulate.app.Properties.Resources;
    using XmlHelper = Helpers.XmlHelper;


    /// <summary>
    /// Umbraco package action that transforms an XML file.
    /// </summary>
    public class TransformXmlFile : IPackageAction
    {

        #region Public Methods

        /// <summary>
        /// The alias of this package action.
        /// </summary>
        /// <returns>
        /// The alias to be used for this package action.
        /// </returns>
        public string Alias()
        {
            var packageName = MetaConstants.PackageName;
            var actionName = typeof(TransformXmlFile).Name;
            var aliasFormat = "{0}.{1}";
            var alias = string.Format(aliasFormat, packageName, actionName);
            return alias;
        }


        /// <summary>
        /// Sample XML that can be used to invoke this package action.
        /// </summary>
        /// <returns>The sample XML.</returns>
        public XmlNode SampleXml()
        {
            var sample = Resources.TransformWebConfig;
            return helper.parseStringToXmlNode(sample);
        }


        /// <summary>
        /// Transforms the XML file with the install transformation.
        /// </summary>
        /// <param name="packageName">
        /// The package name.
        /// </param>
        /// <param name="xmlData">
        /// The package action XML data.
        /// </param>
        /// <returns>
        /// True, if execution was successful; otherwise, false.
        /// </returns>
        public bool Execute(string packageName, XmlNode xmlData)
        {
            Transform(xmlData, true);
            return true;
        }


        /// <summary>
        /// Transforms the XML file with the uninstall transformation.
        /// </summary>
        /// <param name="packageName">
        /// The package name.
        /// </param>
        /// <param name="xmlData">
        /// The package action XML data.
        /// </param>
        /// <returns>
        /// True, if execution was successful; otherwise, false.
        /// </returns>
        public bool Undo(string packageName, XmlNode xmlData)
        {
            Transform(xmlData, false);
            return true;
        }

        #endregion


        #region Private Methods

        /// <summary>
        /// Transforms the XML file.
        /// </summary>
        /// <param name="xmlData">
        /// The package action XML.
        /// </param>
        /// <param name="install">
        /// Install or uninstall?
        /// </param>
        private void Transform(XmlNode xmlData, bool install)
        {

            // Extract paths from XML.
            var fileToTransform = GetAttributeValue(xmlData, "file");
            var transformAttribute = install
                ? "installTransform"
                : "uninstallTransform";
            var transformFile = GetAttributeValue(xmlData, transformAttribute);


            // Map paths.
            fileToTransform = HostingEnvironment.MapPath(fileToTransform);
            transformFile = HostingEnvironment.MapPath(transformFile);


            // Transform file.
            using (var doc = new XmlTransformableDocument())
            using (var transform = new XmlTransformation(transformFile))
            {
                doc.PreserveWhitespace = true;
                doc.Load(fileToTransform);
                transform.Apply(doc);
                doc.Save(fileToTransform);
            }

        }


        /// <summary>
        /// Gets the value of the specified attribute from the
        /// specified XML.
        /// </summary>
        /// <param name="xmlData">The XML data.</param>
        /// <param name="attributeName">The attribute name.</param>
        /// <returns>The value.</returns>
        private string GetAttributeValue(XmlNode xmlData,
            string attributeName)
        {
            return XmlHelper.GetAttributeValueFromNode(
                xmlData, attributeName);
        }

        #endregion

    }

}