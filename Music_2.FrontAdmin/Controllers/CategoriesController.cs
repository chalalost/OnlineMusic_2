using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Music_2.ApiIntegration.Category;
using Music_2.Data.EF;
using Music_2.Data.Entities;
using Music_2.Data.Models.Catalog.Categories;
using Music_2.Data.Models.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_2.FrontAdmin.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryApiClient _categoryApiClient;
        private readonly IConfiguration _configuration;
        private readonly OnlineMusicDbContext _context;

        public CategoriesController(ICategoryApiClient categoryApiClient, IConfiguration configuration, OnlineMusicDbContext context)
        {
            _categoryApiClient = categoryApiClient;
            _configuration = configuration;
            _context = context;
        }
        public async Task<IActionResult> Index(string keyword, string languageId, int pageIndex = 1, int pageSize = 5)
        {
            var request = new GetCategoriesPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                LanguageId = languageId,
                PageSize = pageSize
            };
            var data = await _categoryApiClient.GetAllPaging(request);
            ViewBag.Keyword = keyword;
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            return View(data);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create(CategoryCreateRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _categoryApiClient.Create(request);
            if (result)
            {
                TempData["result"] = "Thêm mới danh mục sản phẩm thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Thêm danh mục sản phẩm thất bại");
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var languageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);
            var result = await _categoryApiClient.GetById(languageId, id);
            var editVm = new CategoryUpdateRequest()
            {
                Id = result.Id,
                Name = result.Name,
                SeoAlias = result.SeoAlias,
                SeoDescription = result.SeoDescription,
                SeoTitle = result.SeoTitle
            };
            return View(editVm);
        }
        [HttpPost]
        public async Task<IActionResult> Edit([FromForm] CategoryUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _categoryApiClient.Update( request);
            if (result)
            {
                TempData["result"] = "Cập nhật danh mục thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Cập nhật danh không mục thành công");
            return View(request);

        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View(new CategoryDeleteRequest()
            {
                Id = id
            });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(CategoryDeleteRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _categoryApiClient.Delete(request.Id);
            if (result)
            {
                TempData["result"] = "Xóa danh mục thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Xóa không thành công");
            return View(request);
        }

        //export
        public IActionResult ExportCsv()
        {
            var users = _context.Categories.ToList();
            var builder = new StringBuilder();
            builder.AppendLine("Id,Name");
            foreach (var user in users)
            {
                builder.AppendLine($"{user.Id},{user.Name}");
            }
            var name = "categories";
            return File(Encoding.UTF8.GetBytes(builder.ToString()), "text/csv", $"{name}.csv");
        }
    }
}
