using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFastFoodRestaurantApp.Domain;

namespace WebFastFoodRestaurantApp.Abstraction
{
    public interface ITypeFoodService
    {
        List<TypeFood> GetTypeFoods();
        TypeFood GetTypeFoodById(int TypeFoodId);
        List<Product> GetProductsByTypeFood(int TypeFoodId);
    }
}
