namespace WebAPI.Entity
{
    public class RecommendedProduct
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public long Quantity { get; set; }
        public long SoldCount { get; set; }

        public Product ToProduct()
        {
            return new Product
            {
                Name = Name, 
                Price = Price, 
                Quantity = Quantity
            };
        }
    }
}
