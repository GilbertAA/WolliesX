using System.Collections.Generic;
using System.Linq;
using WebAPI.Entity;
using WebAPI.Enum;

namespace WebAPI.Services
{
    public class ProductServices : IProductServices
    {
        private readonly IResourceService _resourceService;

        public ProductServices(IResourceService resourceService)
        {
            _resourceService = resourceService;
        }

        public List<Product> GetProducts(SortOption sortOption)
        {
            return sortOption == SortOption.Recommended ? GetSortedRecommended() : GetSorted(sortOption);
        }

        private List<Product> GetSorted(SortOption sortOption)
        {
            var products = _resourceService.GetProducts();
            if (products != null && products.Any())
            {
                return sortOption switch
                {
                    SortOption.High => products.OrderByDescending(p => p.Price).ToList(),
                    SortOption.Low => products.OrderBy(p => p.Price).ToList(),
                    SortOption.Ascending => products.OrderBy(p => p.Name).ToList(),
                    SortOption.Descending => products.OrderByDescending(p => p.Name).ToList(),
                    _ => products
                };
            }
            return new List<Product>();
        }

        private List<Product> GetSortedRecommended()
        {
            // get all products
            var products = _resourceService.GetProducts();
            // get products in customer order
            var customerOrders = _resourceService.GetShopperHistory();
            var productsInCustomerOrder = customerOrders.SelectMany(c => c.Products).ToList();
            var recommendedProducts = products.Select(
                product => new RecommendedProduct
                {
                    Product = product, 
                    SoldCount = productsInCustomerOrder.Count(p => p.Name == product.Name)
                }).ToList();

            var sorted = recommendedProducts.OrderByDescending(x => x.SoldCount).ToList();
            return sorted.Select(x => x.Product).ToList();
        }
    }
}
