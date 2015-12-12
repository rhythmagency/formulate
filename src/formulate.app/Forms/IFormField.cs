namespace formulate.app.Forms
{

    // Namespaces.
    using System;


    /// <summary>
    /// Interface for all form fields.
    /// </summary>
    public interface IFormField
    {
        Guid Id { get; set; }
        string Alias { get; set; }
        string Name { get; set; }
        string Label { get; set; }
        string FieldConfiguration { get; set; }
        IFormFieldMetaInfo[] MetaInfo { get; set; }
        string GetDirective();
        string GetTypeLabel();
    }

}