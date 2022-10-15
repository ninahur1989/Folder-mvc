using Folders.Data;
using Folders.Data.DbModels;
using Folders.Interfaces;
using Folders.Models;
using Folders.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Folders.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;
        private readonly IHomeService _service;

        public HomeController(ILogger<HomeController> logger, AppDbContext context, IHomeService homeService)
        {
            _logger = logger;
            _context = context;
            _service = homeService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Catalog(int id)
        {
            if (id >= 1)
            {
                var thisFolder = _context.Folders.FirstOrDefault(x => x.Id == id);
                ViewBag.Name = thisFolder.Name;

                var attachedFolder = _context.Folders.Where(x => x.FolderId == id).ToList();

                return View(attachedFolder);
            }
            return Ok("error");

        }


        public IActionResult ImportFromOS()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ImportFromOS(Folder folder)
        {
            if (_service.ImportDataFromOS(folder))
            {
                return RedirectToAction("Catalog", new { id = 1 });
            }

            return Ok("error");
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
    }
}
