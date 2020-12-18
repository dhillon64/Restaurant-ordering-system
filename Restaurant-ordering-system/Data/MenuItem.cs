using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant_ordering_system.Data
{
    public class MenuItem
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Spicyness { get; set; }
        public enum ESpicy { NA=0,NotSpicy=1,Spicy=2, VerySpicy=3}

        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]

        public virtual Category Category { get; set; }

        public double Price { get; set; }

        public string Image { get; set; }


    }
}
