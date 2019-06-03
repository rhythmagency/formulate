namespace formulate.app.Helpers
{

    // Namespaces.
    using Entities;
    using System;
    using System.Linq;
    using Umbraco.Core;
    using CoreConstants = Umbraco.Core.Constants;
    using DataSourceConstants = formulate.app.Constants.Trees.DataSources;
    using DataValueConstants = formulate.app.Constants.Trees.DataValues;
    using FormConstants = formulate.app.Constants.Trees.Forms;
    using LayoutConstants = formulate.app.Constants.Trees.Layouts;
    using SubmissionConstants = formulate.app.Constants.Trees.Submissions;
    using ValidationConstants = formulate.app.Constants.Trees.Validations;


    /// <summary>
    /// Helps with entities.
    /// </summary>
    internal class EntityHelper : IEntityHelper
    {
        private ILocalizationHelper LocalizationHelper { get; set; }

        #region Constants

        private const string InvalidId = "The specified ID was not recognized as a valid root ID.";

        #endregion


        #region Readonly Variables

        static readonly Guid FormId = GuidHelper.GetGuid(FormConstants.Id);
        static readonly Guid LayoutId = GuidHelper.GetGuid(LayoutConstants.Id);
        static readonly Guid ValidationId = GuidHelper.GetGuid(ValidationConstants.Id);
        static readonly Guid DataValueId = GuidHelper.GetGuid(DataValueConstants.Id);
        static readonly Guid DataSourceId = GuidHelper.GetGuid(DataSourceConstants.Id);
        static readonly Guid SubmissionId = GuidHelper.GetGuid(SubmissionConstants.Id);


        #endregion


        #region Constructors


        public EntityHelper(ILocalizationHelper localizationHelper)
        {

            LocalizationHelper = localizationHelper;
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
        public string GetIconForRoot(Guid id)
        {
            if (id == FormId)
            {
                return FormConstants.TreeIcon;
            }
            else if (id == LayoutId)
            {
                return LayoutConstants.TreeIcon;
            }
            else if (id == ValidationId)
            {
                return ValidationConstants.TreeIcon;
            }
            else if (id == DataValueId)
            {
                return DataValueConstants.TreeIcon;
            }
            else if (id == DataSourceId)
            {
                return DataSourceConstants.TreeIcon;
            }
            else if (id == SubmissionId)
            {
                return SubmissionConstants.TreeIcon;
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
        public string GetGroupIconByRoot(Guid id)
        {
            if (id == FormId)
            {
                return FormConstants.GroupIcon;
            }
            else if (id == LayoutId)
            {
                return LayoutConstants.GroupIcon;
            }
            else if (id == ValidationId)
            {
                return ValidationConstants.GroupIcon;
            }
            else if (id == DataValueId)
            {
                return DataValueConstants.GroupIcon;
            }
            else if (id == DataSourceId)
            {
                return DataSourceConstants.GroupIcon;
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
        public string GetNameForRoot(Guid id)
        {
            var name = default(string);
            if (id == FormId)
            {
                name = FormConstants.Title;
            }
            else if (id == LayoutId)
            {
                name = LayoutConstants.Title;
            }
            else if (id == ValidationId)
            {
                name = ValidationConstants.Title;
            }
            else if (id == DataValueId)
            {
                name = DataValueConstants.Title;
            }
            else if (id == DataSourceId)
            {
                name = DataSourceConstants.Title;
            }
            else if (id == SubmissionId)
            {
                name = SubmissionConstants.Title;
            }
            else
            {
                throw new ArgumentOutOfRangeException("id", InvalidId);
            }
            return LocalizationHelper.GetTreeName(name);
        }


        /// <summary>
        /// Indicates whether or not the entity with the specified ID
        /// is a root entity.
        /// </summary>
        /// <param name="id">The entity ID.</param>
        /// <returns>
        /// True, if the entity is a root entity; otherwise, false.
        /// </returns>
        public bool IsRoot(Guid id)
        {
            return id == FormId || id == LayoutId || id == ValidationId
                || id == DataValueId || id == DataSourceId || id == SubmissionId;
        }


        /// <summary>
        /// Converts an entity kind to a string.
        /// </summary>
        /// <param name="kind">The entity kind.</param>
        /// <returns>
        /// The entity kind string.
        /// </returns>
        public string GetString(EntityKind kind)
        {
            return Enum.GetName(typeof(EntityKind), kind);
        }


        /// <summary>
        /// Converts a server-side entity path to a client-side entity path.
        /// </summary>
        /// <param name="path">
        /// The server-side entity path.
        /// </param>
        /// <returns>
        /// The client-side entity path.
        /// </returns>
        /// <remarks>
        /// The client-side entity path expects an extra root ID of "-1",
        /// which this method includes.
        /// </remarks>
        public string[] GetClientPath(Guid[] path)
        {
            var rootId = CoreConstants.System.Root.ToInvariantString();
            var clientPath = new[] { rootId }
                .Concat(path.Select(x => GuidHelper.GetString(x)))
                .ToArray();
            return clientPath;
        }

        #endregion

    }

}