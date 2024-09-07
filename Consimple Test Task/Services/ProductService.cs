using Consimple_Test_Task.Models;
using Consimple_Test_Task.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Consimple_Test_Task.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Product> CreateProductAsync(Product model)
        {
            var existingProduct = await _dbContext.Products.FirstOrDefaultAsync(p => p.Name == model.Name);
            if (existingProduct != null)
                return existingProduct;

            var newProduct = new Product
            {
                Name = model.Name,
                Price = model.Price,
                Category = model.Category,
            };

            await _dbContext.Products.AddAsync(newProduct);
            await _dbContext.SaveChangesAsync();
            return newProduct;
        }

        public async Task DeleteProductAsync(Guid id)
        {
            var product = await GetProductByIdAsync(id);
            if (product == null) return;

            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Product?> GetProductByIdAsync(Guid productId)
        {
            return await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == productId);
        }

        public async Task<Product?> UpdateProductAsync(Product model)
        {
            if (model == null)
                return null;

            var existingProduct = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == model.Id);
            if (existingProduct == null)
                return null;

            existingProduct.Name = model.Name;
            existingProduct.Price = model.Price;
            existingProduct.Category = model.Category;

            await _dbContext.SaveChangesAsync();
            return existingProduct;
        }
    }
}
