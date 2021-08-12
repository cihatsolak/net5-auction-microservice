using ESourcing.Products.Data.Interfaces;
using ESourcing.Products.Entities;
using ESourcing.Products.Repositories.Interfaces;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESourcing.Products.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IProductContext _context;

        public ProductRepository(IProductContext context)
        {
            _context = context;
        }

        public async Task InsertAsync(Product product)
        {
            await _context.Products.InsertOneAsync(product);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var filteredProduct = Builders<Product>.Filter.Eq(product => product.Id, id);
            DeleteResult deleteResult = await _context.Products.DeleteOneAsync(filteredProduct);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public async Task<Product> GetProductAsync(string id)
        {
            return await _context.Products.Find(product => product.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByCategoryAsync(string categoryName)
        {
            var filteredProduct = Builders<Product>.Filter.Eq(product => product.Category, categoryName);
            return await _context.Products.Find(filteredProduct).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByNameAsync(string name)
        {
            var filteredProduct = Builders<Product>.Filter.ElemMatch(product => product.Name, name);
            return await _context.Products.Find(filteredProduct).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _context.Products.Find(product => true).ToListAsync();
        }

        public async Task<bool> UpdateAsync(Product product)
        {
            var updateResult = await _context.Products.ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }
    }
}