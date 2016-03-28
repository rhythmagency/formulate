namespace formulate.app.Forms
{
    using core.Types;
    using System;
    using System.Collections.Generic;
    public interface IFormHandlerType
    {
        string Directive { get; }
        string TypeLabel { get; }
        string Icon { get; }
        Guid TypeId { get; }
        object DeserializeConfiguration(string configuration);
        void HandleForm(Form form, IEnumerable<FieldSubmission> data, object configuration);
    }
}