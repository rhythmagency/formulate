namespace formulate.deploy.Models
{

    /// <summary>
    /// Used when removing a Formulate entity from the Umbraco Cloud.
    /// </summary>
    public class RemoveEntityFromCloudRequest
    {
        public string EntityId { get; set; }
    }

}