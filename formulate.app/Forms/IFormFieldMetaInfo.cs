namespace formulate.app.Forms
{

    // Namespaces.
    using System;


    /// <summary>
    /// The interface for all form field meta information.
    /// </summary>
    public interface IFormFieldMetaInfo
    {
        string Alias { get; set; }
        string Name { get; set; }
        Type ValueType { get; }
    }

}