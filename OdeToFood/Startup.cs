using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using OdeToFood.Models;
using Owin;

[assembly: OwinStartupAttribute(typeof(OdeToFood.Startup))]
namespace OdeToFood {

    public partial class Startup {

        public void Configuration(IAppBuilder app) {

            ConfigureAuth(app);

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

        }

    }

}

