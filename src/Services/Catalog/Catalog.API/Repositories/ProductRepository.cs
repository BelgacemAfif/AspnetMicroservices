using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _context;
        public ProductRepository(ICatalogContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Product> GetProduct(string id)
        {
            return await _context
                    .Products
                    .Find(x => x.Id == id)
                    .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context
                        .Products
                        .Find(x => true)
                        .ToListAsync();
        }
        public async  Task CreateProduct(Product product)
        {
           await _context
                .Products
                .InsertOneAsync(product);
        }      

        public async Task<IEnumerable<Product>> GetProductsByCategory(string category)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(x => x.Category, category);
            return await _context
                    .Products
                    .Find(filter)
                    .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByName(string name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(x => x.Name, name);
            return await _context
                    .Products
                    .Find(filter)
                    .ToListAsync();
        }

        public async Task<bool> UpdateProduct(Product product)
        {
           var updateResult = await _context.Products
                 .ReplaceOneAsync(filter: x => x.Id == product.Id, replacement: product);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
             
        }
        public async Task<bool> DeleteProduct(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(x => x.Id, id);

            var updateResult = await _context.Products
                 .DeleteOneAsync(filter);
            return updateResult.IsAcknowledged && updateResult.DeletedCount > 0;
        }

    }
}
