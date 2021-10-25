using System;
using System.Collections.Generic;
using Formulate.Core.Validations;

namespace Formulate.Core.Tests.ValidationTests
{
    public partial class ValidationFactoryTests
    {
        private static class Constants
        {
            public const string MissingValidationDefinitionId = "5F05924C14BD450B8C30F9A97EEEB1FC";

            public const string TestValidationDefinitionId = "D3CE69AC280C408A91207DA3F3123E2F";
        }

        private sealed class TestValidationSettings : IValidationSettings
        {
            public Guid DefinitionId { get; set; }

            public string Name { get; set; }

            public Guid Id { get; set; }
            
            public string Configuration { get; set; }
        }

        private sealed class TestValidationDefinition : IValidationDefinition
        {
            public Guid DefinitionId => Guid.Parse(Constants.TestValidationDefinitionId);
            
            public string DefinitionLabel => "Test Validation";
            
            public string Directive => "formulate-test-validation";
            
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
