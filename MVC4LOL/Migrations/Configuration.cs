namespace MVC4LOL.Migrations
{
    using MVC4LOL.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Security;

    internal sealed class Configuration : /*DbMigrationsConfiguration<MVC4LOL.Models.MVC4LOLContext>, */ DbMigrationsConfiguration<MVC4LOL.Models.UsersContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(MVC4LOL.Models.UsersContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.UserProfiles.AddOrUpdate(u => u.UserName, new UserProfile { UserName = "Lahatron" });

            //if (!Roles.RoleExists("Admin")) // ERROR! update-database
            //{
            //    Roles.CreateRole("Admin");
            //}

            //if (Membership.GetUser("Lahotron") == null)
            //{
            //    Membership.CreateUser("Lahotron", "mientka rurka");
            //}
        }

        
    }
}
