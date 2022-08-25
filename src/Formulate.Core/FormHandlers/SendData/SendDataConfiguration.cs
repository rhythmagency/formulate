namespace Formulate.Core.FormHandlers.SendData
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