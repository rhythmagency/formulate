namespace formulate.app.Forms
{

    // Namespaces.
    using core.Types;
    using System;
    using System.Collections.Generic;


    /// <summary>
    /// Interface for all form handlers.
    /// </summary>
    public interface IFormHandler
    {
        Guid TypeId { get; set; }
        Guid Id { get; set; }
        string Alias { get; set; }
        string Name { get; set; }
        string HandlerConfiguration { get; set; }
        string GetIcon();
        string GetDirective();
        string GetTypeLabel();
        Type GetHandlerType();
        object DeserializeConfiguration();
        void HandleForm(Form form, IEnumerable<FieldSubmission> data,
            IEnumerable<FileFieldSubmission> files, IEnumerable<PayloadSubmission> payload);
    }

}