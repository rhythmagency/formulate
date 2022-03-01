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
            Assert.Throws<ArgumentNullException>(() => factory.Create(settings));
        }

        [Fact(DisplayName = "When no KindId matches should return Default")]
        public void WhenNoKindIdMatchesShouldReturnDefault()
        {
            // arrange
            var factory = CreateFactory();
            var settings = new TestLayoutSettings()
            {
                KindId = Guid.Parse(Constants.MissingLayoutKindId)
            };

            // act
            var layout = factory.Create(settings);

            // assert
            Assert.Equal(default, layout);
        }

        [Fact(DisplayName = "When KindId matches a Layout Definition should return an expected Layout")]
        public void WhenKindIdMatchesALayoutDefinitionShouldReturnAnExpectedLayout()
        {
            // arrange
            var factory = CreateFactory();
            var settings = new TestLayoutSettings()
            {
                KindId = Guid.Parse(Constants.TestLayoutKindId)
            };

            // act
            var layout = factory.Create(settings);

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
