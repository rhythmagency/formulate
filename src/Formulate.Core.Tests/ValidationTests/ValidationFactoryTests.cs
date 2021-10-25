using System;
using System.Collections.Generic;
using Formulate.Core.Validations;
using Xunit;

namespace Formulate.Core.Tests.ValidationTests
{
    public partial class ValidationFactoryTests
    {
        [Fact(DisplayName = "When no settings provided should throw an Argument Null Exception")]
        public void WhenNoSettingsProvidedShouldThrowArgumentNullException()
        {
            // arrange
            var factory = CreateFactory();
            TestValidationSettings settings = default;

            // act / asset
            Assert.Throws<ArgumentNullException>(() => factory.Create(settings));
        }

        [Fact(DisplayName = "When no DefinitionId matches should return Default")]
        public void WhenNoDefinitionIdMatchesShouldReturnDefault()
        {
            // arrange
            var factory = CreateFactory();
            var settings = new TestValidationSettings()
            {
                DefinitionId = Guid.Parse(Constants.MissingValidationDefinitionId)
            };

            // act
            var validation = factory.Create(settings);

            // assert
            Assert.Equal(default, validation);
        }

        [Fact(DisplayName = "When DefinitionId matches a Validation Definition should return an expected Validation")]
        public void WhenDefinitionIdMatchesAValidationDefinitionShouldReturnAnExpectedValidation()
        {
            // arrange
            var factory = CreateFactory();
            var settings = new TestValidationSettings()
            {
                DefinitionId = Guid.Parse(Constants.TestValidationDefinitionId)
            };

            // act
            var validation = factory.Create(settings);

            // assert
            Assert.IsType<TestValidation>(validation);
            Assert.NotEqual(default, validation);
        }

        private static IValidationFactory CreateFactory()
        {
            var items = new List<IValidationDefinition>()
            {
                new TestValidationDefinition()
            };

            var collection = new ValidationDefinitionCollection(() => items);
            
            return new ValidationFactory(collection);
        }
    }
}
