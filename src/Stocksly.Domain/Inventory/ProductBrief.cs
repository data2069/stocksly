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
        public int Stocks { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
