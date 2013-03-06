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

            //var jsonResult = Json(ConvertToModelWithStringImage(model), JsonRequestBehavior.AllowGet);
            var jsonResult = Json(ChangeByteArrayToStringForModel(model), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        
        public Object ChangeByteArrayToStringForModel(ChampionsViewModel model)
        {

            List<ChampionsViewModel> ml = new List<ChampionsViewModel>();
            ml.Add(model);

            var res = ml.Select(o => new
            {
                Tags = o.Tags,
                Patches = o.Patches,
                Champions = o.Champions.Select(c => new 
                            {
                                Image = c.Image != null ? Convert.ToBase64String(c.Image) : String.Empty,
                                Armor = c.Armor,
                                ArmorPerLvl = c.ArmorPerLvl,
                                AtkSpeed = c.AtkSpeed,
                                AtkSpeedPerLvl = c.AtkSpeedPerLvl,
                                Champion = c.Champion,
                                ChampionId = c.ChampionId,
                                Damage = c.Damage,
                                DamagePerLvl = c.DamagePerLvl,
                                Health = c.Health,
                                HealthPerLvl = c.HealthPerLvl,
                                HealthRegen = c.HealthRegen,
                                HealthRegenPerLvl = c.HealthRegenPerLvl,
                                Id = c.Id,
                                IP_Cost = c.IP_Cost,
                                MagicResist = c.MagicResist,
                                MagicResistPerLvl = c.MagicResistPerLvl,
                                Mana = c.Mana,
                                ManaPerLvl = c.ManaPerLvl,
                                ManaRegen = c.ManaRegen,
                                ManaRegenPerLvl = c.ManaRegenPerLvl,
                                MoveSpeed = c.MoveSpeed,
                                Name = c.Name,
                                PatchVersion = c.PatchVersion,
                                PatchVersionId = c.PatchVersionId,
                                Range = c.Range,
                                ReleaseDate = c.ReleaseDate,
                                RP_Cost = c.RP_Cost,
                                Title = c.Title,
                            })
            });

            return res.First();
        }

        // TODO : REFACTOR!!!!111 DONE;
        //public ChampionWithStringImage ConvertToBase64String(Model.ChampionData champion)
        //{
        //    ChampionWithStringImage outputChampion = new ChampionWithStringImage();
        //    outputChampion.champion = champion;
        //    outputChampion.imageBase64 = champion.Image != null ? System.Convert.ToBase64String(champion.Image) : String.Empty;
        //    return outputChampion;
        //}

        //public ModelWithChampionsWithStringImage ConvertToModelWithStringImage(ChampionsViewModel model)
        //{
        //    ModelWithChampionsWithStringImage outputModel = new ModelWithChampionsWithStringImage();
        //    outputModel.championsWithStrImg = model.Champions.Select(o => ConvertToBase64String(o)).ToList();
        //    outputModel.patches = model.Patches;
        //    outputModel.tags = model.Tags;
        //    return outputModel;

        //}

        //public class ChampionWithStringImage 
        //{
        //    public Model.ChampionData champion { get; set; }
        //    public string imageBase64 { get; set; }
        //}

        //public class ModelWithChampionsWithStringImage
        //{
        //    public List<ChampionWithStringImage> championsWithStrImg { get; set; }
        //    public List<String> tags { get; set; }
        //    public List<Model.PatchVersion> patches { get; set; }
        //}
    }
}
