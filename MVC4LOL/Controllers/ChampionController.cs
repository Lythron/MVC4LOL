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
            var userId = 1; // HARDCODE;
            model.Patches = _cx.PatchVersions.ToList();

            //model.Champions = _cx.Champions.Select(o => ChampionData()) TODO:

            Int32 latestPatchId = model.Patches.OrderByDescending(p => p.Date).First().Id;
            model.Champions = _cx.ChampionData
                                 .Where(o => o.PatchVersionId == latestPatchId)
                                 .ToList();


            //model.Tags = _cx.Tags.Where(o => o.UserId == userId).GroupBy(o => o.Name).Select(o => o.Key).ToList();
            model.Tags = _cx.Tags.Where(o => o.UserId == userId).ToList();
            return View(model);
        }

        public ActionResult Details(String name)
        {
            Int32 championId = _cx.Champions.FirstOrDefault(o => o.Name.Equals(name)).Id;
            var userId = 1;
            var model = new ChampionDetailsViewModel();

            model.Patches = _cx.PatchVersions.ToList();
            Int32 latestPatchId = model.Patches.OrderByDescending(o => o.Date).First().Id;
            //ViewBag.SelectedPatchId = latestPatchId;
            model.SelectedPatchId = latestPatchId;

            model.Tags = _cx.Tags.Where(o => o.UserId == userId && o.ChampionId == championId).ToList();
            model.Champions = _cx.ChampionData.Where(o => o.ChampionId == championId).ToList();

            return View(model);
        }

        public ActionResult Details2(Int32 championId)
        {
            var userId = 1;
            var model = new ChampionDetailsViewModel();

            model.Patches = _cx.PatchVersions.ToList();
            Int32 latestPatchId = model.Patches.OrderByDescending(o => o.Date).First().Id;
            //ViewBag.SelectedPatchId = latestPatchId;
            model.SelectedPatchId = latestPatchId;

            model.Tags = _cx.Tags.Where(o => o.UserId == userId && o.ChampionId == championId).ToList();
            model.Champions = _cx.ChampionData.Where(o => o.ChampionId == championId).ToList();
            
            return View("Details", model);
        }

        public ActionResult Tags(Int32 championId)
        {
            TagsViewModel model = new TagsViewModel();
            model.Tags = _cx.Tags.Where(o => o.ChampionId == championId).ToList();
            model.Champion = _cx.Champions.First(o => o.Id == championId);
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
                return Tags(model.ChampionId);
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
            byte[] photo = _cx.ChampionData.First(c => c.Id == photoId).Image;
            return photo != null ? File(photo, "image/jpeg") : (ActionResult)(null);
        }

    }
}
