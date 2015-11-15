using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocksly.Domain.Inventory
{
    public class ProductBrief
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string DisplayName { get; set; }

        public int SupplierId { get; set; }
        public int CategoryId { get; set; }
    }
}
