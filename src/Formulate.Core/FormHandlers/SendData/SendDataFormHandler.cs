namespace Formulate.Core.FormHandlers.SendData
{
    using Formulate.Core.Submissions.Requests;
    // Namespaces.
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Sends form submission data to a URL.
    /// </summary>
    internal sealed class SendDataFormHandler : AsyncFormHandler
    {
        private readonly HttpClient _httpClient;
        private readonly SendDataConfiguration _configuration;

        public SendDataFormHandler(IFormHandlerSettings settings, SendDataConfiguration configuration, IHttpClientFactory httpClientFactory) : base(settings)
        {
            _configuration = configuration;

            if (string.IsNullOrWhiteSpace(_configuration.HttpClientName))
            {
                _httpClient = httpClientFactory.CreateClient(_configuration.HttpClientName);
            }
            else
            {
                _httpClient = httpClientFactory.CreateClient();
            }
        }

        public override async Task HandleAsync(FormSubmissionRequest submission,
            CancellationToken cancellationToken = default)
        {
            var content = "{\"message\": \"Hello world!\"}";
                        
            var message = new HttpRequestMessage(HttpMethod.Post, _configuration.EndpointUrl)
            {
                Content = new StringContent(content, System.Text.Encoding.UTF8, "application/json")
            };

            await _httpClient.SendAsync(message, cancellationToken);
        }
    }
}