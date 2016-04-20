namespace formulate.app.DataValues.Kinds
{

    // Namespaces.
    using Helpers;
    using System;
    using Constants = Constants.DataValues.DataValueList;


    /// <summary>
    /// A data value kind that stores a list of strings.
    /// </summary>
    public class DataValueList : IDataValueKind
    {

        #region Properties

        /// <summary>
        /// The kind ID.
        /// </summary>
        public Guid Id
        {
            get
            {
                return GuidHelper.GetGuid(Constants.Id);
            }
        }


        /// <summary>
        /// The kind name.
        /// </summary>
        public string Name
        {
            get
            {
                return LocalizationHelper.GetDataValueName(Constants.Name);
            }
        }


        /// <summary>
        /// The kind directive.
        /// </summary>
        public string Directive
        {
            get
            {
                return Constants.Directive;
            }
        }

        #endregion

    }

}