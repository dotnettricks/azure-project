
using ePizzaHub.Entities;
using ePizzaHub.Services.Interfaces;
using ePizzaHub.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ePizzaHub.WebUI.Extensions;

namespace ePizzaHub.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        ICatalogService _catalogService;
        IMemoryCache _cache;
        IDistributedCache _distributedCache;

        public HomeController(ILogger<HomeController> logger, ICatalogService catalogService, IMemoryCache cache, IDistributedCache distributedCache)
        {
            _catalogService = catalogService;
            _logger = logger;
            _cache = cache;
            _distributedCache = distributedCache;
        }

        public async Task<IActionResult> Index()
        {
            //LoadItems();
            IEnumerable<Item> itemList = await LoadItems();
            return View(itemList);

            //try
            //{
            //    int x = 2, y = 0;
            //    int z = x / y;
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError(ex, "Exception Caught");
            //}
            
            //return View();
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

        

        private async Task<IEnumerable<Item>> LoadItems()
        {
            IEnumerable<Item> listItems = null;
            string recordKey = "ItemList_" + DateTime.Now.ToString("yyyyMMdd_hh");

            listItems = await _distributedCache.GetRecordAsync<IEnumerable<Item>>(recordKey);

            if(listItems is null)
            {
                listItems = _catalogService.GetItems();
               
                await _distributedCache.SetRecordAsync(recordKey, listItems);
            }
            

            return listItems;
        }
    }
}
