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

        [Fact(DisplayName = "When no TypeId matches should return Default")]
        public void WhenNoTypeIdMatchesShouldReturnDefault()
        {
            // arrange
            var factory = CreateFactory();
            var settings = new TestLayoutSettings()
            {
                TypeId = Guid.Parse(Constants.MissingLayoutTypeId)
            };

            // act
            var layout = factory.CreateLayout(settings);

            // assert
            Assert.Equal(default, layout);
        }

        [Fact(DisplayName = "When TypeId matches a Layout Type should return an expected Layout")]
        public void WhenTypeIdMatchesALayoutTypeShouldReturnAnExpectedLayout()
        {
            // arrange
            var factory = CreateFactory();
            var settings = new TestLayoutSettings()
            {
                TypeId = Guid.Parse(Constants.TestLayoutTypeId)
            };

            // act
            var layout = factory.CreateLayout(settings);

            // assert
            Assert.IsType<TestLayout>(layout);
            Assert.NotEqual(default, layout);
        }

        private static ILayoutFactory CreateFactory()
        {
            var items = new List<ILayoutType>()
            {
                new TestLayoutType()
            };

            var collection = new LayoutTypeCollection(() => items);
            
            return new LayoutFactory(collection);
        }
    }
}
