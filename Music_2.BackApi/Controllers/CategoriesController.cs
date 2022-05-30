using Microsoft.AspNetCore.Mvc;
using Music_2.BackApi.Services.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music_2.BackApi.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _CategoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _CategoryService = categoryService;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(string languageId)
        {
            var cates = await _CategoryService.GetAll(languageId);
            return Ok(cates);
        }
    }
}
