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
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        // Only for Expando example
        //public ActionResult Champions()
        //{
            
        //    var cx = new MVC4LOLDb();

        //    var model = cx.Champions;

        //    var userId = 1; // HARDCODE!!

        //    //var list = cx.Attributes.Where(o => o.UserId == userId).Select(o => o.Name).Distinct().ToList();//.GroupBy(o => o.Name).Select(c => new {Naam = c.Key}).ToList();
        //    var list = cx.Tags.Where(o => o.UserId == userId).GroupBy(o => o.Name)
        //                                .Select(c => new { Naam = c.Key }).ToExpandoList();
        //    //var list = cx.Attributes.Where(o => o.UserId == userId).GroupBy(o => o.Name) // It ain't working!
        //    //                            .Select(c => new { Naam = c.Key }).Select(o => o.ToExpando()).ToList();

        //    ViewBag.Tags = list;

        //    return View(model);
        //}
        
    }
}
