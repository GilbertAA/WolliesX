using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using UrlCombineLib;
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
            var finalUrl = ConstructFinalUrl<T>(path);
            return await HttpClientWrapper.Get<T>(finalUrl);
        }

        private static Uri ConstructFinalUrl<T>(string path)
        {
            var url = UrlCombine.Combine(Constants.UrlResourceBase, path);
            var tokenParam = new Dictionary<string, string>
            {
                {Constants.UrlParamToken, Constants.Token}
            };
            var finalUrl = new Uri(QueryHelpers.AddQueryString(url, tokenParam));
            return finalUrl;
        }
    }
}
