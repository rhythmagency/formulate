namespace Formulate.Extensions.PlainJavaScriptTemplate.Core
{
    using Formulate.Core.RenderModels;
    using Formulate.Core.Utilities;
    using Microsoft.AspNetCore.Antiforgery;
    using Microsoft.AspNetCore.Http;
    using Umbraco.Cms.Core.Mapping;
    using Umbraco.Cms.Core.Web;

    public sealed class BuildPlainJavaScriptJson : IBuildPlainJavaScriptJson
    {
        private readonly IJsonUtility _jsonUtility;
        private readonly IUmbracoContextAccessor _umbracoContextAccessor;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAntiforgery _antiforgery;
        private readonly IUmbracoMapper _umbracoMapper;
        
        public BuildPlainJavaScriptJson(IJsonUtility jsonUtility, IUmbracoContextAccessor umbracoContextAccessor, IHttpContextAccessor httpContextAccessor, IAntiforgery antiforgery, IUmbracoMapper umbracoMapper)
        {
            _jsonUtility = jsonUtility;
            _umbracoContextAccessor = umbracoContextAccessor;
            _httpContextAccessor = httpContextAccessor;
            _antiforgery = antiforgery;
            _umbracoMapper = umbracoMapper;
        }

        public string Build(FormLayoutRenderModel renderModel, string containerId)
        {
            var layout = _umbracoMapper.Map<PlainJavaScriptLayout?>(renderModel.Layout);

            if (layout is null)
            {
                return string.Empty;
            }

            // Exclude server-side only fields.
            var fields = renderModel.Form.Fields.Where(x => !x.IsServerSideOnly).Select(x => x.Field).ToArray();

            var pageId = GetPageId();

            var csrfToken = GenerateAntiForgeryToken();

            if (string.IsNullOrEmpty(csrfToken))
            {
                return string.Empty;
            }

            // Structure fields as an anonymous object suitable for serialization to JSON.
            var fieldsData = fields.Select(x => {
                var mappedField = _umbracoMapper.Map<PlainJavaScriptFormField>(x) ?? new PlainJavaScriptFormField("unknown");
                return new
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
                    fieldType = mappedField.FieldType,
                    validations = x.Validations.Select(y =>
                    {
                        var mappedValidation = _umbracoMapper.Map<PlainJavaScriptValidation>(y) ?? new PlainJavaScriptValidation("unknown");
                        return new
                        {
                            id = y.Id.ToString("N"),
                            alias = y.Name,
                            validationType = mappedValidation.ValidationType,
                            configuration = mappedValidation.Configuration
                        };
                    }).ToArray(),
                    // The field configuration stores parameters particular to a field (e.g., a list of items).
                    configuration = mappedField.Configuration,
                    // The initial value comes from the query string based on the field alias.
                    initialValue = "",
                    // The user selected category of the field
                    category = x.Category                
                };

            }).ToArray();


            // Structure layout as an anonymous object suitable for serialization to JSON.
            var rowsData = layout.Rows.Select(x => new
            {
                isStep = x.IsStep,
                cells = x.Cells.Select(y => new
                {
                    columns = y.ColumnSpan,
                    fields = y.FieldIds.Select(z => new
                    {
                        id = z.ToString("N")
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
                    url = "/api/formulate/plain-js/submit"
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
