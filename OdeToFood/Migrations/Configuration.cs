namespace OdeToFood.Migrations {
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using OdeToFood.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Security;
    using WebMatrix.WebData;

    internal sealed class Configuration : DbMigrationsConfiguration<OdeToFood.Models.OdeToFoodDb> {

        public Configuration() {

            AutomaticMigrationsEnabled = true;
            ContextKey = "OdeToFood.Models.OdeToFoodDb";

        }

        protected override void Seed(OdeToFood.Models.OdeToFoodDb context) {

            context.Restaurants.AddOrUpdate(r => r.Name,
                new Restaurant { Name = "Sabatino's", City = "Baltimore", Country = "USA" },
                new Restaurant { Name = "Great Lake", City = "Chicago", Country = "USA" },
                new Restaurant {
                    Name = "Smaka",
                    City = "Gothenburg",
                    Country = "Sweden",
                    Reviews = new List<RestaurantReview> { new RestaurantReview { Rating = 9, Body = "Great food!", ReviewerName = "Scott" } }
                });

            for (int i = 0; i < 1000; i++) {

                context.Restaurants.AddOrUpdate(r => r.Name, new Restaurant { Name = i.ToString(), City = "Nowhere", Country = "USA" });

            }

            // SeedMemberShipAsync();

        }

        private async void SeedMemberShipAsync() {

            #region Version anterior (¿?)

            //var roles = (SimpleRoleProvider) Roles.Provider;
            //var membership = (SimpleMembershipProvider) Membership.Provider;

            //if (!roles.RoleExists("Admin"))
            //    roles.CreateRole("Admin");

            //if (membership.GetUser("sallen", false) == null)
            //    membership.CreateUserAndAccount("sallen", "imalittleteapot");

            //if (!roles.GetRolesForUser("sallen").Contains("Admin"))
            //    roles.AddUsersToRoles(new[] { "sallen" }, new[] { "admin" });

            #endregion

            #region Usar en Startup.cs
            using (var context = new ApplicationDbContext()) {

                // Se crea el "repositorio" de Roles
                var roleStore = new RoleStore<IdentityRole>(context);

                // Se crea una manager de Rol a partir del "repositorio de roles"
                var roleManager = new RoleManager<IdentityRole>(roleStore);

                // Se crea el "repositorio" de Usuarios
                var userStore = new UserStore<ApplicationUser>(context);

                // Se crea un manager de Usuario a partir del "repositorio de usuario"
                var userManager = new UserManager<ApplicationUser>(userStore);

                if (!roleManager.RoleExists("Admin"))
                    await roleManager.CreateAsync(new IdentityRole("Admin"));

                if (userManager.FindByName("sallen") == null) {

                    var user = new ApplicationUser { Email = "correo@hotmail.com", UserName = "sallen" };

                    await userManager.CreateAsync(user, "iAdministrador7.");

                    await userManager.AddToRoleAsync(user.Id, "Admin");

                }

            } 
            #endregion

        }

    }

}
