using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Products.DataAccess.Models.Dtos
{
    public class ProductsListDto
	{
		public int Id { get; set; }
		public string TypeElaboration { get; set; }
		public string Name { get; set; }
		public string Status { get; set; }
		public DateTime CreationDate { get; set; }
		public DateTime? UpdateDate { get; set; }
        public string CreationUser { get; set; }
    }
}
