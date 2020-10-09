namespace formulate.app.Forms.Fields.Recaptcha
{
    using Helpers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Web.Configuration;

    /// <summary>
    /// A Google reCAPTCHA form field type.
    /// </summary>
    public class RecaptchaField : IFormFieldType, IFormFieldTypeExtended
    {
        /// <inheritdoc />
        public string Directive => "formulate-recaptcha-field";

        /// <inheritdoc />
        public string TypeLabel => "Recaptcha";

        /// <inheritdoc />
        public string Icon => "icon-formulate-recaptcha";

        /// <inheritdoc />
        public Guid TypeId => new Guid("80C0543D419E4DDFAB052C2D052B97A2");

        /// <inheritdoc />
        public bool IsTransitory => false;

        /// <inheritdoc />
        public bool IsServerSideOnly => false;

        /// <inheritdoc />
        public bool IsHidden => false;

        /// <inheritdoc />
        public bool IsStored => false;

        /// <inheritdoc />
        public object DeserializeConfiguration(string configuration)
        {
            return null;
        }

        /// <inheritdoc />
        public string FormatValue(IEnumerable<string> values, FieldPresentationFormats format, object configuration)
        {
            return string.Join(", ", values);
        }

        /// <inheritdoc />
        public bool IsValid(IEnumerable<string> value)
        {
            if (value == null || value.Count() != 1)
            {
                return false;
            }
            var key = WebConfigurationManager.AppSettings["Formulate:RecaptchaSecretKey"];
            var client = new WebClient();
            var encodedKey = WebUtility.UrlEncode(key);
            var encodedValue = WebUtility.UrlEncode(value.First());
            var url = $"https://www.google.com/recaptcha/api/siteverify?secret={encodedKey}&response={encodedValue}";
            var response = client.DownloadString(url);
            var decoded = JsonHelper.Deserialize<dynamic>(response);
            var validResponse = (decoded.success.Value as bool?).GetValueOrDefault(false);
            return validResponse;
        }

        /// <summary>
        /// Returns the validation message that is native to this field.
        /// </summary>
        /// <returns>
        /// The validation error message.
        /// </returns>
        public string GetNativeFieldValidationMessage()
        {
            return "Recaptcha failed.";
        }
    }
}
