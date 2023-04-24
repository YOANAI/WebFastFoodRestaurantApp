using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFastFoodRestaurantApp.Abstraction;
using WebFastFoodRestaurantApp.Domain;
using WebFastFoodRestaurantApp.Models.TypeFood;
using WebFastFoodRestaurantApp.Models.Category;
using WebFastFoodRestaurantApp.Models.Product;
using System.Diagnostics;
using System.Drawing;
using System.Reflection.Metadata;

namespace WebFastFoodRestaurantApp.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly ITypeFoodService _TypeFoodService;

        public ProductController(IProductService productService, ICategoryService categoryService, ITypeFoodService TypeFoodService)
        {
            this._productService = productService;
            this._categoryService = categoryService;
            this._TypeFoodService = TypeFoodService;

        }
        //GET: ProductController/Create
        public ActionResult Create()
        {
            var product = new ProductCreateVM();
            product.TypeFoods = _TypeFoodService.GetTypeFoods()
                .Select(x => new TypeFoodPairVM()
                {
                    Id = x.Id,
                    Name = x.TypeFoodName
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
                var createId = _productService.Create(product.ProductName, product.Description, product.TypeFoodId, product.CategoryId, product.Picture, product.Quantity, product.Price, product.Discount);
                if (createId)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View();
        }

        // GET : ProductController
        [AllowAnonymous]
        public ActionResult Index(string searchStringCategoryName, string searchStringTypeFoodName)
        {
            List<ProductIndexVM> products = _productService.GetProducts(searchStringCategoryName, searchStringTypeFoodName)
                .Select(product => new ProductIndexVM
                {
                    Id = product.Id,
                    ProductName = product.ProductName,
                    Description = product.Description,
                    TypeFoodId = product.TypeFoodId,
                    TypeFoodName = product.TypeFood.TypeFoodName,
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
            if (product == null)
            {
                return NotFound();
            }

            ProductEditVM updatedProduct = new ProductEditVM()
            {
                Id = product.Id,
                ProductName = product.ProductName,
                Description = product.Description,
                TypeFoodId = product.TypeFoodId,
                //TypeFoodName = product.TypeFood.TypeFoodName,
                CategoryId = product.CategoryId,
                //CategoryName = product.Category.CategoryName,
                Picture = product.Picture,
                Quantity = product.Quantity,
                Price = product.Price,
                Discount = product.Discount
            };
            updatedProduct.TypeFoods = _TypeFoodService.GetTypeFoods()
                .Select(b => new TypeFoodPairVM()
                {
                    Id = b.Id,
                    Name = b.TypeFoodName
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
            var errors = ModelState
            .Where(x => x.Value.Errors.Count > 0)
                .Select(x => new { x.Key, x.Value.Errors })
                .ToArray();

            if (ModelState.IsValid)
            {
                var updated = _productService.Update(id, product.ProductName, product.Description, product.TypeFoodId, product.CategoryId, product.Picture, product.Quantity, product.Price, product.Discount);
                if (updated)
                {
                    return this.RedirectToAction("Index");
                }
            }
            return View(product);
        }

        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            Product item = _productService.GetProductById(id);
            if (item == null)
            {
                return NotFound();
            }
            ProductDetailsVM product = new ProductDetailsVM()
            {
                Id = item.Id,
                ProductName = item.ProductName,
                Description = item.Description,
                TypeFoodId = item.TypeFoodId,
                TypeFoodName = item.TypeFood.TypeFoodName,
                CategoryId = item.CategoryId,
                CategoryName = item.Category.CategoryName,
                Picture = item.Picture,
                Quantity = item.Quantity,
                Price = item.Price,
                Discount = item.Discount

            };
            return View(product);
        }
        public ActionResult Delete(int id)
        {
            Product item = _productService.GetProductById(id);
            if (item == null)
            {
                return NotFound();
            }
            ProductDeleteVM product = new ProductDeleteVM()
            {
                Id = item.Id,
                ProductName = item.ProductName,
                //Description = item.Description,
                TypeFoodId = item.TypeFoodId,
                //TypeFoodName = item.TypeFood.TypeFoodName,
                CategoryId = item.CategoryId,
                //CategoryName = item.Category.CategoryName,
                Picture = item.Picture,
                Quantity = item.Quantity,
                Price = item.Price,
                Discount = item.Discount

            };
            return View(product);
        }
        // POST : ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Delete(int id, IFormCollection collection)
        {
            var deleted = _productService.RemoveById(id);
            if (deleted)
            {
                return this.RedirectToAction("Success");
            }
            else
            {
                return View();
            }
        }
        public IActionResult Success()
        {
            return View();
        }
    }
}

    

