namespace formulate.app.DataValues
{

    // Namespaces.
    using System;


    /// <summary>
    /// Classes that implement this are the kinds of data values that
    /// can be created.
    /// </summary>
    public interface IDataValueKind
    {
        Guid Id { get; }
        string Name { get; }
        string Directive { get; }
    }

}