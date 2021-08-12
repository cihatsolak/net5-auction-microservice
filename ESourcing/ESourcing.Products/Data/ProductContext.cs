using ESourcing.Products.Data.Interfaces;
using ESourcing.Products.Entities;
using ESourcing.Products.Settings.ProductDatabase;
using MongoDB.Driver;

namespace ESourcing.Products.Data
{
    public class ProductContext : IProductContext
    {
        #region Ctor
        public ProductContext(IProductDatabaseSettings productDatabaseSettings)
        {
            var mongoClient = new MongoClient(productDatabaseSettings.ConnectionStrings);
            var database = mongoClient.GetDatabase(productDatabaseSettings.DatabaseName);

            Products = database.GetCollection<Product>(productDatabaseSettings.CollectionName);
            ProductContextSeed.SeedData(Products);
        }
        #endregion

        public IMongoCollection<Product> Products { get; }
    }
}
