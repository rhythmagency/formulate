namespace formulate.app.Validations
{

    // Namespaces.
    using core.Types;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A contract for creating a form field validation rule.
    /// </summary>
    public interface IValidationKind
    {
        /// <summary>
        /// Gets the id.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the directive.
        /// </summary>
        string Directive { get; }

        /// <summary>
        /// The deserialize configuration.
        /// </summary>
        /// <param name="configuration">
        /// The configuration.
        /// </param>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        object DeserializeConfiguration(string configuration, ValidationContext context);

        /// <summary>
        /// The is value valid.
        /// </summary>
        /// <param name="dataValues">
        /// The data values.
        /// </param>
        /// <param name="fileValues">
        /// The file values.
        /// </param>
        /// <param name="configuration">
        /// The configuration.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool IsValueValid(
            IEnumerable<string> dataValues,
            IEnumerable<FileFieldSubmission> fileValues,
            object configuration);
    }
}
