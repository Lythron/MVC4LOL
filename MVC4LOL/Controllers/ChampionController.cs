using MVC4LOL.Models;
using MVC4LOL.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC4LOL.Controllers
{
    public class ChampionController : Controller
    {
        private UsersContext _cx { get; set; }
        public ChampionController()
        {
            if (_cx == null)
            {
                _cx = new UsersContext();
            }
        }

        public ActionResult Index()
        {
            var model = new ChampionsViewModel();
            var userId = 1;
            model.Champions = _cx.Champions.ToList();
            model.Tags = _cx.Tags.Where(o => o.UserId == userId).GroupBy(o => o.Name).Select(o => o.Key).ToList();
            return View(model);
        }
        
        public ActionResult Details(String name)
        {
            var model = _cx.Champions.Where(o => o.Name == name).FirstOrDefault();

            return View(model);
        }

        public ActionResult Details2(Int32 id)
        {
            var model = _cx.Champions.Where(o => o.Id == id).FirstOrDefault();

            return View("Details", model);
        }

        [HttpGet]
        public ActionResult CreateAttributes2(Int32 championId)
        {
            var modelView = new EditAttributesViewModel();
            modelView.ChampionId = championId;

            //var model = _cx.Attributes.Where(o => o.ChampionId == championId);
            return View(modelView);
        }

        [HttpPost]
        public ActionResult CreateAttributes2(EditAttributesViewModel model)
        {
            if (ModelState.IsValid)
            {
                MVC4LOL.Models.Tag tag = new Models.Tag();
                tag.ChampionId = model.ChampionId;
                tag.UserId = 1;
                tag.Name = model.Name;

                _cx.Tags.Add(tag);
                _cx.SaveChanges();
            }
            return View(model);
        }

        public ActionResult GetPhoto(int photoId)
        {
            byte[] photo = _cx.Champions.First(c => c.Id == photoId).Image;
            return photo != null ? File(photo, "image/jpeg") : (ActionResult)(null);
        }

    }
}
