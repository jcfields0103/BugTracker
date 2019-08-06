namespace BugTracker.Migrations
{
    using BugTracker.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BugTracker.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(BugTracker.Models.ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(
                 new RoleStore<IdentityRole>(context));
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }
            if (!context.Roles.Any(r => r.Name == "ProjectManager"))
            {
                roleManager.Create(new IdentityRole { Name = "ProjectManager" });
            }
            if (!context.Roles.Any(r => r.Name == "Developer"))
            {
                roleManager.Create(new IdentityRole { Name = "Developer" });
            }
            if (!context.Roles.Any(r => r.Name == "Submitter"))
            {
                roleManager.Create(new IdentityRole { Name = "Submitter" });
            }
            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));

            if (!context.Users.Any(u => u.Email == "JCFields@mailinator.com"))
                userManager.Create(new ApplicationUser
                {
                    UserName = "JCFields@mailinator.com",
                    Email = "JCFields@mailinator.com",
                    FirstName = "Caleb",
                    LastName = "Fields",
                    DisplayName = "JCFields",
                }, "Abc&123!");

            
            if (!context.Users.Any(u => u.Email == "PFlynn@mailinator.com"))
                userManager.Create(new ApplicationUser
                {
                    UserName = "PFlynn@mailinator.com",
                    Email = "PFlynn@mailinator.com",
                    FirstName = "Paul",
                    LastName = "Flynn",
                    DisplayName = "PJ",
                }, "Abc&123!");
            if (!context.Users.Any(u => u.Email == "CPrice@mailinator.com"))
                userManager.Create(new ApplicationUser
                {
                    UserName = "CPrice@mailinator.com",
                    Email = "CPrice@mailinator.com",
                    FirstName = "Chase",
                    LastName = "Price",
                    DisplayName = "Pryce",
                }, "Abc&123!");
            if (!context.Users.Any(u => u.Email == "CFBooth@mailinator.com"))
                userManager.Create(new ApplicationUser
                {
                    UserName = "CFBooth@mailinator.com",
                    Email = "CFBooth@mailinator.com",
                    FirstName = "Caitlyn",
                    LastName = "Booth",
                    DisplayName = "Booth",
                }, "Abc&123!");

            var userId = userManager.FindByEmail("JCFields@mailinator.com").Id;
            userManager.AddToRole(userId, "Admin");

            userId = userManager.FindByEmail("PFlynn@mailinator.com").Id;
            userManager.AddToRole(userId, "ProjectManager");

            userId = userManager.FindByEmail("CPrice@mailinator.com").Id;
            userManager.AddToRole(userId, "Developer");

            userId = userManager.FindByEmail("CFBooth@mailinator.com").Id;
            userManager.AddToRole(userId, "Submitter");
        }
    }
}
