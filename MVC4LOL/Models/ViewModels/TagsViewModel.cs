using MVC4LOL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC4LOL.Models.ViewModels
{
    public class TagsViewModel
    {
        public List<Tag> Tags { get; set; }
        public Champion Champion { get; set; }
    }
}