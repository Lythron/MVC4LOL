using MVC4LOL.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVC4LOL.Model
{
    [Table("Champion")]
    public class Champion
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public Int32 Id { get; set; }
        public String Name { get; set; }
    }

    [Table("Tag")]
    public class Tag
    {
        public Int32 Id { get; set; }
        public String Name { get; set; }

        public Int32 UserId { get; set; }
        public virtual UserProfile UserProfile { get; set; }
        public Int32 ChampionId { get; set; }
        public virtual Champion Champion { get; set; }
    }

    [Table("Skill")]
    public class Skill
    {
        public Int32 Id { get; set; }
        public String Name { get; set; }
        public Int32 Lvl { get; set; }
        public String Description { get; set; }

        public Int32 ChampionId { get; set; }
        public virtual Champion Champion { get; set; }
    }

    // TODO : moved to AccountModels
    //public class MVC4LOLContext : DbContext
    //{
    //    public MVC4LOLContext()
    //        : base("DefaultConnection")
    //    {
    //    }

    //    DbSet<Champion> Champions { get; set; }
    //}

}