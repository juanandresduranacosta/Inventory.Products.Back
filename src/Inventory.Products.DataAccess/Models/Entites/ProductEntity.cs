using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Products.DataAccess.Models.Entites
{
    public class ProductEntity
    {
        public int Id { get; set; }
        public int TypeElaboration { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int CreationUser { get; set; }
    }
}
