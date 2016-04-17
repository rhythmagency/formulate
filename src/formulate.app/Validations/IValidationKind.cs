namespace formulate.app.Validations
{

    // Namespaces.
    using System;


    /// <summary>
    /// Classes that implement this are the kinds of validations that
    /// can be created.
    /// </summary>
    public interface IValidationKind
    {
        Guid Id { get; }
        string Name { get; }
        string Directive { get; }
        object DeserializeConfiguration(string configuration, ValidationContext context);
    }

}