using Consimple_Test_Task.Models;
using System;
using System.Threading.Tasks;

namespace Consimple_Test_Task.Services.Interfaces
{
    public interface IProductService
    {
        Task<Product> CreateProductAsync(Product model);
        Task DeleteProductAsync(Guid id);
        Task<Product?> UpdateProductAsync(Product model);
        Task<Product?> GetProductByIdAsync(Guid productId);
    }
}
