namespace formulate.app.Forms.Fields.Recaptcha
{
    using Helpers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Web.Configuration;
    public class RecaptchaField : IFormFieldType, IFormFieldTypeExtended
    {
        public string Directive => "formulate-recaptcha-field";
        public string TypeLabel => "Recaptcha";
        public string Icon => "icon-formulate-recaptcha";
        public Guid TypeId => new Guid("80C0543D419E4DDFAB052C2D052B97A2");
        public bool IsTransitory => false;
        public bool IsServerSideOnly => false;
        public bool IsHidden => false;
        public bool IsStored => false;
        public bool AlreadyHtmlEncoded => false;
        public object DeserializeConfiguration(string configuration)
        {
            return null;
        }
        public string FormatValue(IEnumerable<string> values, FieldPresentationFormats format,
            object configuration)
        {
            return string.Join(", ", values);
        }
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
    }
}