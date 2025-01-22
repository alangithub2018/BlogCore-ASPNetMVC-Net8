using BlogCore_ASPNetMVC_Net8.Data.Repository.IRepository;
using BlogCore_ASPNetMVC_Net8.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore_ASPNetMVC_Net8.Data.Repository
{
    public class SliderRepository : Repository<Slider>, ISliderRepository
    {
        private readonly ApplicationDbContext _context;
        public SliderRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public void Update(Slider slider)
        {
            var objectFromDB = _context.Slider.FirstOrDefault(s => s.Id == slider.Id);
            objectFromDB.Name = slider.Name;
            objectFromDB.State = slider.State;
            objectFromDB.URLImage = slider.URLImage;
        }
    }
}
