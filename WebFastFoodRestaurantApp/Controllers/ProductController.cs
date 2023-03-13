using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFastFoodRestaurantApp.Abstraction;
using WebFastFoodRestaurantApp.Domain;
using WebFastFoodRestaurantApp.Models.Brand;
using WebFastFoodRestaurantApp.Models.Category;
using WebFastFoodRestaurantApp.Models.Product;

namespace WebFastFoodRestaurantApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IBrandService _brandService;

        public ProductController(IProductService productService, ICategoryService categoryService, IBrandService brandService)
        {
            this._productService = productService;
            this._categoryService = categoryService;
            this._brandService = brandService;

        }
        //GET: ProductController/Create
        public ActionResult Create()
        {
            var product = new ProductCreateVM();
            product.Brands = _brandService.GetBrands()
                .Select(x => new BrandPairVM()
                {
                    Id = x.Id,
                    Name = x.BrandName
                }).ToList();
            product.Categories = _categoryService.GetCategories()
        .Select(x => new CategoryPairVM()
        {
            Id = x.Id,
            Name = x.CategoryName
        }).ToList();
            return View(product);
        }

        //POST: ProductController
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create([FromForm] ProductCreateVM product)
        {
            if (ModelState.IsValid)
            {
                var createId = _productService.Create(product.ProductName, product.Description, product.BrandId, product.CategoryId, product.Picture, product.Quantity, product.Price, product.Discount);
                if (createId)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View();
        }

        // GET : ProductController
        public ActionResult Index(string searchStringCategoryName, string searchStringBrandName)
        {
            List<ProductIndexVM> products = _productService.GetProducts(searchStringCategoryName, searchStringBrandName)
                .Select(product => new ProductIndexVM
                {
                    Id = product.Id,
                    ProductName = product.ProductName,
                    BrandId = product.BrandId,
                    BrandName = product.Brand.BrandName,
                    CategoryId = product.CategoryId,
                    CategoryName = product.Category.CategoryName,
                    Picture = product.Picture,
                    Quantity = product.Quantity,
                    Price = product.Price,
                    Discount = product.Discount


                }).ToList();
            return this.View(products);
        }
        public ActionResult Edit(int id)
        {
            Product product = _productService.GetProductById(id);
            if(product == null)
            {
                return NotFound();
            }

            ProductEditVM updateProduct = new ProductEditVM()
            {
                Id = product.Id,
                ProductName = product.ProductName,
                BrandId = product.BrandId,
                //BrandName = product.Brand.BrandName,
                CategoryId = product.CategoryId,
                //CategoryName = product.Category.CategoryName,
                Picture = product.Picture,
                Quantity = product.Quantity,
                Price = product.Price,
                Discount = product.Discount
            };
            updatedProduct.Brands = _brandService.GetBrands()
                .Select(b => new BrandPairVM()
                {
                    Id = b.Id,
                    Name = b.BrandName
                })
                .ToList();

            updatedProduct.Categories = _categoryService.GetCategories()
                .Select(c => new CategoryPairVM()
                {
                    Id = c.Id,
                    Name = c.CategoryName
                })
                .ToList();
            return View(updatedProduct);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ProductEditVM product)
        {
            {
                if(ModelState.IsValid)
                {
                    var updated = _productService.Update(id, product.ProductName, product.BrandId, product.CategoryId, product.Picture, product.Quantity, product.Price, product.Discount);
                    if(updated)
                    {
                        return this.RedirectToAction("Index");
                    }
                }
                return View(product);
            }
        }

    }
}
    

