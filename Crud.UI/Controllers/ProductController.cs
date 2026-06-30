using Crud.Application.IServices;
using Crud.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Crud.UI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var products = _productService.GetAllProducts();
            return View(products);
        }

        [HttpGet]
        public IActionResult Create(Product product)
        {
            return View(product);
        }

        [HttpGet]
        public IActionResult Delete(long id)
        {
            _productService.DeleteProduct(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult GetProductById(long id)
        {
            var product = _productService.GetProductById(id);
            return View("Create", product);
        }

        public IActionResult SaveProduct(Product product)
        {
            if (product.ProductId == 0)
            {
                _productService.CreateProduct(product);
            }
            else
            {
                _productService.UpdateProduct(product);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Search(string categoryName, string productName)
        {
            ViewBag.CategoryNames = _productService.GetAllCategoryNames();
            ViewBag.ProductNames = _productService.GetAllProductNames();
            ViewBag.SelectedCategoryName = categoryName;
            ViewBag.SelectedProductName = productName;

            List<Product> results = (string.IsNullOrEmpty(categoryName) && string.IsNullOrEmpty(productName))
                ? new List<Product>()
                : _productService.SearchProducts(categoryName, productName);

            return View(results);
        }
    }
}
