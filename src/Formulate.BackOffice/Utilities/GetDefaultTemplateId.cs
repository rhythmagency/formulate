using Formulate.BackOffice.Configuration;
using Formulate.Core.Templates;
using Formulate.Core.Types;
using Microsoft.Extensions.Options;
using System;
using System.Linq;

namespace Formulate.BackOffice.Utilities
{
    internal sealed class GetDefaultTemplateId : IGetDefaultTemplateId
    {
        private readonly TemplateDefinitionCollection _templateDefinitions;

        private readonly Guid? _templateId;

        public GetDefaultTemplateId(TemplateDefinitionCollection templateDefinitions, IOptions<FormulateBackOfficeOptions> options)
        {
            _templateDefinitions = templateDefinitions;

            _templateId = options.Value.DefaultTemplateId;
        }

        public Guid? GetValue()
        {
            var template = _templateDefinitions.FirstOrDefault(_templateId);

            if (template is not null)
            {
                return template.KindId;
            }

            return _templateDefinitions.FirstOrDefault()?.KindId;
        }
    }
}
