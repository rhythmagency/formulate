using System;
using System.Collections.Generic;
using Formulate.Core.Layouts;
using Xunit;

namespace Formulate.Core.Tests.LayoutTests
{
    public partial class LayoutFactoryTests
    {
        [Fact(DisplayName = "When no layout provided should throw an Argument Null Exception")]
        public void WhenNoLayoutProvidedShouldThrowArgumentNullException()
        {
            // arrange
            var factory = CreateFactory();
            PersistedLayout layout = default;

            // act / asset
            Assert.Throws<ArgumentNullException>(() => factory.Create(layout));
        }

        [Fact(DisplayName = "When no KindId matches should return Default")]
        public void WhenNoKindIdMatchesShouldReturnDefault()
        {
            // arrange
            var factory = CreateFactory();
            var layout = new PersistedLayout()
            {
                KindId = Guid.Parse(Constants.MissingLayoutKindId)
            };

            // act
            var createdLayout = factory.Create(layout);

            // assert
            Assert.Equal(default, createdLayout);
        }

        [Fact(DisplayName = "When KindId matches a Layout Definition should return an expected Layout")]
        public void WhenKindIdMatchesALayoutDefinitionShouldReturnAnExpectedLayout()
        {
            // arrange
            var factory = CreateFactory();
            var layout = new PersistedLayout()
            {
                KindId = Guid.Parse(Constants.TestLayoutKindId)
            };

            // act
            var createdLayout = factory.Create(layout);

            // assert
            Assert.IsType<TestLayout>(createdLayout);
            Assert.NotEqual(default, createdLayout);
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
