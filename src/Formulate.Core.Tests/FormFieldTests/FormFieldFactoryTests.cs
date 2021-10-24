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
        public void WhenNoSettingsProvidedShouldThrowArgumentNullException()
        {
            // arrange
            var factory = CreateFactory();
            TestFormFieldSettings settings = default;

            // act / asset
            Assert.Throws<ArgumentNullException>(() => factory.CreateField(settings));
        }

        [Fact(DisplayName = "When no TypeId matches should return Default")]
        public void WhenNoTypeIdMatchesShouldReturnDefault()
        {
            // arrange
            var factory = CreateFactory();
            var settings = new TestFormFieldSettings()
            {
                TypeId = Guid.Parse(Constants.MissingFormFieldTypeId)
            };

            // act
            var formField = factory.CreateField(settings);

            // assert
            Assert.Equal(default, formField);
        }

        [Fact(DisplayName = "When TypeId matches a Form Field Type should return an expected Form Field")]
        public void WhenTypeIdMatchesAFormFieldTypeShouldReturnAnExpectedFormField()
        {
            // arrange
            var factory = CreateFactory();
            var settings = new TestFormFieldSettings()
            {
                TypeId = Guid.Parse(Constants.TestFormFieldTypeId)
            };

            // act
            var formField = factory.CreateField(settings);

            // assert
            Assert.IsType<TestFormField>(formField);
            Assert.NotEqual(default, formField);
        }

        private static IFormFieldFactory CreateFactory()
        {
            var items = new List<IFormFieldType>()
            {
                new TestFormFieldType()
            };

            var collection = new FormFieldTypeCollection(() => items);
            
            return new FormFieldFactory(collection);
        }
    }
}
