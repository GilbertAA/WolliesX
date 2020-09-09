using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Entity;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [ApiController]
    public class TrolleyController : ControllerBase
    {
        private readonly ITrolleyService _trolleyService;

        public TrolleyController(ITrolleyService trolleyService)
        {
            _trolleyService = trolleyService;
        }

        [HttpPost]
        [Route("api/trolley/trolleyTotal")]
        public IActionResult CalculateTrolleyTotal([FromBody] TrolleyInput trolleyInput)
        {
            try
            {
                return Ok(_trolleyService.CalculateTrolley(trolleyInput));
            }
            catch (System.Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
        [HttpGet]
        [Route("Test")]
        public IActionResult CalculateTrolleyTotal()
        {
            //{
            //    "products": [
            //    {
            //        "name": "string",
            //        "price": 0
            //    }
            //    ],
            //    "specials": [
            //    {
            //        "quantities": [
            //        {
            //            "name": "string",
            //            "quantity": 0
            //        }
            //        ],
            //        "total": 0
            //    }
            //    ],
            //    "quantities": [
            //    {
            //        "name": "string",
            //        "quantity": 0
            //    }
            //    ]
            //}
            try
            {
                var input = new TrolleyInput
                {
                    products = new List<TrolleyProduct> {new TrolleyProduct {name = "test", price = 0}},
                    quantities = new List<Quantity> {new Quantity {name = "test", quantity = 0}},
                    specials = new List<Special> {new Special()}
                };
                input.specials[0].quantities = new List<Quantity>{new Quantity{name = "test", quantity = 0}};
                input.specials[0].total = 0;
                return Ok(_trolleyService.CalculateTrolley(input));
            }
            catch (System.Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

    }
}
