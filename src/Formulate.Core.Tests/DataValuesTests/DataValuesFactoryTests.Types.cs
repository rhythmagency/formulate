namespace Formulate.Core.Tests.DataValuesTests
{
    // Namespaces.
    using DataValues;
    using System;
    using System.Collections.Generic;

    public partial class DataValuesFactoryTests
    {
        private static class Constants
        {
            public const string MissingDataValuesKindId = "8D72D9E68AF44348A2F1FEE138902BA5";

            public const string TestDataValuesKindId = "B5BDFE470A07446CACA733EA99B902F4";
        }

        private sealed class TestDataValuesSettings : IDataValuesSettings
        {
            public Guid KindId { get; set; }
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Data { get; set; }
        }

        private sealed class TestDataValuesDefinition : DataValuesDefinitionBase
        {
            public override Guid KindId => Guid.Parse(Constants.TestDataValuesKindId);

            public override string DefinitionLabel => "Test Data Values";

            public override string Directive => "test-data-values";

            public override string Icon => "icon-test";

            public override IDataValues CreateDataValues(IDataValuesSettings settings)
            {
                return new TestDataValues(settings, new List<KeyValuePair<string, string>>());
            }

            public override object GetBackOfficeConfiguration(IDataValuesSettings settings)
            {
                return default;
            }
        }

        private sealed class TestDataValues : DataValuesBase
        {
            public TestDataValues(IDataValuesSettings settings, IReadOnlyCollection<KeyValuePair<string, string>> items) : base(settings, items)
            {
            }
        }
    }
}