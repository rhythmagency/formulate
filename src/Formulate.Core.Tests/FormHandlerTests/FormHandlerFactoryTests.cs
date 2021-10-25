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
            Assert.Throws<ArgumentNullException>(() => factory.CreateHandler(settings));
        }

        [Fact(DisplayName = "When no DefinitionId matches should return Default")]
        public void WhenNoDefinitionIdMatchesShouldReturnDefault()
        {
            // arrange
            var factory = CreateFactory();
            var settings = new TestFormHandlerSettings()
            {
                DefinitionId = Guid.Parse(Constants.MissingFormHandlerDefinitionId)
            };

            // act
            var handler = factory.CreateHandler(settings);

            // assert
            Assert.Equal(default, handler);
        }

        [Fact(DisplayName = "When DefinitionId matches an unsupported Form Handler Definition should throw a Not Supported Exception")]
        public void WhenDefinitionIdMatchesAnUnsupportedFormHandlerDefinitionShouldThrowException()
        {
            // arrange
            var factory = CreateFactory();
            var settings = new TestFormHandlerSettings()
            {
                DefinitionId = Guid.Parse(Constants.TestUnsupportedFormHandlerDefinitionId)
            };

            // act / assert
            Assert.Throws<NotSupportedException>(() => factory.CreateHandler(settings));
        }

        [Fact(DisplayName = "When DefinitionId matches an Async Form Handler Definition should return an Async Form Handler")]
        public void WhenDefinitionIdMatchesAnAsyncFormHandlerDefinitionShouldReturnAnAsyncFormHandler()
        {
            // arrange
            var factory = CreateFactory();
            var settings = new TestFormHandlerSettings()
            {
                DefinitionId = Guid.Parse(Constants.TestAsyncFormHandlerDefinitionId)
            };
            
            // act
            var handler = factory.CreateHandler(settings);

            // assert
            Assert.IsAssignableFrom<AsyncFormHandler>(handler);
            Assert.NotEqual(default, handler);
        }

        [Fact(DisplayName = "When DefinitionId matches a Form Handler Definition should return a Form Handler")]
        public void WhenDefinitionIdMatchesAFormHandlerDefinitionShouldReturnAFormHandler()
        {
            // arrange
            var factory = CreateFactory();
            var settings = new TestFormHandlerSettings()
            {
                DefinitionId = Guid.Parse(Constants.TestFormHandlerDefinitionId)
            };

            // act
            var handler = factory.CreateHandler(settings);

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
