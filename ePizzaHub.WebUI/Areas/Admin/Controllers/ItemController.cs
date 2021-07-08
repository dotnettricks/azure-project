﻿
using ePizzaHub.Entities;
using ePizzaHub.Services.Interfaces;
using ePizzaHub.WebUI.Helpers;
using ePizzaHub.WebUI.Interfaces;
using ePizzaHub.WebUI.Models;
using ePizzaHub.WebUI.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace ePizzaHub.WebUI.Areas.Admin.Controllers
{
    public class ItemController : BaseController
    {
        ICatalogService _catalogService;
        IFileHelper _fileHelper;
        IConfiguration _config;
        public ItemController(ICatalogService catalogService, IConfiguration config, IFileHelper fileHelper)
        {
            _catalogService = catalogService;
            _config = config;
            _fileHelper = fileHelper;
        }

        public IActionResult Index()
        {
            var data = _catalogService.GetItems();
            return View(data);
        }

        public IActionResult Create()
        {
            ViewBag.Categories = _catalogService.GetCategories();
            ViewBag.ItemTypes = _catalogService.GetItemTypes();
            return View();
        }

        [HttpPost]
        public IActionResult Create(ItemModel model)
        {
            try
            {
                string filename = Path.GetFileName(model.File.FileName);

                ImageBlobStorageService imgBlobService = new ImageBlobStorageService(_config);
                model.ImageUrl = imgBlobService.UploadFileToBlobAsync(filename, model.File.OpenReadStream(), model.File.ContentType).Result;
                //model.ImageUrl = _fileHelper.UploadFile(model.File);
                Item data = new Item
                {
                    Name = model.Name,
                    UnitPrice = model.UnitPrice,
                    CategoryId = model.CategoryId,
                    ItemTypeId = model.ItemTypeId,
                    Description = model.Description,
                    ImageUrl = model.ImageUrl
                };

                _catalogService.AddItem(data);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

            }
            ViewBag.Categories = _catalogService.GetCategories();
            ViewBag.ItemTypes = _catalogService.GetItemTypes();
            return View();
        }

        public IActionResult Edit(int id)
        {
            ViewBag.Categories = _catalogService.GetCategories();
            ViewBag.ItemTypes = _catalogService.GetItemTypes();
            var data = _catalogService.GetItem(id);
            ItemModel model = new ItemModel
            {
                Id = data.Id,
                Name = data.Name,
                UnitPrice = data.UnitPrice,
                CategoryId = data.CategoryId,
                ItemTypeId = data.ItemTypeId,
                Description = data.Description,
                ImageUrl = data.ImageUrl
            };
            return View("Create", model);
        }

        [HttpPost]
        public IActionResult Edit(ItemModel model)
        {
            try
            {
                if (model.File != null)
                {
                    //_fileHelper.DeleteFile(model.ImageUrl);
                    //model.ImageUrl = _fileHelper.UploadFile(model.File);
                    string filename = Path.GetFileName(model.File.FileName);

                    ImageBlobStorageService imgBlobService = new ImageBlobStorageService(_config);
                    imgBlobService.DeleteBlobData(model.ImageUrl);
                    model.ImageUrl = imgBlobService.UploadFileToBlobAsync(filename, model.File.OpenReadStream(), model.File.ContentType).Result;
                    
                }

                Item data = new Item
                {
                    Id = model.Id,
                    Name = model.Name,
                    UnitPrice = model.UnitPrice,
                    CategoryId = model.CategoryId,
                    ItemTypeId = model.ItemTypeId,
                    Description = model.Description,
                    ImageUrl = model.ImageUrl
                };

                _catalogService.UpdateItem(data);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

            }
            ViewBag.Categories = _catalogService.GetCategories();
            ViewBag.ItemTypes = _catalogService.GetItemTypes();
            return View("Create", model);
        }

        [Route("~/Admin/Item/Delete/{id}/{url}")]
        public IActionResult Delete(int id, string url)
        {
            url = url.Replace("%2F", "/"); //replace to find the file
            _catalogService.DeleteItem(id);
            //_fileHelper.DeleteFile(url);
            ImageBlobStorageService objBlob = new ImageBlobStorageService(_config);
            objBlob.DeleteBlobData(url);
            return RedirectToAction("Index");
        }
    }
}
