using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Music_2.ApiIntegration.Slide;
using Music_2.Front.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Music_2.Front.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISlideApiClient _slideApiClient;

        public HomeController(ILogger<HomeController> logger, ISlideApiClient slideApiClient)
        {
            _logger = logger;
            _slideApiClient = slideApiClient;
        }

        public IActionResult Index()
        {
            return View();
        }
        public PartialViewResult Slide()
        {
            var model = _slideApiClient.GetAll();
            return PartialView(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        


        public IActionResult SetCultureCookie(string cltr, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(cltr)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }
    }
}
