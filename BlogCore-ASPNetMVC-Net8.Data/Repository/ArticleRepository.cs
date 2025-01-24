using BlogCore_ASPNetMVC_Net8.Data.Repository.IRepository;
using BlogCore_ASPNetMVC_Net8.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore_ASPNetMVC_Net8.Data.Repository
{
    public class ArticleRepository : Repository<Article>, IArticleRepository
    {
        private readonly ApplicationDbContext _context;

        public ArticleRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<Article> AsQueryable()
        {
            return _context.Set<Article>().AsQueryable();
        }

        void IArticleRepository.Update(Article article)
        {
            var objectFromDB = _context.Article.FirstOrDefault(s => s.Id == article.Id);
            objectFromDB.Name = article.Name;
            objectFromDB.Description = article.Description;
            objectFromDB.URLImage = article.URLImage;
            objectFromDB.CategoryId = article.CategoryId;

            //_context.SaveChanges();
        }
    }
}
