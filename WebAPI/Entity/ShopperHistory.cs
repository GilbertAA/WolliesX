﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Entity
{
    public class ShopperHistory
    {
        public long CustomerId { get; set; }
        public List<Product> Products { get; set; }
    }
}
