namespace Formulate.BackOffice.Controllers
{
    public sealed class DeleteEntityResponse
    {
        public string[] DeletedEntityIds { get; set; }

        public string[] ParentPath { get; set; }
    }
}