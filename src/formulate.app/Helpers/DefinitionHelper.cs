namespace formulate.app.Helpers
{

    // Namespaces.
    using core.Extensions;
    using core.Types;
    using Managers;
    using Persistence;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Validations;


    /// <summary>
    /// Helps with type definitions.
    /// </summary>
    public class DefinitionHelper : IDefinitionHelper
    {

        #region Properties

        /// <summary>
        /// Configuration manager.
        /// </summary>
        private IConfigurationManager Config { get; set; }

        /// <summary>
        /// Form persistence.
        /// </summary>
        private IFormPersistence Forms { get; set; }


        /// <summary>
        /// Layout persistence.
        /// </summary>
        private ILayoutPersistence Layouts { get; set; }


        /// <summary>
        /// Validation persistence.
        /// </summary>
        private IValidationPersistence Validations { get; set; }

        #endregion

        public DefinitionHelper(IConfigurationManager configurationManager, IFormPersistence formPersistence, ILayoutPersistence layoutPersistence, IValidationPersistence validationPersistence)
        {
            Config = configurationManager;
            Forms = formPersistence;
            Layouts = layoutPersistence;
            Validations = validationPersistence;
        }

        #region Public Methods

        /// <summary>
        /// Gets the path to the template with the specified ID.
        /// </summary>
        /// <param name="templateId">
        /// The template ID.
        /// </param>
        /// <returns>
        /// The template path.
        /// </returns>
        public string GetTemplatePath(Guid? templateId)
        {
            if (templateId.HasValue)
            {
                var template = Config.Templates.FirstOrDefault(x => x.Id == templateId.Value);
                return template?.Path;
            }
            return null;
        }


        /// <summary>
        /// Gets the form definition for the form with the specified ID.
        /// </summary>
        /// <param name="formId">
        /// The form ID.
        /// </param>
        /// <returns>
        /// The form definition.
        /// </returns>
        public FormDefinition GetFormDefinition(Guid? formId)
        {
            if (formId.HasValue)
            {
                var form = Forms.Retrieve(formId.Value);
                if (form != null)
                {
                    var definition = new FormDefinition();
                    definition.FormId = form.Id;
                    definition.Name = form.Name;
                    definition.Alias = form.Alias;
                    var fields = new List<FieldDefinition>();
                    definition.Fields = fields;
                    foreach (var field in form.Fields)
                    {
                        var validations = field.Validations
                            .Select(x => Validations.Retrieve(x)).WithoutNulls();
                        var context = new ValidationContext()
                        {
                            Field = field,
                            Form = form
                        };
                        var newField = new FieldDefinition()
                        {
                            Alias = field.Alias,
                            Configuration = field.DeserializeConfiguration(),
                            FieldType = field.GetFieldType(),
                            Id = field.Id,
                            Label = field.Label,
                            Category = field.Category,
                            Name = field.Name,
                            IsServerSideOnly = field.IsServerSideOnly,
                            Validations = validations.Select(x => new ValidationDefinition()
                            {
                                Alias = x.Alias,
                                Configuration = x.DeserializeConfiguration(context),
                                Id = x.Id,
                                Name = x.Name,
                                ValidationType = x.GetValidationKind().GetType()
                            })
                        };
                        fields.Add(newField);
                    }
                    return definition;
                }
            }
            return null;
        }


        /// <summary>
        /// Gets the layout definition for the layout with the specified ID.
        /// </summary>
        /// <param name="layoutId">
        /// The layout ID.
        /// </param>
        /// <returns>
        /// The layout definition.
        /// </returns>
        public LayoutDefinition GetLayoutDefinition(Guid? layoutId)
        {
            if (layoutId.HasValue)
            {
                var layout = Layouts.Retrieve(layoutId.Value);
                if (layout != null)
                {
                    var definition = new LayoutDefinition()
                    {
                        Alias = layout.Alias,
                        Id = layout.Id,
                        Name = layout.Name,
                        LayoutType = layout.GetLayoutKind().GetType(),
                        Configuration = layout.DeserializeConfiguration()
                    };
                    return definition;
                }
            }
            return null;
        }

        #endregion

    }

    public interface IDefinitionHelper
    {
        string GetTemplatePath(Guid? templateId);

        FormDefinition GetFormDefinition(Guid? formId);
        LayoutDefinition GetLayoutDefinition(Guid? layoutId);
    }
}