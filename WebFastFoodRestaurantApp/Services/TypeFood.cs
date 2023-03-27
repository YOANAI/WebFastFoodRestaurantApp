using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFastFoodRestaurantApp.Abstraction;
using WebFastFoodRestaurantApp.Domain;
using WebFastFoodRestaurantApp.Data;

namespace WebFastFoodRestaurantApp.Services
{
    public class TypeFoodService:ITypeFoodService
    {
        private readonly ApplicationDbContext _context;
         
        public TypeFoodService(ApplicationDbContext context)
        {
            _context = context;
        }

        public TypeFood GetTypeFoodById(int TypeFoodId)
        {
            return _context.TypeFoods.Find(TypeFoodId);
        }
        public List<TypeFood> GetTypeFoods()
        {
            List<TypeFood> TypeFoods = _context.TypeFoods.ToList();
            return TypeFoods;
        }
        public List<Product> GetProductsByTypeFood(int TypeFoodId)
        {
            return _context.Products
                .Where(x=>x.TypeFoodId== TypeFoodId)
                .ToList();
        }
    }
}

