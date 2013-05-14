using MVC4LOL.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC4LOL.Repository
{
    public interface IMVC4LOLDb : IDisposable
    {
        IQueryable<T> Query<T>() where T : class;
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Remove<T>(T entity) where T : class;
        void SaveChanges();
    }
    public class MVC4LOLDb : DbContext, IMVC4LOLDb
    {
        public MVC4LOLDb()
            : base("DefaultConnection")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<ChampionData> ChampionData { get; set; }
        public DbSet<Champion> Champions { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<PatchVersion> PatchVersions { get; set; }

        public DbSet<Item> Items { get; set; }
        public DbSet<Build> Builds { get; set; }
        public DbSet<BuildItem> BuildItems { get; set; }
        public DbSet<RunePage> RunePages { get; set; }
        public DbSet<Rune> Runes { get; set; }
        public DbSet<RuneType> RuneTypes { get; set; }
        public DbSet<RuneForRunePage> RunesForRunePages { get; set; }

        IQueryable<T> IMVC4LOLDb.Query<T>()
        {
            return Set<T>();
        }

        void IMVC4LOLDb.Add<T>(T entity)
        {
            Set<T>().Add(entity);
        }

        void IMVC4LOLDb.Update<T>(T entity)
        {
            Entry(entity).State = System.Data.EntityState.Modified;
        }

        void IMVC4LOLDb.Remove<T>(T entity)
        {
            Set<T>().Remove(entity);
        }

        void IMVC4LOLDb.SaveChanges()
        {
            SaveChanges();
        }

        // TODO TEST : precision is now set in migration.
        //protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<ChampionData>().Property(o => o.AttackSpeed).HasPrecision(8, 3);

        //    base.OnModelCreating(modelBuilder);
        //}

    }
}
