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

        [Fact(DisplayName = "When no KindId matches should return Default")]
        public void WhenNoKindIdMatchesShouldReturnDefault()
        {
            // arrange
            var factory = CreateFactory();
            var settings = new TestValidationSettings()
            {
                KindId = Guid.Parse(Constants.MissingValidationKindId)
            };

            // act
            var validation = factory.Create(settings);

            // assert
            Assert.Equal(default, validation);
        }

        [Fact(DisplayName = "When KindId matches a Validation Definition should return an expected Validation")]
        public void WhenKindIdMatchesAValidationDefinitionShouldReturnAnExpectedValidation()
        {
            // arrange
            var factory = CreateFactory();
            var settings = new TestValidationSettings()
            {
                KindId = Guid.Parse(Constants.TestValidationKindId)
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
