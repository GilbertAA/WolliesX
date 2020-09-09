using System.Collections.Generic;
using WebAPI.Entity;

namespace WebAPI.Services
{
    public interface IProductServices
    {
        List<Product> GetProducts(SortOption sortOption);
    }
}