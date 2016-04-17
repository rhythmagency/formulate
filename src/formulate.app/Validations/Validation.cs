namespace formulate.app.Validations
{

    // Namespaces.
    using Entities;
    using Helpers;
    using Newtonsoft.Json;
    using System;
    using System.Linq;


    /// <summary>
    /// Validates a form field.
    /// </summary>
    public class Validation : IEntity
    {

        #region Properties

        /// <summary>
        /// The ID of the validation kind.
        /// </summary>
        public Guid KindId { get; set; }


        /// <summary>
        /// The ID of this validation.
        /// </summary>
        public Guid Id { get; set; }


        /// <summary>
        /// The entity ID path of this validation.
        /// </summary>
        public Guid[] Path { get; set; }


        /// <summary>
        /// The alias of this validation.
        /// </summary>
        public string Alias { get; set; }


        /// <summary>
        /// The name of this validation.
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// The icon for validations.
        /// </summary>
        [JsonIgnore()]
        public string Icon
        {
            get
            {
                return Constants.Trees.Validations.ItemIcon;
            }
        }


        /// <summary>
        /// The kind of this entity.
        /// </summary>
        [JsonIgnore()]
        public EntityKind Kind
        {
            get
            {
                return EntityKind.Validation;
            }
        }


        /// <summary>
        /// The data stored by this validation.
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