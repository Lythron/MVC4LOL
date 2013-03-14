using MVC4LOL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC4LOL.Models.ViewModels
{
    public class ChampionsViewModel
    {
        public List<ChampionData> Champions { get; set; }
        public List<Tag> Tags { get; set; }
        public List<PatchVersion> Patches { get; set; }
    }
}