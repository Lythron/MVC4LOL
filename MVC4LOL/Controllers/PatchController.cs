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
        private  UsersContext _uc;

        public PatchController()
        {
            if (_uc == null)
            {
                _uc = new UsersContext();
            }
        }

        public PatchController(UsersContext usersContext)
        {
            if (_uc == null)
            {
                _uc = usersContext;
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
