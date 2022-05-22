using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Music_2.BackApi.Services.Product;
using Music_2.Data.EF;
using Music_2.Data.Models.Catalog.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music_2.BackApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("paging")]
        public async Task<IActionResult> Get()
        {
            var products = await _productService.GetAll();
            return Ok("ok"); 
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] ProductCreateRequest request)
        {
            var resut = await _productService.Create(request);
            return Ok();
        }
    }
}
