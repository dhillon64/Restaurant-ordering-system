using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant_ordering_system.Data
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            Count = 1;
        }

        [Key]
        public int Id { get; set; }

        public String ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        public int MenuItemId { get; set; }
        [ForeignKey("MenuItemId")]
        public virtual MenuItem MenuItem { get; set; }

        [Range(1,int.MaxValue, ErrorMessage ="Please enter a value greater that or equal to {1}")]
        public int Count { get; set; }

    }
}
