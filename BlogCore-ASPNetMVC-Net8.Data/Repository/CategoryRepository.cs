using BlogCore_ASPNetMVC_Net8.Data.Repository.IRepository;
using BlogCore_ASPNetMVC_Net8.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore_ASPNetMVC_Net8.Data.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<SelectListItem> GetCategoryList()
        {
            return _context.Category.Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString(),
            });
        }

        void ICategoryRepository.Update(Category category)
        {
            var objectFromDB = _context.Category.FirstOrDefault(s => s.Id == category.Id);
            objectFromDB.Name = category.Name;
            objectFromDB.Order = category.Order;

            //_context.SaveChanges();
        }
    }
}
