namespace formulate.app.Resolvers
{

    // Namespaces.
    using Persistence;
    using Umbraco.Core.ObjectResolution;


    /// <summary>
    /// The resolver for the form persistence manager.
    /// </summary>
    public class FormPersistence
        : SingleObjectResolverBase<FormPersistence, IFormPersistence>
    {

        #region Properties

        /// <summary>
        /// Gets the form persistence manager.
        /// </summary>
        public IFormPersistence Manager
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
        /// The form persistence manager to use.
        /// </param>
        internal FormPersistence(IFormPersistence persistence)
            : base(persistence)
        {
        }

        #endregion


        #region Methods

        /// <summary>
        /// Sets the form persistence manager.
        /// </summary>
        /// <param name="manager">
        /// The new form persistence manager.
        /// </param>
        public void SetFormPersistence(IFormPersistence persistence)
        {
            Value = persistence;
        }

        #endregion

    }

}