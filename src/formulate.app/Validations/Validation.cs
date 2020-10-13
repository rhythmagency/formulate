namespace formulate.app.Validations
{

    // Namespaces.
    using core.Types;
    using Entities;
    using Helpers;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Validates a form field.
    /// </summary>
    public class Validation : IEntity
    {
        #region Properties

        /// <summary>
        /// Gets or sets the ID of the validation kind.
        /// </summary>
        public Guid KindId { get; set; }

        /// <summary>
        /// Gets or sets the ID of this validation.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the entity ID path of this validation.
        /// </summary>
        public Guid[] Path { get; set; }

        /// <summary>
        /// Gets or sets the alias of this validation.
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        /// Gets or sets the name of this validation.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the icon for validations.
        /// </summary>
        [JsonIgnore]
        public string Icon => Constants.Trees.Validations.ItemIcon;

        /// <summary>
        /// Gets the kind of this entity.
        /// </summary>
        [JsonIgnore]
        public EntityKind Kind => EntityKind.Validation;

        /// <summary>
        /// Gets or sets the data stored by this validation.
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// Deserializes the configuration data into a C# class instance.
        /// </summary>
        /// <param name="context">
        /// The validation configuration deserialization context.
        /// </param>
        /// <returns>
        /// The deserialized configuration data.
        /// </returns>
        public object DeserializeConfiguration(ValidationContext context)
        {
            var kind = GetValidationKind();
            return kind.DeserializeConfiguration(Data, context);
        }

        /// <summary>
        /// Is the specified value valid?
        /// </summary>
        /// <param name="dataValues">
        /// The data values to test.
        /// </param>
        /// <param name="fileValues">
        /// The file values to test.
        /// </param>
        /// <param name="context">
        /// The validation context.
        /// </param>
        /// <returns>
        /// True, if the values are valid; otherwise, false.
        /// </returns>
        public bool IsValueValid(
            IEnumerable<string> dataValues,
            IEnumerable<FileFieldSubmission> fileValues,
            ValidationContext context)
        {
            var config = DeserializeConfiguration(context);
            var kind = GetValidationKind();
            return kind.IsValueValid(dataValues, fileValues, config);
        }

        /// <summary>
        /// Returns the validation kind.
        /// </summary>
        /// <returns>
        /// The kind of validation.
        /// </returns>
        public IValidationKind GetValidationKind()
        {
            var allKinds = ValidationHelper.GetAllValidationKinds();
            var kind = allKinds.FirstOrDefault(x => x.Id == KindId);
            return kind;
        }

        #endregion
    }
}
