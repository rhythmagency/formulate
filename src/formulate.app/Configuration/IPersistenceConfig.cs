namespace formulate.app.Configuration
{
    using formulate.app.Templates;
    using System.Collections.Generic;

    public interface IPersistenceConfig
    {
        string JsonBasePath { get; }
        string FileStorageBasePath { get; }
    }

    public interface ITemplatesConfig
    {
        IEnumerable<Template> Templates { get; }
    }
}
