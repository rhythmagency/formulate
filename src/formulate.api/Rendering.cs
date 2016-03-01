namespace formulate.api
{

    // Namespaces.
    using app.Managers;
    using app.Persistence;
    using app.Resolvers;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Types;
    using ResolverConfig = app.Resolvers.Configuration;


    /// <summary>
    /// Handles operations related to rendering Formualte forms.
    /// </summary>
    public static class Rendering
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
        /// Creates a view model for the specified form, layout, and template.
        /// </summary>
        /// <param name="formId">
        /// The form ID.
        /// </param>
        /// <param name="layoutId">
        /// The layout ID.
        /// </param>
        /// <param name="templateId">
        /// The template ID.
        /// </param>
        /// <returns>
        /// The view model.
        /// </returns>
        /// <remarks>
        /// This model is used to render a form.
        /// </remarks>
        public static FormViewModel GetFormViewModel(Guid? formId, Guid? layoutId, Guid? templateId)
        {
            var model = new FormViewModel();
            model.FormDefinition = GetFormDefinition(formId);
            model.LayoutDefinition = GetLayoutDefinition(layoutId);
            model.TemplatePath = GetTemplatePath(templateId);
            return model;
        }

        #endregion


        #region Private Methods

        /// <summary>
        /// Gets the path to the template with the specified ID.
        /// </summary>
        /// <param name="templateId">
        /// The template ID.
        /// </param>
        /// <returns>
        /// The template path.
        /// </returns>
        private static string GetTemplatePath(Guid? templateId)
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
        private static FormDefinition GetFormDefinition(Guid? formId)
        {
            if (formId.HasValue)
            {
                var form = Forms.Retrieve(formId.Value);
                if (form != null)
                {
                    var definition = new FormDefinition();
                    var fields = new List<FieldDefinition>();
                    definition.Fields = fields;
                    foreach (var field in form.Fields)
                    {
                        var validations = field.Validations.Select(x => Validations.Retrieve(x));
                        var newField = new FieldDefinition()
                        {
                            Alias = field.Alias,
                            Configuration = field.DeserializeConfiguration(),
                            FieldType = field.GetFieldType(),
                            Id = field.Id,
                            Label = field.Label,
                            Name = field.Name,
                            Validations = validations.Select(x => new ValidationDefinition()
                            {
                                Alias = x.Alias,
                                Configuration = x.DeserializeConfiguration(),
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
        private static LayoutDefinition GetLayoutDefinition(Guid? layoutId)
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