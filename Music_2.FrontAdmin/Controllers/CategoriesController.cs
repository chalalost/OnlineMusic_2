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
        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var request = new GetCategoriesPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = await _categoryApiClient.GetAllPaging(request);
            ViewBag.Keyword = keyword;
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            return View(data.ResultObj);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id, string languageId)
        {
            var result = await _categoryApiClient.GetById(languageId,id);
            return View(result.ResultObj);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _categoryApiClient.Create(request);
            if (result.IsSuccessed)
            {
                TempData["result"] = "Thêm mới danh mục thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", result.Message);
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id, string languageId)
        {
            var result = await _categoryApiClient.GetById(languageId, id);
            if (result.IsSuccessed)
            {
                var cate = result.ResultObj;
                var editVm = new CategoryUpdateRequest()
                {
                    Id = cate.Id,
                    Name = cate.Name,
                    SeoAlias = cate.SeoAlias,
                    SeoDescription = cate.SeoDescription,
                    SeoTitle = cate.SeoTitle
                };
                return View(editVm);
            }
            return RedirectToAction("Error", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(CategoryUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _categoryApiClient.Update(request.Id, request);
            if (result.IsSuccessed)
            {
                TempData["result"] = "Cập nhật người dùng thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", result.Message);
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
                TempData["result"] = "Xóa sản phẩm thành công";
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

            return File(Encoding.UTF8.GetBytes(builder.ToString()), "text/csv", "users.csv");
        }
    }
}
