using System.Collections.Generic;

namespace WebAPI.Entity
{
    public class ShopperHistory
    {
        public long CustomerId { get; set; }
        public List<Product> Products { get; set; }
    }
}
