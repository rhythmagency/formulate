using System;
using System.Collections.Generic;
using Formulate.Core.Validations;

namespace Formulate.Core.Tests.ValidationTests
{
    public partial class ValidationFactoryTests
    {
        private static class Constants
        {
            public const string MissingValidationTypeId = "5F05924C14BD450B8C30F9A97EEEB1FC";

            public const string TestValidationTypeId = "D3CE69AC280C408A91207DA3F3123E2F";
        }

        private sealed class TestValidationSettings : IValidationSettings
        {
            public Guid TypeId { get; set; }

            public Guid Id { get; set; }
            
            public string Configuration { get; set; }
        }

        private sealed class TestValidationType : IValidationType
        {
            public Guid TypeId => new Guid(Constants.TestValidationTypeId);
            
            public string Icon => "icon-test-field";

            public string TypeLabel => "Test";
            
            public string Directive => "formulate-test-field";
            
            public IValidation CreateValidation(IValidationSettings settings)
            {
                return new TestValidation(settings);
            }
        }

        private sealed class TestValidation : Validation
        {
            public TestValidation(IValidationSettings settings) : base(settings)
            {
            }
        }
    }
}
