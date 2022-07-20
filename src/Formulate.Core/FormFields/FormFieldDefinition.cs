namespace Formulate.Core.FormFields
{
    // Namespaces.
    using DataValues;

    /// <summary>
    /// The abstract class for creating synchronous data values definitions.
    /// </summary>
    /// <remarks>
    /// <para>Acts as wrapper for synchronous data value creation.</para>
    /// <para>All calls are made asynchronously to maintain consistency with asynchronous definitions.</para>
    /// <para>If your form field intends to make use of <see cref="IDataValues"/> you should use <see cref="IFormFieldDefinition"/> or <see cref="FormFieldDefinitionBase"/> to make an asynchronous call instead.</para>
    /// </remarks>
    public abstract class FormFieldDefinition<TField> : FormFieldDefinitionBase<TField>
        where TField : IFormField
    {
    }
}