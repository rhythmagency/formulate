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

        [Fact(DisplayName = "When no TypeId matches should return Default")]
        public void WhenNoTypeIdMatchesShouldReturnDefault()
        {
            // arrange
            var factory = CreateFactory();
            var settings = new TestFormHandlerSettings()
            {
                TypeId = Guid.Parse(Constants.MissingFormHandlerTypeId)
            };

            // act
            var handler = factory.CreateHandler(settings);

            // assert
            Assert.Equal(default, handler);
        }

        [Fact(DisplayName = "When TypeId matches an unsupported Form Handler Type should throw a Not Supported Exception")]
        public void WhenTypeIdMatchesAnUnsupportedFormHandlerTypeShouldThrowException()
        {
            // arrange
            var factory = CreateFactory();
            var settings = new TestFormHandlerSettings()
            {
                TypeId = Guid.Parse(Constants.TestUnsupportedFormHandlerTypeId)
            };

            // act / assert
            Assert.Throws<NotSupportedException>(() => factory.CreateHandler(settings));
        }

        [Fact(DisplayName = "When TypeId matches an Async Form Handler Type should return an Async Form Handler")]
        public void WhenTypeIdMatchesAnAsyncFormHandlerTypeShouldReturnAnAsyncFormHandler()
        {
            // arrange
            var factory = CreateFactory();
            var settings = new TestFormHandlerSettings()
            {
                TypeId = Guid.Parse(Constants.TestAsyncFormHandlerTypeId)
            };
            
            // act
            var handler = factory.CreateHandler(settings);

            // assert
            Assert.IsAssignableFrom<AsyncFormHandler>(handler);
        }

        [Fact(DisplayName = "When TypeId matches a Form Handler Type should return a Form Handler")]
        public void WhenTypeIdMatchesAFormHandlerTypeShouldReturnAFormHandler()
        {
            // arrange
            var factory = CreateFactory();
            var settings = new TestFormHandlerSettings()
            {
                TypeId = Guid.Parse(Constants.TestFormHandlerTypeId)
            };

            // act
            var handler = factory.CreateHandler(settings);

            // assert
            Assert.IsAssignableFrom<FormHandler>(handler);
        }
        
        private static IFormHandlerFactory CreateFactory()
        {
            var items = new List<IFormHandlerType>
            {
                new TestAsyncFormHandlerType(), 
                new TestFormHandlerType(), 
                new TestUnsupportedFormHandlerType()
            };

            var collection = new FormHandlerTypeCollection(() => items);

            return new FormHandlerFactory(collection);
        }
    }
}
