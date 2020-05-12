using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;

using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using MyModels.Entity;
using MyModels.Models;
using Microsoft.AspNet.Identity;

namespace MyModels.DAL
{
    public class DatabaseContextInitializer : System.Data.Entity.NullDatabaseInitializer<DatabaseContext>
    //CreateDatabaseIfNotExists  -  DropCreateDatabaseIfModelChanges  -  DropCreateDatabaseAlways - NullDatabaseInitializer<DatabaseContext>
    {
        public DatabaseContextInitializer()
        {           
        }

        //protected override void Seed(DatabaseContext databaseContext)
        //{
        //    InitializeIdentityForEF(databaseContext);
        //    base.Seed(databaseContext);

        //}

        //Create User=Admin@Admin.com with password=Admin@123456 in the Admin role
        public static void InitializeIdentityForEF(DatabaseContext db)
        {

            if (!db.Roles.Any(r => r.Name == "AppAdmin"))
            {
                var store = new RoleStore(db);
                var manager = new RoleManager<Role,int>(store);

                var role1 = new Role { Name = "AppAdmin" };
                var role2 = new Role { Name = "Admin" };
                var role3 = new Role { Name = "Author" };
                var role4 = new Role { Name = "Editor" };
                var role5 = new Role { Name = "NormalUser" };
                var role6 = new Role { Name = "Contributor" };
                manager.Create(role1);
                manager.Create(role2);
                manager.Create(role3);
                manager.Create(role4);
                manager.Create(role5);
                manager.Create(role6);
            }

            if (!db.Users.Any(u => u.UserName == "FirstAppAdmin" || u.UserName == "FirstAdmin"))
            {
                var store = new UserStore(db);
                var manager = new UserManager<ApplicationUser, int>(store);
                var userAppAdmin = new ApplicationUser { UserName = "FirstAppAdmin", Email = "FirstAppAdmin@info.com", FullName = "کاربر ادمین ارشد"};
                var userAdmin = new ApplicationUser { UserName = "FirstAdmin", Email = "FirstAdmin@info.com", FullName = "کاربر ادمین" };

                manager.Create(userAppAdmin, "!1Admin!");
                manager.Create(userAdmin, "@2Admin@");

                manager.AddToRole(userAppAdmin.Id, "AppAdmin");
                manager.AddToRole(userAdmin.Id, "Admin");
            }



            ////var userManager =
            ////   HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();

            ////var roleManager =
            ////    HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>();

            //const string name = "admin@admin.com";
            ////const string password = "Admin@123456";
            //const string roleName = "Admin";

            ////Create Role Admin if it does not exist
            
            //var role = db.Roles.FirstOrDefault(t => t.Name == roleName);// roleManager.FindByName(roleName);

            //if (role == null)
            //{
            //    role = new Role(roleName);
            //    db.Roles.Add(role);
            //}

            //var user = db.Users.FirstOrDefault(t => t.UserName == name);
            ////var user = userManager.FindByName(name);
            //if (user == null)
            //{
            //    //user = new ApplicationUser { UserName = name, Email = name };
            //    //var result = userManager.Create(user, password);
            //    //result = userManager.SetLockoutEnabled(user.Id, false);
            //    user = new ApplicationUser()
            //    {
            //        UserName = name,
            //        Email = name,
            //        PhoneNumber = name,
            //        PasswordHash = "!Pp" + name,
            //        LockoutEnabled = false
            //    };

            //    db.Users.Add(user);
            //}

            //db.SaveChanges();

            //// Add user admin to Role Admin if not already added
            ////var rolesForUser = userManager.GetRoles(user.Id);
            //var rolesForUser = new UserRole() { UserId = user.Id, RoleId = role.Id};
            //db.TblUserRoles.Add(rolesForUser);
            ////if (!rolesForUser.Contains(role.Name))
            ////{
            ////    var result = userManager.AddToRole(user.Id, role.Name);
            ////}
        }
        
    }
}
