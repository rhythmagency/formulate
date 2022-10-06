namespace Formulate.Web.Utilities
{
    using System.Threading.Tasks;

    public interface IAttemptSubmitForm
    {
        Task<AttemptSubmitFormOutput> SubmitAsync(AttemptSubmitFormInput input, CancellationToken cancellationToken);
    }
}
