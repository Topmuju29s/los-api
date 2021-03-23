using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ios_api.Models
{

    public class losContext : DbContext
    {
        public losContext(DbContextOptions<losContext> options)
            : base(options)
        { }
        public DbSet<Product> Product { get; set; }
        public DbSet<Stock> Stock { get; set; }
    }

    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new losContext(
                serviceProvider.GetRequiredService<DbContextOptions<losContext>>()))
            {
                if (context.Product.Any() && context.Stock.Any()) return;
                context.Product.AddRange(
                    new Product
                    {
                        id = 1,
                        name = "Milk",
                        price = 100,
                        imageUrl = "www.google.com"
                    },
                    new Product
                    {
                        id = 2,
                        name = "Water",
                        price = 50,
                        imageUrl = "www.google.com"
                    },
                    new Product
                    {
                        id = 3,
                        name = "Beer",
                        price = 200,
                        imageUrl = "www.google.com"
                    },
                    new Product
                    {
                        id = 4,
                        name = "Wine",
                        price = 300,
                        imageUrl = "www.google.com"
                    },
                    new Product
                    {
                        id = 5,
                        name = "Vodka",
                        price = 400,
                        imageUrl = "www.google.com"
                    }
                    );

                context.Stock.AddRange(
               new Stock
               {
                   id = 1,
                   productId = 1,
                   amount = 100,
               },
               new Stock
               {
                   id = 2,
                   productId = 2,
                   amount = 50,
               },
               new Stock
               {
                   id = 3,
                   productId = 3,
                   amount = 200,
               },
               new Stock
               {
                   id = 4,
                   productId = 4,
                   amount = 300,
               },
               new Stock
               {
                   id = 5,
                   productId = 5,
                   amount = 400,
               }
               );
                context.SaveChanges();
            }
        }
    }

    public class Product
    {
        public long id { get; set; }
        public string name { get; set; }
        public string imageUrl { get; set; }
        public decimal? price { get; set; }
    }

    public class Stock
    {
        public long id { get; set; }
        public long productId { get; set; }
        public decimal amount { get; set; }
    }
}
