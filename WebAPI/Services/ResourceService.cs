using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Entity;
using WebAPI.Exception;
using WebAPI.Util;

namespace WebAPI.Services
{
    public class ResourceService : IResourceService
    {
        private const string UrlPathBase = "api/resource/";
        private const string UrlPathProducts = UrlPathBase + "products";
        private const string UrlPathShopperHistory = UrlPathBase + "shopperHistory";

        private readonly IHttpClientWrapper _httpClientWrapper;

        public ResourceService(IHttpClientWrapper httpClientWrapper)
        {
            _httpClientWrapper = httpClientWrapper;
        }

        public List<Product> GetProducts()
        {
            var result = GetResource<List<Product>>(UrlPathProducts).Result;
            if (result == default(List<Product>))
            {
                throw new UnableToGetResourceException("Unable to get products");
            }

            return result;
        }

        public List<ShopperHistory> GetShopperHistory()
        {
            var result = this.GetResource<List<ShopperHistory>>(UrlPathShopperHistory).Result;
            if (result == default(List<ShopperHistory>))
            {
                throw new UnableToGetResourceException("Unable to get the shopper history");
            }

            return result;
        }

        private async Task<T> GetResource<T>(string path)
        {
            var finalUrl = UrlConstructor.ConstructUri(path);
            return await _httpClientWrapper.Get<T>(finalUrl);
        }
    }
}
