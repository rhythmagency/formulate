namespace formulate.app.Forms
{

    // Namespaces.
    using System;


    /// <summary>
    /// The interface for all form meta information.
    /// </summary>
    public interface IFormMetaInfo
    {
        string Alias { get; set; }
        string Name { get; set; }
        Type ValueType { get; }
    }

}