using System;
using System.Collections.Generic;
using Formulate.Core.FormFields;
using Formulate.Core.Validations;

namespace Formulate.Core.Tests.FormFieldTests
{
    public partial class FormFieldFactoryTests
    {
        private static class Constants
        {
            public const string MissingFormFieldTypeId = "8D72D9E68AF44348A2F1FEE138902BA5";

            public const string TestFormFieldTypeId = "B5BDFE470A07446CACA733EA99B902F4";
        }

        private sealed class TestFormFieldSettings : IFormFieldSettings
        {
            public Guid TypeId { get; set; }
            public Guid Id { get; set; }
            public string Alias { get; set; }
            public string Name { get; set; }
            public string Label { get; set; }
            public string Configuration { get; set; }
            public string Category { get; set; }
            public Guid[] Validations { get; set; }
        }

        private sealed class TestFormFieldType : FormFieldType
        {
            public override Guid TypeId => new Guid(Constants.TestFormFieldTypeId);

            public override string TypeLabel => "Text Form Field";

            public override string Icon => "icon-test-field";

            public override string Directive => "formulate-test-field";
            
            public override IFormField CreateField(IFormFieldSettings settings)
            {
                return new TestFormField(settings);
            }
        }

        private sealed class TestFormField : FormField
        {
            public TestFormField(IFormFieldSettings settings) : base(settings)
            {
            }

            public TestFormField(IFormFieldSettings settings, IReadOnlyCollection<IValidation> validations) : base(settings, validations)
            {
            }
        }
    }
}
