using Crud.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.Application.IServices
{
    public interface IProductService
    {
        long CreateProduct(Product product);
        long UpdateProduct(Product product);
        long DeleteProduct(long id);
        Product GetProductById(long id);
        List<Product> GetAllProducts();

        // 1
        List<Product> SearchProducts(string categoryName, string productName);
        // 2
        List<string> GetAllCategoryNames();
        // 3
        List<string> GetAllProductNames();
    }
}
