using Crud.Application.IServices;
using Crud.Domain;
using Crud.Infrastructure.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public long CreateProduct(Product product)
        {
            return _productRepository.CreateProduct(product);
        }

        public long DeleteProduct(long id)
        {
            return (_productRepository.DeleteProduct(id));
        }

        public List<string> GetAllCategoryNames()
        {
            return _productRepository.GetAllCategoryNames();
        }

        public List<string> GetAllProductNames()
        {
            return _productRepository.GetAllProductNames();
        }

        public List<Product> GetAllProducts()
        {
            return _productRepository.GetAllProducts();
        }

        public Product GetProductById(long id)
        {
            return _productRepository.GetProductById(id);
        }

        public List<Product> SearchProducts(string categoryName, string productName)
        {
            return _productRepository.SearchProducts(categoryName, productName);
        }

        public long UpdateProduct(Product product)
        {
            return _productRepository.UpdateProduct(product);
        }
    }
}
