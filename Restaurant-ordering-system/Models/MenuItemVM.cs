using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Restaurant_ordering_system.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant_ordering_system.Models
{
    public class MenuItemVM
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Spicyness { get; set; }
        public enum ESpicy { NA = 0, NotSpicy = 1, Spicy = 2, VerySpicy = 3 }

        public string Image { get; set; }

        public int CategoryId { get; set; }

        public virtual CategoryVM Category { get; set; }

        public double Price { get; set; }
    }

    public class AdminViewVM
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public int CategoryId { get; set; }

        public virtual CategoryVM Category { get; set; }

        public double Price { get; set; }
    }

    public class CreateMenuItemVM
    {

        [Required] 
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public string Spicyness { get; set; }
        public enum ESpicy { NA = 0, NotSpicy = 1, Spicy = 2, VerySpicy = 3 }

        public IFormFile Image { get; set; }

        [Display(Name ="Category")]
        public IEnumerable<SelectListItem> Category { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Required]
        public double Price { get; set; }
    }

    public class EditMenuItemVM
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Spicyness { get; set; }
        public enum ESpicy { NA = 0, NotSpicy = 1, Spicy = 2, VerySpicy = 3 }

        public IFormFile Image { get; set; }

        [Display(Name = "Category")]
        public IEnumerable<SelectListItem> Category { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Required]
        public double Price { get; set; }

        public string  CurrentPicture { get; set; }
    }

    public class CustomerMenuVM
    {
        public List<CategoryVM> Categories { get; set; }

        public List<MenuItemVM> Starters { get; set; }

        public List<MenuItemVM> MainCourse { get; set; }

        public List<MenuItemVM> Desserts { get; set; }
    }
}
