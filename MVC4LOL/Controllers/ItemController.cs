using MVC4LOL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC4LOL.Controllers
{
    public class ItemController : Controller
    {
        private MVC4LOLDb db = new MVC4LOLDb();
        public ActionResult Index()
        {
            var model = db.Items.ToList();
            return View("Index", model);
        }
        public ActionResult GetItemImage(int itemId)
        {
            byte[] image = db.Items.First(c => c.Id == itemId).Image;
            return image != null ? File(image, "image/jpeg") : (ActionResult)(null);
        }

    }
}
