﻿using MVC4LOL.Models.ViewModels;
using MVC4LOL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC4LOL.Controllers
{
    public class ChampionBuilderController : Controller
    {
        private MVC4LOLDb db = new MVC4LOLDb();

        public ActionResult Builder(Int32 championId)
        {
            var model = new ChampionBuilderViewModel();
            model.Champion = db.ChampionData.First(o => o.ChampionId == championId);
            model.Items = db.Items.Where(o => o.Availability.Contains("Common") || o.Availability.Contains("Summoner")).ToList();
            return View("Builder", model);
        }

        public ActionResult Load(Int32 championId)
        {
            var model = new ChampionBuilderViewModel();

            model.Champion = db.ChampionData.First(o => o.ChampionId == championId);
            model.Items = db.Items.Where(o => o.Availability.Contains("Common") || o.Availability.Contains("Summoner")).ToList();

            var jsonResult = Json(ChangeByteArrayToStringForModel(model), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public Object ChangeByteArrayToStringForModel(ChampionBuilderViewModel model)
        {

            List<ChampionBuilderViewModel> ml = new List<ChampionBuilderViewModel>();
            ml.Add(model);

            var res = ml.Select(o => new
            {
                Items = o.Items.Select(i => new {
                    Image = i.Image != null ? Convert.ToBase64String(i.Image) : String.Empty,
                    Health = i.Health,
                    HealthRegen = i.HealthRegen,
                    Id = i.Id,
                    Armor = i.Armor,
                    AttackSpeed = i.AttackSpeed,
                    Cost = i.Cost,
                    Damage = i.Damage,
                    Description = i.Description,
                    MagicResist = i.MagicResist,
                    Mana = i.Mana,
                    ManaRegen = i.ManaRegen,
                    MoveSpeed = i.MoveSpeed,
                    Name = i.Name
                }),
                Champion =  new
                {
                    Image = o.Champion.Image != null ? Convert.ToBase64String(o.Champion.Image) : String.Empty,
                    Armor = o.Champion.Armor,
                    ArmorPerLvl = o.Champion.ArmorPerLvl,
                    AtkSpeed = o.Champion.AtkSpeed,
                    AtkSpeedPerLvl = o.Champion.AtkSpeedPerLvl,
                    Champion = o.Champion.Champion,
                    ChampionId = o.Champion.ChampionId,
                    Damage = o.Champion.Damage,
                    DamagePerLvl = o.Champion.DamagePerLvl,
                    Health = o.Champion.Health,
                    HealthPerLvl = o.Champion.HealthPerLvl,
                    HealthRegen = o.Champion.HealthRegen,
                    HealthRegenPerLvl = o.Champion.HealthRegenPerLvl,
                    Id = o.Champion.Id,
                    IP_Cost = o.Champion.IP_Cost,
                    MagicResist = o.Champion.MagicResist,
                    MagicResistPerLvl = o.Champion.MagicResistPerLvl,
                    Mana = o.Champion.Mana,
                    ManaPerLvl = o.Champion.ManaPerLvl,
                    ManaRegen = o.Champion.ManaRegen,
                    ManaRegenPerLvl = o.Champion.ManaRegenPerLvl,
                    MoveSpeed = o.Champion.MoveSpeed,
                    Name = o.Champion.Name,
                    PatchVersion = o.Champion.PatchVersion,
                    PatchVersionId = o.Champion.PatchVersionId,
                    Range = o.Champion.Range,
                    ReleaseDate = o.Champion.ReleaseDate,
                    RP_Cost = o.Champion.RP_Cost,
                    Title = o.Champion.Title,
            }});

            return res.First();
        }

    }
}
