using BlogCore_ASPNetMVC_Net8.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore_ASPNetMVC_Net8.Data.Repository
{
    public class WorkContainer : IWorkContainer
    {
        private readonly ApplicationDbContext _context;

        public WorkContainer(ApplicationDbContext context)
        {
            _context = context;
            CategoryRepository = new CategoryRepository(_context);
            ArticleRepository = new ArticleRepository(_context);
            SliderRepository = new SliderRepository(_context);
        }

        public ICategoryRepository CategoryRepository { get; private set; }

        public IArticleRepository ArticleRepository { get; private set; }

        public ISliderRepository SliderRepository { get; private set; }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
