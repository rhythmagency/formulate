namespace formulate.app.Validations
{

    // Namespaces.
    using core.Types;
    using System;
    using System.Collections.Generic;


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
        bool IsValueValid(IEnumerable<string> dataValues, IEnumerable<FileFieldSubmission> fileValues,
            object configuration);
    }

}