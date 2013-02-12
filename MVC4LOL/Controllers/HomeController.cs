using MVC4LOL.CRAWLER;
using MVC4LOL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MVC4LOL.Helpers;
using MVC4LOL.Models.ViewModels;
using MVC4LOL.Repository;

namespace MVC4LOL.Controllers
{
    //[Authorize(Roles = "User")]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //[Authorize(Roles="Admin")]
        // TODO : MOve to Crawler controller
        public ActionResult Crawlers()
        {
            //TODO: 
            Wikia.HarvestData();

            return View();

            //return View("About");
        }

        // Only for Expando example
        public ActionResult Champions()
        {
            
            var cx = new UsersContext();

            var model = cx.Champions;

            var userId = 1; // HARDCODE!!

            //var list = cx.Attributes.Where(o => o.UserId == userId).Select(o => o.Name).Distinct().ToList();//.GroupBy(o => o.Name).Select(c => new {Naam = c.Key}).ToList();
            var list = cx.Tags.Where(o => o.UserId == userId).GroupBy(o => o.Name)
                                        .Select(c => new { Naam = c.Key }).ToExpandoList();
            //var list = cx.Attributes.Where(o => o.UserId == userId).GroupBy(o => o.Name) // It ain't working!
            //                            .Select(c => new { Naam = c.Key }).Select(o => o.ToExpando()).ToList();

            ViewBag.Tags = list;

            return View(model);
        }
        
    }
}
