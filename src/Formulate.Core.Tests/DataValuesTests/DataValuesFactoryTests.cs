namespace Formulate.Core.Tests.DataValuesTests
{
    // Namespaces.
    using DataValues;
    using System;
    using System.Collections.Generic;
    using Xunit;

    public partial class DataValuesFactoryTests
    {
        [Fact(DisplayName = "When no entity provided should throw an Argument Null Exception")]
        public void WhenNoSettingsProvidedShouldThrowArgumentNullException()
        {
            // arrange
            var factory = CreateFactory();
            PersistedDataValues entity = default;

            // act / asset
            Assert.Throws<ArgumentNullException>(() =>  factory.Create(entity));
        }

        [Fact(DisplayName = "When no KindId matches should return Default")]
        public void WhenNoKindIdMatchesShouldReturnDefault()
        {
            // arrange
            var factory = CreateFactory();
            var entity = new PersistedDataValues()
            {
                KindId = Guid.Parse(Constants.MissingDataValuesKindId)
            };

            // act
            var formField = factory.Create(entity);

            // assert
            Assert.Equal(default, formField);
        }

        [Fact(DisplayName = "When KindId matches a Data Values Definition should return an expected Data Values")]
        public void WhenKindIdMatchesADataValuesDefinitionShouldReturnAnExpectedDataValues()
        {
            // arrange
            var factory = CreateFactory();
            var entity = new PersistedDataValues()
            {
                KindId = Guid.Parse(Constants.TestDataValuesKindId)
            };

            // act
            var formField = factory.Create(entity);

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