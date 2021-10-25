using System.Threading;
using System.Threading.Tasks;
using Formulate.Core.DataValues;

namespace Formulate.Core.FormFields
{
    /// <summary>
    /// The abstract class for creating synchronous data values definitions.
    /// </summary>
    /// <remarks>
    /// <para>Acts as wrapper for synchronous data value creation.</para>
    /// <para>All calls are made asynchronously to maintain consistency with asynchronous definitions.</para>
    /// <para>If your form field intends to make use of <see cref="IDataValues"/> you should use <see cref="IFormFieldDefinition"/> or <see cref="FormFieldDefinitionBase"/> to make an asynchronous call instead.</para>
    /// </remarks>
    public abstract class FormFieldDefinition : FormFieldDefinitionBase
    {
        protected abstract IFormField CreateField(IFormFieldSettings settings);

        public override async Task<IFormField> CreateFieldAsync(IFormFieldSettings settings, CancellationToken cancellationToken = default)
        {
            return await Task.Run(() => CreateField(settings), cancellationToken);
        }
    }
}