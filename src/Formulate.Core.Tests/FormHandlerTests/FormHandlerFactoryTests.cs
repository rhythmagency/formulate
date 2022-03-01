using System;
using System.Collections.Generic;
using Formulate.Core.FormHandlers;
using Xunit;

namespace Formulate.Core.Tests.FormHandlerTests
{
    public partial class FormHandlerFactoryTests
    {
        [Fact(DisplayName = "When no settings provided should throw an Argument Null Exception")]
        public void WhenNoSettingsProvidedShouldThrowArgumentNullException()
        {
            // arrange
            var factory = CreateFactory();
            TestFormHandlerSettings settings = default;

            // act / asset
            Assert.Throws<ArgumentNullException>(() => factory.Create(settings));
        }

        [Fact(DisplayName = "When no KindId matches should return Default")]
        public void WhenNoKindIdMatchesShouldReturnDefault()
        {
            // arrange
            var factory = CreateFactory();
            var settings = new TestFormHandlerSettings()
            {
                KindId = Guid.Parse(Constants.MissingFormHandlerKindId)
            };

            // act
            var handler = factory.Create(settings);

            // assert
            Assert.Equal(default, handler);
        }

        [Fact(DisplayName = "When KindId matches an unsupported Form Handler Definition should throw a Not Supported Exception")]
        public void WhenKindIdMatchesAnUnsupportedFormHandlerDefinitionShouldThrowException()
        {
            // arrange
            var factory = CreateFactory();
            var settings = new TestFormHandlerSettings()
            {
                KindId = Guid.Parse(Constants.TestUnsupportedFormHandlerKindId)
            };

            // act / assert
            Assert.Throws<NotSupportedException>(() => factory.Create(settings));
        }

        [Fact(DisplayName = "When KindId matches an Async Form Handler Definition should return an Async Form Handler")]
        public void WhenKindIdMatchesAnAsyncFormHandlerDefinitionShouldReturnAnAsyncFormHandler()
        {
            // arrange
            var factory = CreateFactory();
            var settings = new TestFormHandlerSettings()
            {
                KindId = Guid.Parse(Constants.TestAsyncFormHandlerKindId)
            };
            
            // act
            var handler = factory.Create(settings);

            // assert
            Assert.IsAssignableFrom<AsyncFormHandler>(handler);
            Assert.NotEqual(default, handler);
        }

        [Fact(DisplayName = "When KindId matches a Form Handler Definition should return a Form Handler")]
        public void WhenKindIdMatchesAFormHandlerDefinitionShouldReturnAFormHandler()
        {
            // arrange
            var factory = CreateFactory();
            var settings = new TestFormHandlerSettings()
            {
                KindId = Guid.Parse(Constants.TestFormHandlerKindId)
            };

            // act
            var handler = factory.Create(settings);

            // assert
            Assert.IsAssignableFrom<FormHandler>(handler);
            Assert.NotEqual(default, handler);
        }

        private static IFormHandlerFactory CreateFactory()
        {
            var items = new List<IFormHandlerDefinition>
            {
                new TestAsyncFormHandlerDefinition(), 
                new TestFormHandlerDefinition(), 
                new TestUnsupportedFormHandlerDefinition()
            };

            var collection = new FormHandlerDefinitionCollection(() => items);

            return new FormHandlerFactory(collection);
        }
    }
}
