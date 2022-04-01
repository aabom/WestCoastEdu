using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WestCoastEdu.DataAccess.Repository;
using WestCoastEdu.Models;
using WestCoastEdu.Utility;

namespace WestCoastEdu.DataAccess.DbInitializer
{


    public class DbInitializer : IDbInitializer
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;

        public DbInitializer(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext db)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
        }
        public void Initialize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception ex)
            {

            }

            if (_db.Location.ToList().Count == 0)
            {
                _db.Location.Add(new Location
                {
                    Name = "Classroom"
                });
                _db.SaveChanges();
            }

            if (_db.Status.ToList().Count == 0)
            {
                _db.Status.Add(new Status
                {
                    Name = "Active"
                });
                _db.SaveChanges();
            }

            if (_db.Products.ToList().Count == 0)
            {
                _db.Products.Add(new Product
                {
                    Title = ".Net-developer",
                    Description = "Become a back end expert",
                    Length = "4 Terms",
                    Level = "Beginner",
                    StartDate = new DateTime(2022, 08, 22),
                    Price = 999,
                    ImageUrl = "https://images.unsplash.com/photo-1468436139062-f60a71c5c892?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1170&q=80",
                    LocationId = 1,
                    StatusId = 1,
                });
                _db.SaveChanges();
            }

            if (!_roleManager.RoleExistsAsync(SD.Role_Admin).GetAwaiter().GetResult() || _db.ApplicationUsers.ToList().Count == 0)
            {
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Employee)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_User_Individual)).GetAwaiter().GetResult();

                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "admin@admin.com",
                    Email = "admin@admin.com",
                    FirstName = "admin",
                    LastName = "admin",
                    PhoneNumber = "1112223344",
                    StreetAddress = "admin lane 1",
                    PostalCode = "55555",
                    City = "admin City"
                }, "Admin123!").GetAwaiter().GetResult();

                ApplicationUser user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "admin@admin.com");
                _userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();
            }


            return;
        }
    }
}
