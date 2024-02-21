using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Products.DataAccess.Models.Entites
{
    public class ProductsFiltersEntity
    {
        public int page { get; set; }
        public int page_size { get; set; }
        public string? name { get; set; }
        public int status { get; set; }
        public int typeElaboration { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
    }
}
