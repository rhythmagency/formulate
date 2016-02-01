namespace formulate.app.Layouts
{

    // Namespaces.
    using System;


    /// <summary>
    /// Classes that implement this are the kinds of layouts that
    /// can be created.
    /// </summary>
    public interface ILayoutKind
    {
        Guid Id { get; }
        string Name { get; }
        string Directive { get; }
    }

}