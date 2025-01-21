using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore_ASPNetMVC_Net8.Models.ViewModels
{
    public class ArticleVM
    {
        public Article Article { get; set; }

        public IEnumerable<SelectListItem> CategoryList { get; set; }
    }
}
