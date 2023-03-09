using MongoDB.Driver;
using Catalog.API.Entities;
using System.Collections.Generic;

namespace Catalog.API.Data
{
    public class CatalogContextSeed
    {
        public static void SeedData(IMongoCollection<Product> productCollection)
        {
            bool existingProduct = productCollection.Find(p => true).Any();

            if (!existingProduct)
            {
                productCollection.InsertManyAsync(preConfiguredProducts());
            }
        }

        private static IEnumerable<Product> preConfiguredProducts()
        {
            return new List<Product>()
                {
                    new Product()
                    {
                        Name = "IPhone",
                        Category = "SmartPhone",
                        Description = "Apple product",
                        ImageFile = "iphone.png"
                    },
                    new Product()
                    {
                        Name = "S20Plus",
                        Category = "SmartPhone",
                        Description = "Samsung product",
                        ImageFile = "s20plus.png"
                    },
                    new Product()
                    {
                        Name = "7aPro",
                        Category = "SmartPhone",
                        Description = "Google product",
                        ImageFile = "7apro.png"
                    },
                };
        }

    }
}
