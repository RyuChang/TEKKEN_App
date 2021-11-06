using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using TEKKEN_WEB;

namespace Localization.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStringLocalizer<HomeController> _localizer;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public HomeController(IStringLocalizer<HomeController> localizer,
                       IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _localizer = localizer;
            _sharedLocalizer = sharedLocalizer;
        }

        public IActionResult Index()
        {
            ViewData["Message"] = @CookieRequestCultureProvider.DefaultCookieName;
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = _localizer["Your application description page."];

            return View();
        }

        public IActionResult About2()
        {
            ViewData["Message"] = _localizer["Your application description page."];

            return View();
        }


        public IActionResult Contact()
        {
            ViewData["Message"] = _localizer["Your contact page."];
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
