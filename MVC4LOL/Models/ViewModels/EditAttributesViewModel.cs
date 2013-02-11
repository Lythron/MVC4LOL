using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC4LOL.Models
{
    public class EditAttributesViewModel
    {
        public Int32 ChampionId { get; set; }
        public Int32 UserId { get; set; }
        
        [Required]
        public String Name { get; set; }
    }
}