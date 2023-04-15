using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Music_2.ApiIntegration.FeedBack;
using Music_2.Data.EF;
using Music_2.Data.Models.FeedBack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_2.FrontAdmin.Controllers
{
    public class FeedBackController : Controller
    {
        private readonly IFeedBackApiClient _fbApiClient;
        private readonly IConfiguration _configuration;
        private readonly OnlineMusicDbContext _context;

        public FeedBackController(IFeedBackApiClient fbApiClient, IConfiguration configuration, OnlineMusicDbContext context)
        {
            _fbApiClient = fbApiClient;
            _configuration = configuration;
            _context = context;
        }
        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 5)
        {
            var request = new GetFeedBackPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = await _fbApiClient.GetAllPaging(request);
            ViewBag.Keyword = keyword;
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            return View(data);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View(new FeedBackDeleteRequest()
            {
                Id = id
            });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(FeedBackDeleteRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _fbApiClient.Delete(request.Id);
            if (result)
            {
                TempData["result"] = "Xóa feedback thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Xóa feedback không thành công");
            return View(request);
        }

        //export
        public IActionResult ExportCsv()
        {
            var users = _context.FeedBacks.ToList();
            var builder = new StringBuilder();
            builder.AppendLine("Id,Date,Email,Content");
            foreach (var user in users)
            {
                builder.AppendLine($"{user.ID},{user.CreateDate},{user.Email},{user.FeedBackContent}");
            }
            var name = "feedbacks";
            return File(Encoding.UTF8.GetBytes(builder.ToString()), "text/csv", $"{name}.csv");
        }
    }
}
