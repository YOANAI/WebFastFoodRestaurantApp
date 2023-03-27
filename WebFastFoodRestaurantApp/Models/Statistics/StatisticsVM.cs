using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebFastFoodRestaurantApp.Models.Statistics
{
    public class StatisticsVM
    {
        [Display(Name = "Count Clients")]
        public int CountClients { get; set; }
        [Display(Name = "Count Products")]

        public int CountProducts { get; set; }
        [Display(Name = "Count Orders")]

        public int CountOrders { get; set; }
        [Display(Name = "Total Sum Orders")]

        public decimal SumOrders { get; set; }
    }
}