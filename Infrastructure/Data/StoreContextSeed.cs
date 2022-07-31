using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public  class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.ProductBrands.Any())
                {
                    string brandsData = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/brands.json");

                    List<ProductBrand> brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                    foreach (ProductBrand brand in brands)
                    {
                        context.ProductBrands.Add(brand);
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.ProductTypes.Any())
                {
                    string typesData = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/types.json");

                    List<ProductType> types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                    foreach (ProductType type in types)
                    {
                        context.ProductTypes.Add(type);
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.Products.Any())
                {
                    string productsData = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/products.json");

                    List<Product> products = JsonSerializer.Deserialize<List<Product>>(productsData);

                    foreach (Product product in products)
                    {
                        context.Products.Add(product);
                    }

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                ILogger<StoreContextSeed> logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}