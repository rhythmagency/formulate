using System;
using System.Collections.Generic;
using Formulate.Core.Validations;

namespace Formulate.Core.Tests.ValidationTests
{
    public partial class ValidationFactoryTests
    {
        private static class Constants
        {
            public const string MissingValidationKindId = "5F05924C14BD450B8C30F9A97EEEB1FC";

            public const string TestValidationKindId = "D3CE69AC280C408A91207DA3F3123E2F";
        }

        private sealed class TestValidationSettings : IValidationSettings
        {
            public Guid KindId { get; set; }

            public string Name { get; set; }

            public Guid Id { get; set; }

            public string Data { get; set; }

            public string Alias { get; set; }
        }

        private sealed class TestValidationDefinition : IValidationDefinition
        {
            public Guid KindId => Guid.Parse(Constants.TestValidationKindId);
            
            public string Name => "Test Validation";
            
            public string Directive => "formulate-test-validation";
            
            public Validation CreateValidation(IValidationSettings settings)
            {
                return new TestValidation(settings);
            }

            public object GetBackOfficeConfiguration(IValidationSettings settings)
            {
                return null;
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
