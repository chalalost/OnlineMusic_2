using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Music_2.ApiIntegration.Slide;
using Music_2.Data.EF;
using Music_2.Data.Models.Slide;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music_2.FrontAdmin.Controllers
{
    public class SlideController : Controller
    {
        private readonly ISlideApiClient _slideApiClient;
        private readonly IConfiguration _configuration;
        private readonly OnlineMusicDbContext _context;
        public SlideController(ISlideApiClient slideApiClient, IConfiguration configuration, OnlineMusicDbContext context)
        {
            _slideApiClient = slideApiClient;
            _context = context;
            _configuration = configuration;
        }
        public async Task<IActionResult> Index()
        {
            var data = await _slideApiClient.GetAll();
            return View(data);
        }
        public async Task<IActionResult> Create([FromForm] SlideCreateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _slideApiClient.Create(request);
            if (result)
            {
                TempData["result"] = "Thêm mới slide thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Thêm slide thất bại");
            return View(request);
        }
    }
}
