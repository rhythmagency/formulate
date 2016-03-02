namespace formulate.core.Exceptions
{

    // Namespaces.
    using System;


    /// <summary>
    /// An exception to be used when a view is not found.
    /// </summary>
    public class ViewNotFoundException : Exception
    {

        #region Variables

        private string viewPath;

        #endregion


        #region Properties

        /// <summary>
        /// The path to the view that was not found.
        /// </summary>
        public string ViewPath
        {
            get
            {
                return viewPath;
            }
        }

        #endregion


        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ViewNotFoundException()
        {
        }


        /// <summary>
        /// Primary constructor.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="viewPath">The path to the view that was not found.</param>
        public ViewNotFoundException(string message, string viewPath)
        : base(message)
        {
            this.viewPath = viewPath;
        }

        #endregion

    }

}