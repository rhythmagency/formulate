namespace formulate.app.Forms
{
    using System;
    public interface IFormFieldType
    {
        string Directive { get; }
        string TypeLabel { get; }
        string Icon { get; }
        Guid TypeId { get; }
    }
}