using System;
using System.Collections.Generic;
using Formulate.Core.Layouts;

namespace Formulate.Core.Tests.LayoutTests
{
    public partial class LayoutFactoryTests
    {
        private static class Constants
        {
            public const string MissingLayoutKindId = "EE529CCB5777482899875AA37A4036E3";

            public const string TestLayoutKindId = "A00C4BE8788B4DC1AC488E6CAAA82F3C";
        }

        private sealed class TestLayoutSettings : ILayoutSettings
        {
            public Guid KindId { get; set; }

            public string Name { get; set; }

            public Guid Id { get; set; }
            
            public string Configuration { get; set; }
        }

        private sealed class TestLayoutDefinition : ILayoutDefinition
        {
            public Guid KindId => Guid.Parse(Constants.TestLayoutKindId);
            
            public string DefinitionLabel => "Test Layout";
            
            public string Directive => "formulate-test-layout";
            
            public ILayout CreateLayout(ILayoutSettings settings)
            {
                return new TestLayout(settings);
            }
        }

        private sealed class TestLayout : Layout
        {
            public TestLayout(ILayoutSettings settings) : base(settings)
            {
            }
        }
    }
}
