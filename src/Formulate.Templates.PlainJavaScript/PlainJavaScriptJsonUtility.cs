namespace Formulate.Templates.PlainJavaScript
{
    using Formulate.Core.FormFields;
    using Formulate.Core.FormFields.Button;
    using Formulate.Core.FormFields.DropDown;
    using Formulate.Core.FormFields.Text;
    using Formulate.Core.Layouts.Basic;
    using Formulate.Core.RenderModels;
    using Formulate.Core.Utilities;
    using Formulate.Core.Validations;
    using Formulate.Core.Validations.Mandatory;
    using Formulate.Core.Validations.Regex;
    using Microsoft.AspNetCore.Antiforgery;
    using Microsoft.AspNetCore.Http;
    using Umbraco.Cms.Core.Web;

    public sealed class PlainJavaScriptJsonUtility : IPlainJavaScriptJsonUtility
    {
        private readonly IJsonUtility _jsonUtility;
        private readonly IUmbracoContextAccessor _umbracoContextAccessor;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAntiforgery _antiforgery;

        public PlainJavaScriptJsonUtility(IJsonUtility jsonUtility, IUmbracoContextAccessor umbracoContextAccessor, IHttpContextAccessor httpContextAccessor, IAntiforgery antiforgery)
        {
            _jsonUtility = jsonUtility;
            _umbracoContextAccessor = umbracoContextAccessor;
            _httpContextAccessor = httpContextAccessor;
            _antiforgery = antiforgery;
        }

        public string GetJson(ConfiguredFormRenderModel renderModel, string containerId)
        {
            var fields = renderModel.Form.Fields
            // Exclude server-side only fields.
            .Where(x => !x.Definition.IsServerSideOnly).Select(x => x.Field).ToArray();
            var layout = renderModel.Layout as BasicLayout;

            if (layout is null)
            {
                // only basic layout is supported.
                return string.Empty;
            }

            var pageId = GetPageId();

            var csrfToken = GenerateAntiForgeryToken();

            if (string.IsNullOrEmpty(csrfToken))
            {
                return string.Empty;
            }

            // A function that returns a validation configuration.
            var getValidationConfig = new Func<IValidation, object>(x =>
            {
                if (x is RegexValidation regex)
                {
                    var config = regex.Configuration;
                    return new
                    {
                        message = config.Message,
                        pattern = config.Pattern
                    };
                }
                else if (x is MandatoryValidation mandatory)
                {
                    var config = mandatory.Configuration;
                    return new
                    {
                        message = config.Message
                    };
                }
                return new { };
            });


            // A function that returns a field configuration.
            var getFieldConfig = new Func<IFormField, object>(x =>
            {
                if (x is DropDownField dropDown)
                {
                    var config = dropDown.Configuration;
                    return new
                    {
                        items = config.Items.Select(y => new
                        {
                            value = y.Value,
                            label = y.Label,
                            selected = y.Selected
                        }).ToArray()
                    };
                }
                else if (x is ButtonField button)
                {
                    var config = button.Configuration;
                    return new
                    {
                        buttonKind = config.ButtonKind
                    };
                }

                return new { };
            });

            var getFieldType = new Func<IFormField, string>(f =>
            {
                switch (f)
                {
                    case ButtonField:
                        return "button";
                    case DropDownField:
                        return "select";
                    case TextField:
                        return "text";
                }

                return f.GetType().Name.ToLower().Replace("field", string.Empty);
            });

            var getValidationType = new Func<IValidation, string>(v =>
            {
                switch (v)
                {
                    case RegexValidation:
                        return "regex";
                    case MandatoryValidation:
                        return "required";
                }

                return v.GetType().Name.ToLower().Replace("validation", string.Empty);
            });

            // Structure fields as an anonymous object suitable for serialization to JSON.
            var fieldsData = fields.Select(x => new
            {
                // The alias can be used to attach custom styles.
                alias = x.Alias,
                // The label can be used to instruct the user what data to enter.
                label = x.Label ?? x.Name,
                // The ID can be submitted to the server to uniquely identify the field.
                id = x.Id.ToString("N"),
                // The random ID can be used to uniquely identify the field on the page.
                // Note that this random ID is regenerated on each page render.
                randomId = Guid.NewGuid().ToString("N"),
                // This field type (e.g., "text", "checkbox") can be used to figure out how to render a field.
                fieldType = getFieldType(x),//.ConvertFieldTypeToAngularType(),
                                            // The validations can be used to validate that the data is of the expected format.
                validations = x.Validations.Select(y => new
                {
                    id = y.Id.ToString("N"),
                    alias = y.Name,
                    validationType = getValidationType(y),//.ConvertValidationTypeToJavaScriptType(),
                                                          // The validation configuration stores parameters particular to a validation instance (e.g., a regex pattern).
                    configuration = getValidationConfig(y)
                }).ToArray(),
                // The field configuration stores parameters particular to a field (e.g., a list of items).
                configuration = getFieldConfig(x),
                // The initial value comes from the query string based on the field alias.
                initialValue = "",
                // The user selected category of the field
                category = x.Category
            }).ToArray();


            // Structure layout as an anonymous object suitable for serialization to JSON.
            var rowsData = layout.Configuration.Rows.Select(x => new
            {
                isStep = x.IsStep,
                cells = x.Cells.Select(y => new
                {
                    columns = y.ColumnSpan,
                    fields = y.Fields.Select(z => new
                    {
                        id = z.FieldId.ToString("N")
                    }).Where(i => fieldsData.Any(f => f.id.Equals(i.id))).ToArray()
                }).ToArray()
            }).ToArray();


            // Convert to a JSON string that can be consumed by JavaScript.
            return _jsonUtility.Serialize(new
            {
                data = new
                {
                    // The name of the form can be use for analytics.
                    name = renderModel.Form.Name,
                    // The form alias can be used for custom styles.
                    alias = renderModel.Form.Alias,
                    // The random ID can be used to uniquely identify the form on the page.
                    // Note that this random ID is regenerated on each page render.
                    randomId = containerId,
                    // The fields in this form.
                    fields = fieldsData,
                    // The rows that define the form layout.
                    rows = rowsData,
                    payload = new
                    {
                        FormId = renderModel.Form.Id.ToString("N"),
                        PageId = pageId,
                        __RequestVerificationToken = csrfToken
                    },
                    url = "/umbraco/formulate/submissions/submit"
                }
            });
        }

        private string GenerateAntiForgeryToken()
        {
            var context = _httpContextAccessor.HttpContext;

            if (context is null)
            {
                return string.Empty;
            }

            var tokens = _antiforgery.GetAndStoreTokens(context);

            return tokens.RequestToken;
        }

        private int? GetPageId()
        {
            if (_umbracoContextAccessor.TryGetUmbracoContext(out var context) == false)
            {
                return default;
            }

            var request = context.PublishedRequest;

            if (request is null || request.PublishedContent is null)
            {
                return default;
            }

            return request.PublishedContent.Id;
        }
    }
}
