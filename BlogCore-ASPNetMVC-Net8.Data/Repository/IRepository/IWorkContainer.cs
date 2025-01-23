using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore_ASPNetMVC_Net8.Data.Repository.IRepository
{
    public interface IWorkContainer : IDisposable
    {
        ICategoryRepository CategoryRepository { get; }

        IArticleRepository ArticleRepository { get; }

        ISliderRepository SliderRepository { get; }

        IUserRepository UserRepository { get; }

        void Save();
    }
}
