using MVC4LOL.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC4LOL.Repository
{
    public class UsersContext : DbContext
    {
        public UsersContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<ChampionData> ChampionData { get; set; }
        public DbSet<Champion> Champions { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<PatchVersion> PatchVersions { get; set; }

        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChampionData>().Property(o => o.AtkSpeed).HasPrecision(8, 3);

            base.OnModelCreating(modelBuilder);
        }
    }
}
