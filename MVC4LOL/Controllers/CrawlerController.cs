using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC4LOL.CRAWLER;

using MVC4LOL.Models;
using System.Data.Entity;
using System.Dynamic;
using System.Web.Routing;
using MVC4LOL.Helpers;
using MVC4LOL.Models.ViewModels;

namespace MVC4LOL.Controllers
{
    public class CrawlerController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        //[Authorize(Roles="Admin")]
        public ActionResult Wikia()
        {
            MVC4LOL.CRAWLER.Wikia.HarvestData();
            return View();
        }

    }
}
