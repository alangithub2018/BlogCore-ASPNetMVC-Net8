using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore_ASPNetMVC_Net8.Models
{
    public class Slider
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required]
        public bool State { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Image")]
        public string URLImage { get; set; }
    }
}
