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
        [HttpPut("{cateId}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update([FromRoute] int cateId, [FromForm] CategoryUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            request.Id = cateId;
            var affectedResult = await _categoryService.Update(request);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }

        [HttpDelete("{cateId}")]
        public async Task<IActionResult> Delete(int cateId)
        {
            var affectedResult = await _categoryService.Delete(cateId);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(string languageId)
        {
            var cate = await _categoryService.GetAll(languageId);
            return Ok(cate);
        }

        [HttpGet("{cateId}/{languageId}")]
        public async Task<IActionResult> GetById(int cateId, string languageId)
        {
            var cate = await _categoryService.GetById(languageId, cateId);
            if (cate == null)
                return BadRequest("Cannot find category");
            return Ok(cate);
        }

        [HttpGet("paging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetCategoriesPagingRequest request)
        {
            var cate = await _categoryService.GetAllPaging(request);
            return Ok(cate);
        }
    }
}
