using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Formulate.Core.DataValues;
using Xunit;

namespace Formulate.Core.Tests.DataValuesTests
{
    public partial class DataValuesFactoryTests
    {
        [Fact(DisplayName = "When no settings provided should throw an Argument Null Exception")]
        public async Task WhenNoSettingsProvidedShouldThrowArgumentNullException()
        {
            // arrange
            var factory = CreateFactory();
            TestDataValuesSettings settings = default;

            // act / asset
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await factory.CreateAsync(settings));
        }

        [Fact(DisplayName = "When no DefinitionId matches should return Default")]
        public async Task WhenNoDefinitionIdMatchesShouldReturnDefault()
        {
            // arrange
            var factory = CreateFactory();
            var settings = new TestDataValuesSettings()
            {
                DefinitionId = Guid.Parse(Constants.MissingDataValuesDefinitionId)
            };

            // act
            var formField = await factory.CreateAsync(settings);

            // assert
            Assert.Equal(default, formField);
        }

        [Fact(DisplayName = "When DefinitionId matches a Data Values Definition should return an expected Data Values")]
        public async Task WhenDefinitionIdMatchesADataValuesDefinitionShouldReturnAnExpectedDataValues()
        {
            // arrange
            var factory = CreateFactory();
            var settings = new TestDataValuesSettings()
            {
                DefinitionId = Guid.Parse(Constants.TestDataValuesDefinitionId)
            };

            // act
            var formField = await factory.CreateAsync(settings);

            // assert
            Assert.IsType<TestDataValues>(formField);
            Assert.NotEqual(default, formField);
        }

        private static IDataValuesFactory CreateFactory()
        {
            var items = new List<IDataValuesDefinition>()
            {
                new TestDataValuesDefinition()
            };

            var collection = new DataValuesDefinitionCollection(() => items);
            
            return new DataValuesFactory(collection);
        }
    }
}
