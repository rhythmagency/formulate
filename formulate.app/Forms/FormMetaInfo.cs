namespace formulate.app.Forms
{

    // Namespaces.
    using System;


    /// <summary>
    /// Information attached to a form.
    /// </summary>
    public class FormMetaInfo<T> : IFormMetaInfo
    {

        #region Properties

        /// <summary>
        /// The alias of this form meta information.
        /// </summary>
        public string Alias { get; set; }


        /// <summary>
        /// The name of this form meta information.
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// The data stored by this form meta information.
        /// </summary>
        public T Data { get; set; }


        /// <summary>
        /// The type of data stored by this form meta information.
        /// </summary>
        public Type ValueType
        {
            get
            {
                return typeof(T);
            }
        }

        #endregion

    }

}