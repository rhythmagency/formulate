namespace formulate.app.Resolvers
{

    // Namespaces.
    using Persistence;
    using Umbraco.Core.ObjectResolution;


    /// <summary>
    /// The resolver for the validation persistence manager.
    /// </summary>
    public class ValidationPersistence
        : SingleObjectResolverBase<ValidationPersistence,
        IValidationPersistence>
    {

        #region Properties

        /// <summary>
        /// Gets the validation persistence manager.
        /// </summary>
        public IValidationPersistence Manager
        {
            get
            {
                return Value;
            }
        }

        #endregion


        #region Constructors

        /// <summary>
        /// Primary constructor.
        /// </summary>
        /// <param name="manager">
        /// The validation persistence manager to use.
        /// </param>
        internal ValidationPersistence(IValidationPersistence persistence)
            : base(persistence)
        {
        }

        #endregion


        #region Methods

        /// <summary>
        /// Sets the validation persistence manager.
        /// </summary>
        /// <param name="manager">
        /// The new validation persistence manager.
        /// </param>
        public void SetValidationPersistence(
            IValidationPersistence persistence)
        {
            Value = persistence;
        }

        #endregion

    }

}