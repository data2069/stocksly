﻿using Stocksly.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocksly.Domain.Suppliers
{
    public class Supplier
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string Telephone { get; set; }
        public Address PostalAddress { get; set; }
        public string CreditToken { get; set; }
    }
}
