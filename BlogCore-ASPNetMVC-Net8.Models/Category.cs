using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore_ASPNetMVC_Net8.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "The Category is required")]
        [Display(Name = "Category Name")]
        public string Name { get; set; }

        [Display(Name = "Visualization order")]
        public int? Order { get; set; }
    }
}
