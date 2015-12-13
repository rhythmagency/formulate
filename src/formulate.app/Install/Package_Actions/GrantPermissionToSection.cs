namespace formulate.app.Install.Package_Actions
{

    // Namespaces.
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml;
    using umbraco.cms.businesslogic.packager.standardPackageActions;
    using umbraco.interfaces;
    using Umbraco.Core;
    using Umbraco.Core.Models.Membership;
    using Umbraco.Web;
    using MetaConstants = meta.Constants;
    using Resources = formulate.app.Properties.Resources;
    using XmlHelper = Helpers.XmlHelper;


    /// <summary>
    /// Umbraco package action that grants permission to the specified
    /// section for the specified user.
    /// </summary>
    /// <remarks>
    /// If the specified username is "$CurrentUser", the current user
    /// will be used. If the specified username is "$AllUsers", all
    /// users will be used.
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
            var packageName = MetaConstants.PackageName;
            var actionName = typeof(GrantPermissionToSection).Name;
            var aliasFormat = "{0}.{1}";
            return string.Format(aliasFormat, packageName, actionName);
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
        /// Sample XML that can be used to invoke this package action.
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
            var users = GetUsersFromConfig(xmlData).ToList();
            var sectionName = GetSectionNameFromXml(xmlData);
            var services = ApplicationContext.Current.Services;
            var service = services.UserService;


            // Validate input.
            if (string.IsNullOrWhiteSpace(sectionName) || users == null || !users.Any())
            {
                return false;
            }


            // Add or remove section.
            foreach(var user in users)
            {
                if (grant)
                {
                    user.AddAllowedSection(sectionName);
                }
                else
                {
                    user.RemoveAllowedSection(sectionName);
                }
            }


            // Save change and indicate success.
            service.Save(users, true);
            return true;

        }


        /// <summary>
        /// Gets the users specified in the XML config.
        /// </summary>
        /// <param name="xmlData">The XML data.</param>
        /// <returns>
        /// The users.
        /// </returns>
        private IEnumerable<IUser> GetUsersFromConfig(XmlNode xmlData)
        {
            var username = GetUsernameFromXml(xmlData);
            if (UsernameIsCurrentUserPlaceHolder(username))
            {
                yield return GetCurrentUser();
            }
            else if(UsernameIsAllUsersPlaceholder(username))
            {
                var services = ApplicationContext.Current.Services;
                var service = services.UserService;
                var total = default(int);
                foreach(var user in service.GetAll(0, 100, out total))
                {
                    yield return user;
                }
            }
            else
            {
                var services = ApplicationContext.Current.Services;
                var service = services.UserService;
                yield return service.GetByUsername(username);
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


        /// <summary>
        /// Indicates whether or not the specified username is a
        /// special placeholder indicating that all users should
        /// be used.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>
        /// True, if the placeholder is detected; otherwise, false.
        /// </returns>
        private bool UsernameIsAllUsersPlaceholder(string username)
        {
            return "$AllUsers".InvariantEquals(username);
        }

        #endregion

    }

}