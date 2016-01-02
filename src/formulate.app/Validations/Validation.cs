namespace formulate.app.Validations
{

    // Namespaces.
    using Entities;
    using Newtonsoft.Json;
    using System;


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

        #endregion

    }

}