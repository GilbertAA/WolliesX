using System.Collections.Generic;
using WebAPI.Entity;

namespace WebAPI.Services
{
    public interface IResourceService
    {
        List<Product> GetProducts();
        List<ShopperHistory> GetShopperHistory();
    }
}