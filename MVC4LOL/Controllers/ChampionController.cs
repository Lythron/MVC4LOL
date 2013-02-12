using MVC4LOL.Model;
using MVC4LOL.Models;
using MVC4LOL.Models.ViewModels;
using MVC4LOL.Repository;
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

        public ActionResult Tags(Int32 championId)
        {
            var model = _cx.Tags.Where(o => o.ChampionId == championId);
            return View("Tags", model);
        }

        [HttpGet]
        public ActionResult CreateTag(Int32 championId)
        {
            var modelView = new EditTagsViewModel();
            modelView.ChampionId = championId;

            //var model = _cx.Attributes.Where(o => o.ChampionId == championId);
            return View(modelView);
        }

        [HttpPost]
        public ActionResult CreateTag(EditTagsViewModel model)
        {
            if (ModelState.IsValid)
            {
                Tag tag = new Tag();
                tag.ChampionId = model.ChampionId;
                tag.UserId = 1;
                tag.Name = model.Name;

                _cx.Tags.Add(tag);
                _cx.SaveChanges();
                return Details2(model.ChampionId);
            }
            else
            {
                return View(model);
            }
        }

        public ActionResult DeleteTag(Int32 tagId)
        {
            Tag tag = _cx.Tags.First(o => o.Id == tagId);
            _cx.Tags.Remove(tag);
            _cx.SaveChanges();
            return Tags(tag.ChampionId);
        }

        public ActionResult GetPhoto(int photoId)
        {
            byte[] photo = _cx.Champions.First(c => c.Id == photoId).Image;
            return photo != null ? File(photo, "image/jpeg") : (ActionResult)(null);
        }

    }
}
