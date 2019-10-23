namespace formulate.app.Forms
{

    // Namespaces.
    using System;


    /// <summary>
    /// Interface for all form handlers.
    /// </summary>
    public interface IFormHandler
    {
        Guid TypeId { get; set; }
        Guid Id { get; set; }
        string Alias { get; set; }
        string Name { get; set; }
        bool Enabled { get; set; }
        string HandlerConfiguration { get; set; }
        string GetIcon();
        string GetDirective();
        string GetTypeLabel();
        Type GetHandlerType();
        object DeserializeConfiguration();
        void PrepareHandleForm(FormSubmissionContext context);
        void HandleForm(FormSubmissionContext context);
        IFormHandler GetFreshCopy();
    }

}