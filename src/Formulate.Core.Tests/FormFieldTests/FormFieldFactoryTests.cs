//TODO: Commented out until I figure out how to pass the appropriate parameters to the FormFieldFactory constructor.

//namespace Formulate.Core.Tests.FormFieldTests
//{
//    // Namespaces.
//    using FormFields;
//    using System;
//    using System.Collections.Generic;
//    using Xunit;

//    public partial class FormFieldFactoryTests
//    {
//        [Fact(DisplayName = "When no settings provided should throw an Argument Null Exception")]
//        public void WhenNoSettingsProvidedShouldThrowArgumentNullException()
//        {
//            // arrange
//            var factory = CreateFactory();
//            TestFormFieldSettings settings = default;

//            // act / asset
//            Assert.Throws<ArgumentNullException>(() => factory.Create(settings));
//        }

//        [Fact(DisplayName = "When no KindId matches should return Default")]
//        public void WhenNoKindIdMatchesShouldReturnDefault()
//        {
//            // arrange
//            var factory = CreateFactory();
//            var settings = new TestFormFieldSettings()
//            {
//                KindId = Guid.Parse(Constants.MissingFormFieldKindId)
//            };

//            // act
//            var formField = factory.Create(settings);

//            // assert
//            Assert.Equal(default, formField);
//        }

//        [Fact(DisplayName = "When KindId matches a Form Field Definition should return an expected Form Field")]
//        public void WhenKindIdMatchesAFormFieldDefinitionShouldReturnAnExpectedFormField()
//        {
//            // arrange
//            var factory = CreateFactory();
//            var settings = new TestFormFieldSettings()
//            {
//                KindId = Guid.Parse(Constants.TestFormFieldKindId)
//            };

//            // act
//            var formField = factory.Create(settings);

//            // assert
//            Assert.IsType<TestFormField>(formField);
//            Assert.NotEqual(default, formField);
//        }

//        private static IFormFieldFactory CreateFactory()
//        {
//            var items = new List<IFormFieldDefinition>()
//            {
//                new TestFormFieldDefinition()
//            };

//            var collection = new FormFieldDefinitionCollection(() => items);

//            return new FormFieldFactory(collection);
//        }
//    }
//}