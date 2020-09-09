using System.Collections.Generic;
using WebAPI.Entity;
using WebAPI.Enum;

namespace WebAPI.Services
{
    public interface IProductServices
    {
        List<Product> GetProducts(SortOption sortOption);
    }
}