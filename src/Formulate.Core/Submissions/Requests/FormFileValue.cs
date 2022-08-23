namespace Formulate.Core.Submissions.Requests
{
    public readonly struct FormFileValue
    {
        public byte[] Data { get; init; }

        public string Name { get; init; }

        public string ContentType { get; init; }
    }
}
