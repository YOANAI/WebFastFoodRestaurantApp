using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFastFoodRestaurantApp.Domain;

namespace WebFastFoodRestaurantApp.Domain
{
    public class TypeFood
    {
        public int Id { get; set; }
        public string TypeFoodName { get; set; }
        public virtual IEnumerable<Product> Products { get; set; } = new List<Product>();
    }
}
