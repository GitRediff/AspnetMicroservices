using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _context;

        public ProductRepository(ICatalogContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context
                .Products
                .Find(p => true)
                .ToListAsync();
        }

        public async Task<Product> GetProduct(string id)
        {
            return await _context
                .Products
                .Find(p => p.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            return
                await _context
                .Products
                .Find(p => p.Name == name)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string category)
        {
            return
                await _context
                .Products
                .Find(p => p.Category == category)
                .ToListAsync();
        }

        public async Task CreateProduct(Product product)
        {
            await _context
            .Products
            .InsertOneAsync(product);

        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var updateResult = await _context
                                    .Products
                                    .ReplaceOneAsync(filter: p => p.Id == product.Id, replacement: product);
            return
                updateResult.IsAcknowledged
                && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> DeleteProduct(string name)
        {
            DeleteResult deleteResult = await _context
                .Products
                .DeleteOneAsync(p => p.Name == name);

            return
                deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }
    }
}