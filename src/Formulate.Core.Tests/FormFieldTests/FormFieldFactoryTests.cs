using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Formulate.Core.FormFields;
using Xunit;

namespace Formulate.Core.Tests.FormFieldTests
{
    public partial class FormFieldFactoryTests
    {
        [Fact(DisplayName = "When no settings provided should throw an Argument Null Exception")]
        public async Task WhenNoSettingsProvidedShouldThrowArgumentNullException()
        {
            // arrange
            var factory = CreateFactory();
            TestFormFieldSettings settings = default;

            // act / asset
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await factory.CreateAsync(settings));
        }

        [Fact(DisplayName = "When no KindId matches should return Default")]
        public async Task WhenNoKindIdMatchesShouldReturnDefault()
        {
            // arrange
            var factory = CreateFactory();
            var settings = new TestFormFieldSettings()
            {
                KindId = Guid.Parse(Constants.MissingFormFieldKindId)
            };

            // act
            var formField = await factory.CreateAsync(settings);

            // assert
            Assert.Equal(default, formField);
        }

        [Fact(DisplayName = "When KindId matches a Form Field Definition should return an expected Form Field")]
        public async Task WhenKindIdMatchesAFormFieldDefinitionShouldReturnAnExpectedFormField()
        {
            // arrange
            var factory = CreateFactory();
            var settings = new TestFormFieldSettings()
            {
                KindId = Guid.Parse(Constants.TestFormFieldKindId)
            };

            // act
            var formField = await factory.CreateAsync(settings);

            // assert
            Assert.IsType<TestFormField>(formField);
            Assert.NotEqual(default, formField);
        }

        private static IFormFieldFactory CreateFactory()
        {
            var items = new List<IFormFieldDefinition>()
            {
                new TestFormFieldDefinition()
            };

            var collection = new FormFieldDefinitionCollection(() => items);
            
            return new FormFieldFactory(collection);
        }
    }
}
