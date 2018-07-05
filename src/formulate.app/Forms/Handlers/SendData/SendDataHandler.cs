namespace formulate.app.Forms.Handlers.SendData
{

    // Namespaces.
    using Helpers;
    using Managers;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Resolvers;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Web;
    using System.Web.Configuration;
    using Umbraco.Core;
    using Umbraco.Core.Logging;
    using Umbraco.Web;


    /// <summary>
    /// A handler that sends a data to a web API.
    /// </summary>
    public class SendDataHandler : IFormHandlerType
    {

        #region Constants

        private const string WebUserAgent = "Formulate, an Umbraco Form Builder";
        private const string SendDataError = "An error occurred during an attempt to send data to an external URL.";


        #endregion


        #region Private Properties

        /// <summary>
        /// Configuration manager.
        /// </summary>
        private IConfigurationManager Config
        {
            get
            {
                return Configuration.Current.Manager;
            }
        }

        #endregion


        #region Public Properties

        /// <summary>
        /// The Angular directive that renders this handler.
        /// </summary>
        public string Directive => "formulate-send-data-handler";


        /// <summary>
        /// The icon shown in the picker dialog.
        /// </summary>
        public string Icon => "icon-formulate-send-data";


        /// <summary>
        /// The ID that uniquely identifies this handler (useful for serialization).
        /// </summary>
        public Guid TypeId => new Guid("C76E8D1D5DF244CB8FA285C32312D688");


        /// <summary>
        /// The label that appears when the user is choosing the handler.
        /// </summary>
        public string TypeLabel => "Send Data";

        #endregion


        #region Public Methods

        /// <summary>
        /// Deserializes the configuration for a send data handler.
        /// </summary>
        /// <param name="configuration">
        /// The serialized configuration.
        /// </param>
        /// <returns>
        /// The deserialized configuration.
        /// </returns>
        public object DeserializeConfiguration(string configuration)
        {

            // Variables.
            var fields = new List<FieldMapping>();
            var config = new SendDataConfiguration()
            {
                Fields = fields
            };
            var configData = JsonHelper.Deserialize<JObject>(configuration);
            var dynamicConfig = configData as dynamic;
            var properties = configData.Properties().Select(x => x.Name);
            var propertySet = new HashSet<string>(properties);
            var handlerTypes = ReflectionHelper
                .GetTypesImplementingInterface<IHandleSendDataResult>();


            // Get field mappings.
            if (propertySet.Contains("fields"))
            {
                foreach (var field in dynamicConfig.fields)
                {
                    fields.Add(new FieldMapping()
                    {
                        FieldId = GuidHelper.GetGuid(field.id.Value as string),
                        FieldName = field.name.Value as string
                    });
                }
            }


            // Set the function that handles the result.
            if (propertySet.Contains("resultHandler"))
            {
                var strHandler = dynamicConfig.resultHandler.Value as string;
                var handlerType = handlerTypes
                    .FirstOrDefault(x => x.AssemblyQualifiedName == strHandler);
                var resultHandler = default(IHandleSendDataResult);
                if (handlerType != null)
                {
                    resultHandler = Activator.CreateInstance(handlerType) as IHandleSendDataResult;
                }
                config.ResultHandler = resultHandler;
            }


            // Get simple properties.
            if (propertySet.Contains("url"))
            {
                config.Url = dynamicConfig.url.Value as string;
            }
            if (propertySet.Contains("method"))
            {
                config.Method = dynamicConfig.method.Value as string;
            }
            if (propertySet.Contains("transmissionFormat"))
            {
                config.TransmissionFormat = dynamicConfig.transmissionFormat.Value as string;
            }


            // Return the send data configuration.
            return config;

        }


        /// <summary>
        /// Prepares to handle to form submission.
        /// </summary>
        /// <param name="context">
        /// The form submission context.
        /// </param>
        /// <param name="configuration">
        /// The handler configuration.
        /// </param>
        /// <remarks>
        /// In this case, no preparation is necessary.
        /// </remarks>
        public void PrepareHandleForm(FormSubmissionContext context, object configuration)
        {
        }


        /// <summary>
        /// Handles a form submission (sends data to a web API).
        /// </summary>
        /// <param name="context">
        /// The form submission context.
        /// </param>
        /// <param name="configuration">
        /// The handler configuration.
        /// </param>
        public void HandleForm(FormSubmissionContext context, object configuration)
        {

            // Variables.
            var config = configuration as SendDataConfiguration;
            var form = context.Form;
            var data = context.Data;
            var result = default(SendDataResult);


            // Convert lists into dictionary.
            var fieldsById = form.Fields.ToDictionary(x => x.Id, x => x);
            var valuesById = data.GroupBy(x => x.FieldId).Select(x => new
            {
                Id = x.Key,
                Values = x.SelectMany(y => y.FieldValues).ToList()
            }).ToDictionary(x => x.Id, x => x.Values);


            // Attempts to get a field value.
            Func<Guid, string> tryGetValue = fieldId =>
            {
                var tempValues = default(List<string>);
                var tempField = default(IFormField);
                var hasValues = valuesById.TryGetValue(fieldId, out tempValues);
                var hasField = fieldsById.TryGetValue(fieldId, out tempField);
                if (hasField && (hasValues || tempField.IsServerSideOnly))
                {
                    tempValues = hasValues
                        ? tempValues
                        : null;
                    return tempField.FormatValue(tempValues, FieldPresentationFormats.Transmission);
                }
                return null;
            };


            // Get the data to transmit.
            var transmissionData = config.Fields
                .Where(x => fieldsById.ContainsKey(x.FieldId))
                .Select(x => new KeyValuePair<string, string>(x.FieldName, tryGetValue(x.FieldId)))
                .Where(x => x.Value != null)
                .ToArray();


            // Query string format?
            if ("Query String".InvariantEquals(config.TransmissionFormat))
            {
                result = SendData(config.Url, transmissionData, config.Method, false, false);
            }
            if ("Form Body".InvariantEquals(config.TransmissionFormat))
            {
                result = SendData(config.Url, transmissionData, config.Method, true, false);
            }
            if ("JSON".InvariantEquals(config.TransmissionFormat))
            {
                result = SendData(config.Url, transmissionData, config.Method, true, true);
            }

            // Call function to handle result?
            if (context != null)
            {
                result.Context = context;
            }
            config?.ResultHandler?.HandleResult(result);

        }

        #endregion


        #region Private Methods

        /// <summary>
        /// Sends a web request with the data either in the query string or in the body.
        /// </summary>
        /// <param name="url">
        /// The URL to send the request to.
        /// </param>
        /// <param name="data">
        /// The data to send.
        /// </param>
        /// <param name="method">
        /// The HTTP method (e.g., GET, POST) to use when sending the request.
        /// </param>
        /// <param name="sendInBody">
        /// Send the data as part of the body (or in the query string)?
        /// </param>
        /// <param name="sendJson">
        /// Send the data as json
        /// </param>
        /// <returns>
        /// True, if the request was a success; otherwise, false.
        /// </returns>
        /// <remarks>
        /// Parts of this function are from: http://stackoverflow.com/a/9772003/2052963
        /// and http://stackoverflow.com/questions/14702902
        /// </remarks>
        private SendDataResult SendData(string url, IEnumerable<KeyValuePair<string, string>> data,
            string method, bool sendInBody, bool sendJson)
        {

            // Construct a URL, possibly containing the data as query string parameters.
            var sendInUrl = !sendInBody;
            var sendDataResult = new SendDataResult();
            var uri = new Uri(url);
            var bareUrl = uri.GetLeftPart(UriPartial.Path);
            var keyValuePairs = data as KeyValuePair<string, string>[] ?? data.ToArray();
            var strQueryString = ConstructQueryString(uri, keyValuePairs);
            var hasQueryString = !string.IsNullOrWhiteSpace(strQueryString);
            var requestUrl = hasQueryString && sendInUrl
                ? $"{bareUrl}?{strQueryString}"
                : url;
            var enableLogging = WebConfigurationManager.AppSettings["Formulate:EnableLogging"];

            // Attempt to send the web request.
            try
            {

                // Construct web request.
                var request = (HttpWebRequest)WebRequest.Create(requestUrl);
                request.AllowAutoRedirect = false;
                request.UserAgent = WebUserAgent;
                request.Method = method;

                // Send the data in the body (rather than the query string)?
                if (sendInBody)
                {
                    if (sendJson)
                    {
                        // if sending as json
                        // Group duplicate Key Name Pair values
                        var grouped = keyValuePairs.GroupBy(x => x.Key).Select(x => new
                        {
                            Key = x.Key,
                            Value = x.Select(y => y.Value)
                        }).ToDictionary(x => x.Key,
                            x => x.Value.Count() > 1 ? x.Value.ToArray() as object : x.Value.FirstOrDefault());
                        ;
                        var json = JsonConvert.SerializeObject(new[] {grouped});

                        // log json being sent
                        if (enableLogging == "true")
                        {
                            LogHelper.Info<SendDataHandler>("Sent URL: " + url);
                            LogHelper.Info<SendDataHandler>("Sent Data: " + json);
                        }

                        var postBytes = Encoding.UTF8.GetBytes(json);
                        request.ContentType = "application/json; charset=utf-8";
                        request.ContentLength = postBytes.Length;
                        using (var postStream = request.GetRequestStream())
                        {
                            postStream.Write(postBytes, 0, postBytes.Length);
                        }
                    }
                    else
                    {
                        // log data being sent
                        if (enableLogging == "true")
                        {
                            LogHelper.Info<SendDataHandler>("Sent URL: " + url);
                            LogHelper.Info<SendDataHandler>("Sent Data: " + strQueryString);
                        }

                        var postBytes = Encoding.UTF8.GetBytes(strQueryString);
                        request.ContentType = "application/x-www-form-urlencoded";
                        request.ContentLength = postBytes.Length;
                        var postStream = request.GetRequestStream();
                        postStream.Write(postBytes, 0, postBytes.Length);
                    }
                }


                // Get and retain response.
                var response = (HttpWebResponse)request.GetResponse();
                sendDataResult.HttpWebResponse = response;
                var responseStream = response.GetResponseStream();
                var reader = new StreamReader(responseStream);
                var resultText = reader.ReadToEnd();
                sendDataResult.ResponseText = resultText;
                sendDataResult.Success = true;

            }
            catch (Exception ex)
            {
                LogHelper.Error<SendDataHandler>(SendDataError, ex);
                sendDataResult.ResponseError = ex;
                sendDataResult.Success = false;
            }


            // Return the result of the request.
            return sendDataResult;

        }

        /// <summary>
        /// Constructs a query string from the specified URL and data.
        /// </summary>
        /// <param name="uri">
        /// The URL (potentially containing a query string).
        /// </param>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// The query string.
        /// </returns>
        private string ConstructQueryString(Uri uri, IEnumerable<KeyValuePair<string, string>> data)
        {
            var queryString = HttpUtility.ParseQueryString(uri.Query);
            foreach (var pair in data)
            {
                queryString.Set(pair.Key, pair.Value);
            }
            var strQueryString = queryString.ToString();
            return strQueryString;
        }

        #endregion

    }

}