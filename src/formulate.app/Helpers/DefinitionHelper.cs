namespace formulate.app.Helpers
{

    // Namespaces.
    using core.Extensions;
    using core.Types;
    using Managers;
    using Persistence;
    using Resolvers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Validations;
    using ResolverConfig = Resolvers.Configuration;


    /// <summary>
    /// Helps with type definitions.
    /// </summary>
    public static class DefinitionHelper
    {

        #region Properties

        /// <summary>
        /// Configuration manager.
        /// </summary>
        private static IConfigurationManager Config
        {
            get
            {
                return ResolverConfig.Current.Manager;
            }
        }


        /// <summary>
        /// Form persistence.
        /// </summary>
        private static IFormPersistence Forms
        {
            get
            {
                return FormPersistence.Current.Manager;
            }
        }


        /// <summary>
        /// Layout persistence.
        /// </summary>
        private static ILayoutPersistence Layouts
        {
            get
            {
                return LayoutPersistence.Current.Manager;
            }
        }


        /// <summary>
        /// Validation persistence.
        /// </summary>
        private static IValidationPersistence Validations
        {
            get
            {
                return ValidationPersistence.Current.Manager;
            }
        }

        #endregion


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
        public static string GetTemplatePath(Guid? templateId)
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
        public static FormDefinition GetFormDefinition(Guid? formId)
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
        public static LayoutDefinition GetLayoutDefinition(Guid? layoutId)
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

}