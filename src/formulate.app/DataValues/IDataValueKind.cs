namespace formulate.app.DataValues
{

    // Namespaces.
    using System;

    using Umbraco.Core.Composing;


    /// <summary>
    /// Classes that implement this are the kinds of data values that
    /// can be created.
    /// </summary>
    public interface IDataValueKind : IDiscoverable
    {
        Guid Id { get; }
        string Name { get; }
        string Directive { get; }
    }

}