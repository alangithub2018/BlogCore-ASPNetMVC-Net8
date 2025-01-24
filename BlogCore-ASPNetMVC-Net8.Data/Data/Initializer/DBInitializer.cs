using BlogCore_ASPNetMVC_Net8.Models;
using BlogCore_ASPNetMVC_Net8.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore_ASPNetMVC_Net8.Data.Data.Initializer
{
    public class DBInitializer : IDBInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DBInitializer(ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = applicationDbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initialize()
        {
            try
            {
                // Create the database if it doesn't exist
                if (_context.Database.GetPendingMigrations().Count() > 0)
                {
                    _context.Database.Migrate();
                }
            }
            catch (Exception)
            {
            }

            if (_context.Roles.Any(r => r.Name == Constants.Administrator)) return;

            _roleManager.CreateAsync(new IdentityRole(Constants.Administrator)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(Constants.Registered)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(Constants.Client)).GetAwaiter().GetResult();

            _userManager.CreateAsync(new ApplicationUser
            {
                UserName = "ventas@innovastampfava.com",
                Email = "ventas@innovastampfava.com",
                EmailConfirmed = true,
                Name = "Admin"
            }, "Admin123*").GetAwaiter().GetResult();

            ApplicationUser user = _context.ApplicationUser.Where(u => u.Email == "ventas@innovastampfava.com").FirstOrDefault();
            _userManager.AddToRoleAsync(user, Constants.Administrator).GetAwaiter().GetResult();
        }
    }
}
