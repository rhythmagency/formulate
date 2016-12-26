namespace formulate.app.Forms
{

    // Namespaces.
    using System;
    using System.Collections.Generic;


    /// <summary>
    /// Interface for all form fields.
    /// </summary>
    public interface IFormField
    {
        Guid TypeId { get; set; }
        Guid Id { get; set; }
        string Alias { get; set; }
        string Name { get; set; }
        string Label { get; set; }
        string Category { get; set; }
        string FieldConfiguration { get; set; }
        Guid[] Validations { get; set; }
        IFormFieldMetaInfo[] MetaInfo { get; set; }
        string GetIcon();
        string GetDirective();
        string GetTypeLabel();
        Type GetFieldType();
        object DeserializeConfiguration();
        string FormatValue(IEnumerable<string> values, FieldPresentationFormats format);
        bool IsTransitory { get; }
    }

}