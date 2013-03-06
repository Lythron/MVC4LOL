using MVC4LOL.Models.ViewModels;
using MVC4LOL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC4LOL.Controllers
{
    public class ChampionJsonController : Controller
    {
        public ActionResult Index()
        { 
            UsersContext us = new UsersContext();
            var m = us.Champions.ToList();

            return Json(m, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index2()
        {
            return View();
        }


        public ActionResult IndexModel()
        {
            ChampionsViewModel model = new ChampionsViewModel();
            UsersContext us = new UsersContext();
            model.Champions = us.ChampionData.ToList();
            model.Patches = us.PatchVersions.ToList();
            var  userId = 1; // HARDCODE
            model.Tags = us.Tags.Where(o => o.UserId == userId).Select(o => o.Name).Distinct().ToList();

            var jsonResult = Json(ConvertToModelWithStringImage(model), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
            
        }


        // TODO : REFACTOR!!!!111
        public ChampionWithStringImage ConvertToBase64String(Model.ChampionData champion)
        {

            ChampionWithStringImage outputChampion = new ChampionWithStringImage();
            outputChampion.champion = champion;
            outputChampion.imageBase64 = champion.Image != null ? System.Convert.ToBase64String(champion.Image) : String.Empty;
            return outputChampion;
        }

        public ModelWithChampionsWithStringImage ConvertToModelWithStringImage(ChampionsViewModel model)
        {
            ModelWithChampionsWithStringImage outputModel = new ModelWithChampionsWithStringImage();
            outputModel.championsWithStrImg = model.Champions.Select(o => ConvertToBase64String(o)).ToList();
            outputModel.patches = model.Patches;
            outputModel.tags = model.Tags;
            return outputModel;

        }

        public class ChampionWithStringImage // TODO: Refactor without using custom class;
        {
            public Model.ChampionData champion { get; set; }
            public string imageBase64 { get; set; }
        }

        public class ModelWithChampionsWithStringImage
        {
            public List<ChampionWithStringImage> championsWithStrImg { get; set; }
            public List<String> tags { get; set; }
            public List<Model.PatchVersion> patches { get; set; }
        }

        //public Model.ChampionData ConvertToBase64String(Model.ChampionData champion)
        //{

        //    return champion.Exte
        //    Object outputChampion = champion;

        //    (Model.ChampionData)outputChampion.Image = System.Convert.ToBase64String(champion.Image);
        //    return outputChampion;
        //}

        //public T Add(string name, object value)
        //{
        //    if (ExtendedProperties == null)
        //    {
        //        ExtendedProperties = new Dictionary<string, object>();
        //    }
        //    ExtendedProperties[name] = value;
        //    return (T)(object)this;
        //}
        

    }
}
