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
            Assert.Throws<ArgumentNullException>(() => factory.CreateValidation(settings));
        }

        [Fact(DisplayName = "When no TypeId matches should return Default")]
        public void WhenNoTypeIdMatchesShouldReturnDefault()
        {
            // arrange
            var factory = CreateFactory();
            var settings = new TestValidationSettings()
            {
                TypeId = Guid.Parse(Constants.MissingValidationTypeId)
            };

            // act
            var validation = factory.CreateValidation(settings);

            // assert
            Assert.Equal(default, validation);
        }

        [Fact(DisplayName = "When TypeId matches a Validation Type should return an expected Validation")]
        public void WhenTypeIdMatchesAValidationTypeShouldReturnAnExpectedValidation()
        {
            // arrange
            var factory = CreateFactory();
            var settings = new TestValidationSettings()
            {
                TypeId = Guid.Parse(Constants.TestValidationTypeId)
            };

            // act
            var validation = factory.CreateValidation(settings);

            // assert
            Assert.IsType<TestValidation>(validation);
            Assert.NotEqual(default, validation);
        }

        private static IValidationFactory CreateFactory()
        {
            var items = new List<IValidationType>()
            {
                new TestValidationType()
            };

            var collection = new ValidationTypeCollection(() => items);
            
            return new ValidationFactory(collection);
        }
    }
}
