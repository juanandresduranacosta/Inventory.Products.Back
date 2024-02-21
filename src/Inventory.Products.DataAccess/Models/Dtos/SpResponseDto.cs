namespace Inventory.Products.DataAccess.Models.Dtos
{
    public class SpResponseDto<T>
    {
        public T Data { get; set; }
        public int Total { get; set; }
    }
}
