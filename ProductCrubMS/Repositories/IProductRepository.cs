using ProductCrubMS.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductCrubMS.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProduct(int id);
        Task AddProduct(Product product);
        Task UpdateProduct(Product product);
        Task DeleteProduct(int id);
    }
}
