namespace Inventory.Products.DataAccess.Models.Configurations
{
    public class LogicConfiguration
    {
        public string DatabaseConnection { get; set; }
        public string SecretKey { get; set; }
        public string UserPool { get; set; }
        public string ClientId { get; set; }
        public string QueueBulk { get; set; }

    }
}
