using Microsoft.AspNetCore.Mvc;
using Music_2.BackApi.Services.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music_2.BackApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(string languageId)
        {
            var cates = await _categoryService.GetAll(languageId);
            return Ok(cates);
        }
    }
}
