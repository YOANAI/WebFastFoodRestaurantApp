using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFastFoodRestaurantApp.Domain;

namespace WebFastFoodRestaurantApp.Abstraction
{
    public interface IBrandService
    {
        List<Brand> GetBrands();
        Brand GetBrandById(int brandId);
        List<Product> GetProductsByBrand(int brandId);
    }
}
