using Microsoft.AspNetCore.Mvc.Rendering;
using Restaurant_ordering_system.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant_ordering_system.Models
{
    public class UsersVM
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }


    }



    public class UsersListVM
    {
        public IList<UsersVM> Managers { get; set; }

        public IList<UsersVM> Customers { get; set; }

        
    }

    public class RegistrationVM
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Name { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Street Address")]
        public string StreetAddress { get; set; }

        public string City { get; set; }

        public string County { get; set; }

        [Display(Name = "Post Code")]
        public string PostCode { get; set; }

        public IEnumerable<SelectListItem> RoleTypes { get; set; }

        public string Role { get; set; }
    }
}
