namespace Formulate.Extensions.SendData.FormHandlers
{
    using System.Runtime.Serialization;

    [DataContract]
    public sealed class SendDataConfiguration
    {
        [DataMember(Name = "httpClientName")]
        public string HttpClientName { get; set; }

        public string EndpointUrl { get; internal set; }
    }
}