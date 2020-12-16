using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant_ordering_system.Models
{
    public class CategoryVM
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
