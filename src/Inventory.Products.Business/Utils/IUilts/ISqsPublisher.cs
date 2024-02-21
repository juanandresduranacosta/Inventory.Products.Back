namespace Inventory.Products.Business.Utils.IUilts
{
    public interface ISqsPublisher
    {
        Task<string> PublishMessage(dynamic obj);
    }
}
