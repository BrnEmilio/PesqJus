using codeyes.msc.each.Mediator;
using codeyes.msc.each.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.IO;

namespace codeyes.msc.each.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ScrapperService _scrapperService;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;         
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Report()
        {
            Report report = null;
            string site = (TempData["url"] == null ? "" : TempData["url"].ToString());
            if (!string.IsNullOrEmpty(site))
            {
                _scrapperService = new ScrapperService();
                report = _scrapperService.GetAnalyticsAsync(site);
            }

            return View(report);
        }

        public IActionResult WebSite(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                TempData["url"] = url;
                return RedirectToAction("Report", "Home");
            }
            else
            {
                return View(url);
            }
        }

        public IActionResult Build(Report model)
        {
            if (model.DateTime != null)
            {
                //GetUniqueFileName(model.MyImage.FileName);
                model.DateTime = DateTime.Now;

                //ViewData["Message"] = string.Format("Consegui ler o arquivo {0}.\\n Data: {1}", uniqueFileName, DateTime.Now.ToString());
                return RedirectToAction("Report", "Home");
            }
            else
            {
                // ViewData["Message"] = string.Format("Nenhum arquivo foi selecionado! {0}.\\n Data: {1}", "", DateTime.Now.ToString());
                return View(model);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}