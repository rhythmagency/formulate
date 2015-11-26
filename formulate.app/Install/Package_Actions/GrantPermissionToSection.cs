namespace formulate.app.Install.Package_Actions
{

    // Namespaces.
    using System.Xml;
    using umbraco.cms.businesslogic.packager.standardPackageActions;
    using umbraco.interfaces;
    using Umbraco.Core;
    using Umbraco.Core.Models.Membership;
    using Umbraco.Web;
    using Resources = formulate.app.Properties.Resources;
    using XmlHelper = Helpers.XmlHelper;


    /// <summary>
    /// Umbraco package action that grants permission to the specified
    /// section for the specified user.
    /// </summary>
    /// <remarks>
    /// If the specified username is "$CurrentUser", the current user will be used.
    /// </remarks>
    public class GrantPermissionToSection : IPackageAction
    {

        #region Public Mehods

        /// <summary>
        /// The alias of this package action.
        /// </summary>
        /// <returns>The alias to be used for this package action.</returns>
        public string Alias()
        {
            return typeof(GrantPermissionToSection).Name;
        }


        /// <summary>
        /// Grant permission.
        /// </summary>
        /// <param name="packageName">The package name.</param>
        /// <param name="xmlData">The XML data.</param>
        /// <returns>
        /// True, if execution was successful; otherwise, false.
        /// </returns>
        public bool Execute(string packageName, XmlNode xmlData)
        {
            return Grant(xmlData);
        }


        /// <summary>
        /// Revokes permission.
        /// </summary>
        /// <param name="packageName">The package name.</param>
        /// <param name="xmlData">The XML data.</param>
        /// <returns>
        /// True, if execution was successful; otherwise, false.
        /// </returns>
        public bool Undo(string packageName, XmlNode xmlData)
        {
            return Revoke(xmlData);
        }


        /// <summary>
        /// Sample XML that can be used to invoke this package actionn.
        /// </summary>
        /// <returns>The sample XML.</returns>
        public XmlNode SampleXml()
        {
            var sample = Resources.GrantPermissionToSection;
            return helper.parseStringToXmlNode(sample);
        }

        #endregion


        #region Private Methods

        /// <summary>
        /// Gets the name for the section to grant permission to from
        /// the specified XML.
        /// </summary>
        /// <param name="xmlData">The XML data.</param>
        /// <returns>The section name.</returns>
        private string GetSectionNameFromXml(XmlNode xmlData)
        {
            return GetAttributeValue(xmlData, "sectionName");
        }


        /// <summary>
        /// Gets the username to grant permission to.
        /// </summary>
        /// <param name="xmlData">The XML data.</param>
        /// <returns>The username.</returns>
        private string GetUsernameFromXml(XmlNode xmlData)
        {
            return GetAttributeValue(xmlData, "username");
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


        /// <summary>
        /// Grants the user permission to the section.
        /// </summary>
        /// <param name="xmlData">The XML data.</param>
        /// <returns>
        /// True, if permission was granted; otherwise false.
        /// </returns>
        private bool Grant(XmlNode xmlData)
        {
            return Toggle(xmlData, true);
        }


        /// <summary>
        /// Revokes permission to the section from the user.
        /// </summary>
        /// <param name="xmlData">The XML data.</param>
        /// <returns>
        /// True, if permission was revoked; otherwise, false.
        /// </returns>
        private bool Revoke(XmlNode xmlData)
        {
            return Toggle(xmlData, false);
        }


        /// <summary>
        /// Toggles the user's permission for the section indicated
        /// by the specified XML.
        /// </summary>
        /// <param name="xmlData">The XML data.</param>
        /// <param name="grant">
        /// If true, permission will be granted to the section; otherwise,
        /// permission will be revoked.
        /// </param>
        /// <returns>
        /// True, if the toggle was successful; otherwise, false.
        /// </returns>
        private bool Toggle(XmlNode xmlData, bool grant)
        {

            // Variables.
            var user = GetUserFromConfig(xmlData);
            var sectionName = GetSectionNameFromXml(xmlData);
            var services = ApplicationContext.Current.Services;
            var service = services.UserService;


            // Validate input.
            if (user == null || string.IsNullOrWhiteSpace(sectionName))
            {
                return false;
            }


            // Add or remove section.
            if (grant)
            {
                user.AddAllowedSection(sectionName);
            }
            else
            {
                user.RemoveAllowedSection(sectionName);
            }


            // Save change and indicate success.
            service.Save(user, true);
            return true;

        }


        /// <summary>
        /// Gets the user specified in the XML config.
        /// </summary>
        /// <param name="xmlData">The XML data.</param>
        /// <returns>
        /// The user.
        /// </returns>
        private IUser GetUserFromConfig(XmlNode xmlData)
        {
            var username = GetUsernameFromXml(xmlData);
            if (UsernameIsCurrentUserPlaceHolder(username))
            {
                return GetCurrentUser();
            }
            else
            {
                var services = ApplicationContext.Current.Services;
                var service = services.UserService;
                return service.GetByUsername(username);
            }
        }


        /// <summary>
        /// Gets the currently logged in user.
        /// </summary>
        /// <returns>The current user.</returns>
        public IUser GetCurrentUser()
        {
            var security = UmbracoContext.Current.Security;
            var currentUser = security.CurrentUser;
            return currentUser;
        }


        /// <summary>
        /// Indicates whether or not the specified username is a
        /// special placeholder indicating that the current user
        /// should be used.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>
        /// True, if the placeholder is detected; otherwise, false.
        /// </returns>
        private bool UsernameIsCurrentUserPlaceHolder(string username)
        {
            return "$CurrentUser".InvariantEquals(username);
        }

        #endregion

    }

}