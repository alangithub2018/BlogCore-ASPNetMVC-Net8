using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore_ASPNetMVC_Net8.Models.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Slider> Sliders { get; set; }
        public IEnumerable<Article> Articles { get; set; }
        
        // Pagination
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
    }
}
