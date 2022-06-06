using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Music_2.BackApi.Services.Category;
using Music_2.Data.Models.Catalog.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music_2.BackApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] CategoryCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var cateId = await _categoryService.Create(request);
            if (cateId == 0)
                return BadRequest();

            var cate = await _categoryService.GetById(request.LanguageId, cateId);

            return CreatedAtAction(nameof(GetById), new { id = cateId }, cate);
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll(string languageId)
        {
            var cates = await _categoryService.GetAll(languageId);
            return Ok(cates);
        }

        [HttpGet("{cateId}/{languageId}")]
        public async Task<IActionResult> GetById(int cateId, string languageId)
        {
            var product = await _categoryService.GetById(languageId, cateId);
            if (product == null)
                return BadRequest("Cannot find category");
            return Ok(product);
        }
    }
}
