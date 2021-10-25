using System;
using System.Collections.Generic;
using Formulate.Core.Layouts;

namespace Formulate.Core.Tests.LayoutTests
{
    public partial class LayoutFactoryTests
    {
        private static class Constants
        {
            public const string MissingLayoutTypeId = "EE529CCB5777482899875AA37A4036E3";

            public const string TestLayoutTypeId = "A00C4BE8788B4DC1AC488E6CAAA82F3C";
        }

        private sealed class TestLayoutSettings : ILayoutSettings
        {
            public Guid TypeId { get; set; }

            public string Name { get; set; }

            public Guid Id { get; set; }
            
            public string Configuration { get; set; }

            public string Directive { get; set; }
        }

        private sealed class TestLayoutType : ILayoutType
        {
            public Guid TypeId => Guid.Parse(Constants.TestLayoutTypeId);
            
            public string TypeLabel => "Test Layout";
            
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
