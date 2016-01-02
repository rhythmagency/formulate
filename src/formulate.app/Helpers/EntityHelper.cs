namespace formulate.app.Helpers
{

    // Namespaces.
    using Entities;
    using System;
    using FormsConstants = formulate.app.Constants.Trees.Forms;
    using LayoutsConstants = formulate.app.Constants.Trees.Layouts;
    using ValidationsConstants = formulate.app.Constants.Trees.Validations;


    /// <summary>
    /// Helps with entities.
    /// </summary>
    internal class EntityHelper
    {

        #region Constants

        private const string InvalidId = "The specified ID was not recognized as a valid root ID.";

        #endregion


        #region Readonly Variables

        static readonly Guid FormId;
        static readonly Guid LayoutId;
        static readonly Guid ValidationId;

        #endregion


        #region Constructors

        /// <summary>
        /// Static constructor.
        /// </summary>
        static EntityHelper()
        {
            FormId = GuidHelper.GetGuid(FormsConstants.Id);
            LayoutId = GuidHelper.GetGuid(LayoutsConstants.Id);
            ValidationId = GuidHelper.GetGuid(ValidationsConstants.Id);
        }

        #endregion


        #region Methods

        /// <summary>
        /// Gets the tree icon for the root entity with the specified ID.
        /// </summary>
        /// <param name="id">
        /// The root entity ID.
        /// </param>
        /// <returns>The icon.</returns>
        public static string GetIconForRoot(Guid id)
        {
            if (id == FormId)
            {
                return FormsConstants.TreeIcon;
            }
            else if (id == LayoutId)
            {
                return LayoutsConstants.TreeIcon;
            }
            else if (id == ValidationId)
            {
                return ValidationsConstants.TreeIcon;
            }
            else
            {
                throw new ArgumentOutOfRangeException("id", InvalidId);
            }
        }


        /// <summary>
        /// Gets the group tree icon for the entities under the root
        /// with the specified ID.
        /// </summary>
        /// <param name="id">
        /// The root entity ID.
        /// </param>
        /// <returns>
        /// The group icon.
        /// </returns>
        public static string GetGroupIconByRoot(Guid id)
        {
            if (id == FormId)
            {
                return FormsConstants.GroupIcon;
            }
            else if (id == LayoutId)
            {
                return LayoutsConstants.GroupIcon;
            }
            else if (id == ValidationId)
            {
                return ValidationsConstants.GroupIcon;
            }
            else
            {
                throw new ArgumentOutOfRangeException("id", InvalidId);
            }
        }


        /// <summary>
        /// Gets the name for the root entity with the specified ID.
        /// </summary>
        /// <param name="id">
        /// The root entity ID.
        /// </param>
        /// <returns>
        /// The entity name.
        /// </returns>
        public static string GetNameForRoot(Guid id)
        {
            if (id == FormId)
            {
                return FormsConstants.Title;
            }
            else if (id == LayoutId)
            {
                return LayoutsConstants.Title;
            }
            else if (id == ValidationId)
            {
                return ValidationsConstants.Title;
            }
            else
            {
                throw new ArgumentOutOfRangeException("id", InvalidId);
            }
        }


        /// <summary>
        /// Indicates whether or not the entity with the specified ID
        /// is a root entity.
        /// </summary>
        /// <param name="id">The entity ID.</param>
        /// <returns>
        /// True, if the entity is a root entity; otherwise, false.
        /// </returns>
        public static bool IsRoot(Guid id)
        {
            return id == FormId || id == LayoutId || id == ValidationId;
        }


        /// <summary>
        /// Converts an entity kind to a string.
        /// </summary>
        /// <param name="kind">The entity kind.</param>
        /// <returns>
        /// The entity kind string.
        /// </returns>
        public static string GetString(EntityKind kind)
        {
            return Enum.GetName(typeof(EntityKind), kind);
        }

        #endregion

    }

}