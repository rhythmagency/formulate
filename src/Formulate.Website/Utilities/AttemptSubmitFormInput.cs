namespace Formulate.Website.Utilities
{
    using Microsoft.AspNetCore.Http;

    public sealed class AttemptSubmitFormInput
    {
        public AttemptSubmitFormInput(Guid formId, int pageId, IFormCollection form, IFormFileCollection files)
        {
            Files = files;
            Form = form;
            FormId = formId;
            PageId = pageId;
        }

        public IFormFileCollection Files { get; init; }

        public IFormCollection Form { get; init; }

        public Guid FormId { get; init; }

        public int PageId { get; init; }
    }
}
