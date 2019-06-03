namespace formulate.app.Configuration
{
    public interface IFormulateConfig
    {
        IPersistenceConfig Persistence { get; }
    }
}
