using MVC4LOL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC4LOL.Models.ViewModels
{
    public class ChampionBuilderViewModel
    {
        public ChampionData Champion { get; set; }
        public List<Item> Items { get; set; }
    }
}