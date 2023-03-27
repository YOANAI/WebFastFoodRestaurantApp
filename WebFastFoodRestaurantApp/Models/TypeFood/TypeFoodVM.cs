using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebFastFoodRestaurantApp.Models.TypeFood
{
    public class TypeFoodPairVM
    {
        public int Id { get; set; }

        [Display(Name = "TypeFood")]

        public string Name { get; set; }
    }
}
