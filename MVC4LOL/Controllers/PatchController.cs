using MVC4LOL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC4LOL.Controllers
{
    public class PatchController : Controller
    {
        private  MVC4LOLDb _uc;

        public PatchController()
        {
            if (_uc == null)
            {
                _uc = new MVC4LOLDb();
            }
        }

        public PatchController(MVC4LOLDb dbContext)
        {
            if (_uc == null)
            {
                _uc = dbContext;
            }
        }

        public ActionResult Index()
        {
            // todo: add viewmodel
            var model = _uc.PatchVersions.ToList();
            return View(model);
        }
    }
}
