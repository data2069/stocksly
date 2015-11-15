using Stocksly.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocksly.Domain.Customers
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string EmailAddress { get; set; }
        public Address PostalAddress { get; set; }
    }
}
