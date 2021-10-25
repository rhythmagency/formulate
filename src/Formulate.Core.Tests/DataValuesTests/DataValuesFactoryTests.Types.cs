using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Formulate.Core.DataValues;

namespace Formulate.Core.Tests.DataValuesTests
{
    public partial class DataValuesFactoryTests
    {
        private static class Constants
        {
            public const string MissingDataValuesDefinitionId = "8D72D9E68AF44348A2F1FEE138902BA5";

            public const string TestDataValuesDefinitionId = "B5BDFE470A07446CACA733EA99B902F4";
        }

        private sealed class TestDataValuesSettings : IDataValuesSettings
        {
            public Guid DefinitionId { get; set; }
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Configuration { get; set; }
        }

        private sealed class TestDataValuesDefinition : DataValuesDefinitionBase
        {
            public override Guid DefinitionId => Guid.Parse(Constants.TestDataValuesDefinitionId);

            public override string DefinitionLabel => "Test Data Values";

            public override string Directive => "test-data-values";

            public override async Task<IDataValues> CreateDataValuesAsync(IDataValuesSettings settings, CancellationToken cancellationToken = default)
            {
                return await Task.Run(() => new TestDataValues(settings, new List<KeyValuePair<string, string>>()), cancellationToken);
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
