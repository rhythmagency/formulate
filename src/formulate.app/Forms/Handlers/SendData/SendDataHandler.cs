namespace formulate.app.Forms.Handlers.SendData
{

    // Namespaces.
    using Helpers;
    using Managers;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
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

        #region Delegates

        /// <summary>
        /// Delegate used when form data is sending.
        /// </summary>
        /// <param name="context">
        /// The sending data context.
        /// </param>
        public delegate void SendingDataEvent(SendingDataContext context);

        /// <summary>
        /// Delegate used when serializing JSON.
        /// </summary>
        /// <param name="data">
        /// The form data passed to the SendData method.
        /// </param>
        /// <returns>
        /// The serialized JSON value, which will be used rather than the built-in serialized JSON.
        /// </returns>
        public delegate string SerializingJsonEvent(IEnumerable<KeyValuePair<string, string>> data);

        #endregion


        #region Events

        /// <summary>
        /// Event raised when form data is being sent.
        /// </summary>
        /// <remarks>
        /// Subscribing to this gives you the opportunity to alter data sent. This is
        /// useful, for example, if you need to change the data or the headers.
        /// </remarks>
        public static event SendingDataEvent SendingData;

        /// <summary>
        /// Event raised when form data is being serialized to json.
        /// </summary>
        /// <remarks>
        /// Subscribing to this gives you the opportunity to alter data sent. This is
        /// useful, for example, if you need to make custom JSON serialization.
        /// </remarks>
        public static event SerializingJsonEvent SerializingJson;

        #endregion


        #region Private Properties

        /// <summary>
        /// Configuration manager.
        /// </summary>
        private IConfigurationManager Config { get; set; }

        private ILogger Logger { get; set; }

        #endregion

        public SendDataHandler(IConfigurationManager configurationManager, ILogger logger)
        {
            Config = configurationManager;
            Logger = logger;
        }


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
                result = SendData(config, context, transmissionData, false, false);
            }
            if ("Form Body".InvariantEquals(config.TransmissionFormat))
            {
                result = SendData(config, context, transmissionData, true, false);
            }
            if ("JSON".InvariantEquals(config.TransmissionFormat))
            {
                result = SendData(config, context, transmissionData, true, true);
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
        /// <param name="config">
        /// The configuration for the data to be sent (e.g., contains the URL and request method).
        /// </param>
        /// <param name="context">
        /// The context for the current form submission.
        /// </param>
        /// <param name="data">
        /// The data to send.
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
        private SendDataResult SendData(SendDataConfiguration config, FormSubmissionContext context,
            IEnumerable<KeyValuePair<string, string>> data, bool sendInBody, bool sendJson)
        {

            // Construct a URL, possibly containing the data as query string parameters.
            var sendInUrl = !sendInBody;
            var sendDataResult = new SendDataResult();
            var uri = new Uri(config.Url);
            var bareUrl = uri.GetLeftPart(UriPartial.Path);
            var keyValuePairs = data as KeyValuePair<string, string>[] ?? data.ToArray();
            var strQueryString = ConstructQueryString(uri, keyValuePairs);
            var hasQueryString = !string.IsNullOrWhiteSpace(strQueryString);
            var requestUrl = hasQueryString && sendInUrl
                ? $"{bareUrl}?{strQueryString}"
                : config.Url;
            var enableLogging = WebConfigurationManager.AppSettings["Formulate:EnableLogging"];
            var jsonMode = WebConfigurationManager.AppSettings["Formulate:Send Data JSON Mode"];
            var isJsonObjectMode = "JSON Object".InvariantEquals(jsonMode);
            var isWrappedObjectMode = "Wrap JSON Object in Array".InvariantEquals(jsonMode);


            // Attempt to send the web request.
            try
            {

                // Construct web request.
                var request = (HttpWebRequest)WebRequest.Create(requestUrl);
                request.AllowAutoRedirect = false;
                request.UserAgent = WebUserAgent;
                request.Method = config.Method;


                // Send an event indicating that the data is about to be sent (which allows code
                // external to Formulate to modify the request).
                var sendContext = new SendingDataContext()
                {
                    Configuration = config,
                    Data = keyValuePairs,
                    Request = request,
                    SubmissionContext = context
                };
                SendingData?.Invoke(sendContext);


                // Update the key/value pairs in case they got changed in the sending data event.
                // This only updates the methods that send the data in the body (as the URL has
                // already been set on the request).
                keyValuePairs = sendContext.Data as KeyValuePair<string, string>[]
                    ?? sendContext.Data.ToArray();


                // Send the data in the body (rather than the query string)?
                if (sendInBody)
                {
                    if (sendJson)
                    {

                        // Variables.
                        var json = default(string);


                        // Send an event indicating that the data is about to be serialized to
                        // JSON (which allows code external to Formulate to modify the JSON)?
                        if (SerializingJson != null)
                        {
                            json = SerializingJson.Invoke(sendContext.Data);
                        }
                        else
                        {

                            // If sending as JSON, group duplicate keys/value pairs.
                            var grouped = keyValuePairs.GroupBy(x => x.Key).Select(x => new
                            {
                                Key = x.Key,
                                Value = x.Select(y => y.Value)
                            }).ToDictionary(x => x.Key,
                                x => x.Value.Count() > 1 ? x.Value.ToArray() as object : x.Value.FirstOrDefault());


                            // Convert data to JSON.
                            if (isJsonObjectMode)
                            {
                                json = JsonConvert.SerializeObject(grouped);
                            }
                            else if (isWrappedObjectMode)
                            {
                                json = JsonConvert.SerializeObject(new[] { grouped });
                            }
                            else
                            {

                                // Ideally, we can remove this "else" branch later. We never really want this
                                // mode to be used, but it's here for legacy support in case anybody managed
                                // to make use of this funky mode.
                                json = JsonConvert.SerializeObject(new[] { grouped });

                            }

                        }


                        // Log JSON being sent.
                        if (enableLogging == "true")
                        {
                            Logger.Info<SendDataHandler>("Sent URL: " + config.Url);
                            Logger.Info<SendDataHandler>("Sent Data: " + JsonHelper.FormatJsonForLogging(json));
                        }


                        // Write JSON data to the request request.
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

                        // Log data being sent.
                        if (enableLogging == "true")
                        {
                            Logger.Info<SendDataHandler>("Sent URL: " + config.Url);
                            Logger.Info<SendDataHandler>("Sent Data: " + strQueryString);
                        }


                        // Update the data (since the sending data event may have modified it).
                        uri = new Uri(config.Url);
                        strQueryString = ConstructQueryString(uri, keyValuePairs);


                        // Write the data to the request stream.
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
                Logger.Error<SendDataHandler>(ex, SendDataError);
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
