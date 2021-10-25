using System;
using System.Collections.Generic;
using Formulate.Core.Layouts;
using Xunit;

namespace Formulate.Core.Tests.LayoutTests
{
    public partial class LayoutFactoryTests
    {
        [Fact(DisplayName = "When no settings provided should throw an Argument Null Exception")]
        public void WhenNoSettingsProvidedShouldThrowArgumentNullException()
        {
            // arrange
            var factory = CreateFactory();
            TestLayoutSettings settings = default;

            // act / asset
            Assert.Throws<ArgumentNullException>(() => factory.CreateLayout(settings));
        }

        [Fact(DisplayName = "When no DefinitionId matches should return Default")]
        public void WhenNoDefinitionIdMatchesShouldReturnDefault()
        {
            // arrange
            var factory = CreateFactory();
            var settings = new TestLayoutSettings()
            {
                DefinitionId = Guid.Parse(Constants.MissingLayoutDefinitionId)
            };

            // act
            var layout = factory.CreateLayout(settings);

            // assert
            Assert.Equal(default, layout);
        }

        [Fact(DisplayName = "When DefinitionId matches a Layout Definition should return an expected Layout")]
        public void WhenDefinitionIdMatchesALayoutDefinitionShouldReturnAnExpectedLayout()
        {
            // arrange
            var factory = CreateFactory();
            var settings = new TestLayoutSettings()
            {
                DefinitionId = Guid.Parse(Constants.TestLayoutDefinitionId)
            };

            // act
            var layout = factory.CreateLayout(settings);

            // assert
            Assert.IsType<TestLayout>(layout);
            Assert.NotEqual(default, layout);
        }

        private static ILayoutFactory CreateFactory()
        {
            var items = new List<ILayoutDefinition>()
            {
                new TestLayoutDefinition()
            };

            var collection = new LayoutDefinitionCollection(() => items);
            
            return new LayoutFactory(collection);
        }
    }
}
