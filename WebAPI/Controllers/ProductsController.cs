using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using WebAPI.Entity;
using WebAPI.Exception;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductServices _productServices;

        public ProductsController()
        {
            _productServices = new ProductServices();
        }

        [HttpGet]
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
