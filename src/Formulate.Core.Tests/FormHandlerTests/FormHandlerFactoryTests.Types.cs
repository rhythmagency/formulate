using System;
using System.Threading;
using System.Threading.Tasks;
using Formulate.Core.FormHandlers;

namespace Formulate.Core.Tests.FormHandlerTests
{
    public partial class FormHandlerFactoryTests
    {
        private static class Constants
        {
            public const string MissingFormHandlerTypeId = "6EB639979DA349198EB29ED35547F740";
            
            public const string TestFormHandlerTypeId = "91FF00DAE0F444B2AEF85A948C5E6074";

            public const string TestAsyncFormHandlerTypeId = "2E3ADACB99394555900F4AC4F9DAA6EE";

            public const string TestUnsupportedFormHandlerTypeId = "85918528E44944E692FFD7ABB71D0093";
        }

        private class TestFormHandlerSettings : IFormHandlerSettings
        {
            public Guid TypeId { get; set; }
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Alias { get; set; }
            public bool Enabled { get; set; }
            public string Configuration { get; set; }
        }

        private sealed class TestFormHandlerType : FormHandlerType
        {
            public override Guid TypeId => Guid.Parse(Constants.TestFormHandlerTypeId);
            
            public override FormHandler CreateHandler(IFormHandlerSettings settings)
            {
                return new TestFormHandler(settings);
            }
        }

        private sealed class TestAsyncFormHandlerType : AsyncFormHandlerType
        {
            public override Guid TypeId => Guid.Parse(Constants.TestAsyncFormHandlerTypeId);

            public override AsyncFormHandler CreateAsyncHandler(IFormHandlerSettings settings)
            {
                return new TestAsyncFormHandler(settings);
            }
        }

        private sealed class TestUnsupportedFormHandlerType : IFormHandlerType
        {
            public Guid TypeId => Guid.Parse(Constants.TestUnsupportedFormHandlerTypeId);
        }

        private sealed class TestFormHandler : FormHandler
        {
            public TestFormHandler(IFormHandlerSettings settings) : base(settings)
            {
            }

            public override void Handle(object submission)
            {
            }
        }

        private sealed class TestAsyncFormHandler : AsyncFormHandler
        {
            public TestAsyncFormHandler(IFormHandlerSettings settings) : base(settings)
            {
            }

            public override Task HandleAsync(object submission, CancellationToken cancellationToken = default)
            {
                return Task.CompletedTask;
            }
        }
    }
}
