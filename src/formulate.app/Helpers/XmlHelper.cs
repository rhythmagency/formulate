namespace formulate.app.Helpers
{

    // Namespaces.
    using System;
    using System.Xml;


    /// <summary>
    /// Helps with XML.
    /// </summary>
    public class XmlHelper
    {

        #region Methods

        /// <summary>
        /// Gets the value from an attribute (or returns an empty string if it wasn't specified).
        /// </summary>
        public static string GetAttributeValueFromNode(XmlNode node, string attributeName)
        {
            return GetAttributeValueFromNode<string>(node, attributeName, string.Empty);
        }


        /// <summary>
        /// Gets the value from an attribute (or returns defaultValue if it wasn't specified).
        /// </summary>
        public static string GetAttributeValueFromNode(XmlNode node, string attributeName, string defaultValue)
        {
            return GetAttributeValueFromNode<string>(node, attributeName, defaultValue);
        }


        /// <summary>
        /// Gets the value from an attribute. If no value or empty, it returns your default value (everything converted to the right type).
        /// </summary>
        public static T GetAttributeValueFromNode<T>(XmlNode node, string attributeName, T defaultValue)
        {
            if (node.Attributes[attributeName] != null)
            {
                var result = node.Attributes[attributeName].InnerText;
                if (string.IsNullOrWhiteSpace(result))
                {
                    return defaultValue;
                }
                return (T)Convert.ChangeType(result, typeof(T));
            }
            return defaultValue;
        }

        #endregion

    }

}