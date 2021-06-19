
using ePizzaHub.Entities;
using ePizzaHub.Services.Interfaces;
using ePizzaHub.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ePizzaHub.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        ICatalogService _catalogService;
        IMemoryCache _cache;
        public HomeController(ILogger<HomeController> logger, ICatalogService catalogService, IMemoryCache cache)
        {
            _catalogService = catalogService;
            _logger = logger;
            _cache = cache;
        }

        public IActionResult Index()
        {
            //IEnumerable<Item> itemList = _catalogService.GetItems();
            //return View(itemList);

            try
            {
                int x = 2, y = 0;
                int z = x / y;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Caught");
            }
            
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult PrivacyPolicy()
        {
            return View("Privacy");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
