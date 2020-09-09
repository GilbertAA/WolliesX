using System.Net;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Enum;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductServices _productServices;

        public ProductsController(IProductServices productServices)
        {
            _productServices = productServices;
        }

        [HttpGet]
        [Route("sort")]
        public IActionResult GetProducts( [FromQuery] SortOption sortOption)
        {
            try
            {
                return Ok(_productServices.GetProducts(sortOption));
            }
            // both UnableToGet and System.exception is treated the same as it's not a client problem
            catch (System.Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
