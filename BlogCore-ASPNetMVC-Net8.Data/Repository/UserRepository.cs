using BlogCore_ASPNetMVC_Net8.Data.Repository.IRepository;
using BlogCore_ASPNetMVC_Net8.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore_ASPNetMVC_Net8.Data.Repository
{
    public class UserRepository : Repository<ApplicationUser>, IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public void ActivateUser(string userId)
        {
            var objFromDb = _context.ApplicationUser.FirstOrDefault(u => u.Id == userId);
            objFromDb.LockoutEnd = DateTime.Now;
            _context.SaveChanges();
        }
        public void DeactivateUser(string userId)
        {
            var objFromDb = _context.ApplicationUser.FirstOrDefault(u => u.Id == userId);
            objFromDb.LockoutEnd = DateTime.Now.AddYears(1000);
            _context.SaveChanges();
        }
    }
}
