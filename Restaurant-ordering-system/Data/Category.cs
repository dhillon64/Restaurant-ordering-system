using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant_ordering_system.Data
{
    public class Category
    {
        [Key] 
        public int Id { get; set; }
        public string Name { get; set; }


    }
}
