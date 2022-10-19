namespace Formulate.Extensions.SendData.FormHandlers
{
    using Formulate.Core.FormHandlers;
    using Formulate.Core.Utilities;
    using System;
    using System.Net.Http;

    /// <summary>
    /// The data definition for a form handler that sends data to a URL.
    /// </summary>
    public sealed class SendDataFormHandlerDefinition : FormHandlerDefinition
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private readonly IJsonUtility _jsonUtility;

        public SendDataFormHandlerDefinition(IHttpClientFactory httpClientFactory, IJsonUtility jsonUtility)
        {
            _httpClientFactory = httpClientFactory;
            _jsonUtility = jsonUtility;
        }

        /// <summary>
        /// Constants related to <see cref="SendDataFormHandlerDefinition"/>.
        /// </summary>
        public static class Constants
        {
            /// <summary>
            /// The kind ID.
            /// </summary>
            public const string KindId = "C76E8D1D5DF244CB8FA285C32312D688";

            /// <summary>
            /// The name.
            /// </summary>
            public const string Name = "Send Data";

            /// <summary>
            /// The icon.
            /// </summary>
            public const string Icon = "icon-formulate-send-data";

            /// <summary>
            /// The Angular JS directive.
            /// </summary>
            public const string Directive = "formulate-send-data-handler";
        }

        /// <inheritdoc />
        public override string Category => FormHandlerConstants.Categories.Notify;

        /// <inheritdoc />
        public override string Directive => Constants.Directive;

        /// <inheritdoc />
        public override string Name => Constants.Name;

        /// <inheritdoc />
        public override Guid KindId => Guid.Parse(Constants.KindId);

        /// <inheritdoc />
        public override string Icon => Constants.Icon;

        /// <inheritdoc />
        public override IFormHandler CreateHandler(IFormHandlerSettings settings)
        {
            var configuration = _jsonUtility.Deserialize<SendDataConfiguration>(settings.Data);

            var handler = new SendDataFormHandler(settings, configuration, _httpClientFactory);

            return handler;
        }

        /// <inheritdoc />
        public override object GetBackOfficeConfiguration(IFormHandlerSettings settings)
        {
            return _jsonUtility.Deserialize<SendDataConfiguration>(settings.Data);
        }
    }
}