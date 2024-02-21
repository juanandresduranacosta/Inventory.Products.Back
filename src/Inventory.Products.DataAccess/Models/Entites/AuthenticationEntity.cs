namespace Inventory.Products.DataAccess.Models.Entites
{
    public class AuthenticationEntity
    {
        public string JWT { get; set; }
        public int ExpiredIn { get; set; }
    }
}
