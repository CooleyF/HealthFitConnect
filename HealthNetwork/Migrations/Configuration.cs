namespace HealthNetwork.Migrations
{
    using HealthNetwork.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<HealthNetwork.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(HealthNetwork.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            #region Roles
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            
            if (!context.Roles.Any(r =>r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }
            #endregion



            #region Users
            UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
           
            
            if (!context.Users.Any(r => r.UserName == "Jecool17@gmail.com"))
            {
                ApplicationUser adminUser = new ApplicationUser()
                {
                    DisplayName = "Admin",
                    UserName = "Jecool17@gmail.com",
                    Email = "Jecool17@gmail.com",
                };

                userManager.Create(adminUser, "PassWord");
            }

            if (!context.Users.Any(r => r.UserName == "pyramidofhealth@gmail.com"))
            {
                ApplicationUser adminUser2 = new ApplicationUser()
                {
                    DisplayName = "Admin",
                    UserName = "pyramidofhealth@gmail.com",
                    Email = "pyramidofhealth@gmail.com",
                };

                userManager.Create(adminUser2, "PassWord");
            }

            #endregion


            #region User Id initialization
            ApplicationUser admU = context.Users.FirstOrDefault(r => r.Email == "Jecool17@gmail.com");
            if (admU != null)
            {
                userManager.AddToRole(admU.Id, "Admin");
            }

            ApplicationUser admU2 = context.Users.FirstOrDefault(r => r.Email == "pyramidofhealth@gmail.com");
            if (admU != null)
            {
                userManager.AddToRole(admU2.Id, "Admin");
            }

            #endregion
        }
    }
}
