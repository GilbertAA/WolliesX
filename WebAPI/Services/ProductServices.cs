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
            var history = _resourceService.GetShopperHistory();
            var productDict = new Dictionary<string, RecommendedProduct>();

            foreach (var shopperHistory in history)
            {
                foreach (var product in shopperHistory.Products)
                {
                    var prodName = product.Name;
                    if (productDict.ContainsKey(prodName))
                    {
                        var prod = productDict[prodName];
                        prod.Quantity += product.Quantity;
                        prod.SoldCount += 1;
                    }
                    else
                    {
                        var newProd = new RecommendedProduct { Name = product.Name, Price = product.Price, Quantity = product.Quantity, SoldCount = 1 };
                        productDict.Add(prodName, newProd);
                    }
                }
            }

            return productDict.Values.ToList()
                .OrderByDescending(p => p.SoldCount)
                .ThenByDescending(p => p.Quantity)
                .Select(p => p.ToProduct()).ToList();
        }
    }
}
