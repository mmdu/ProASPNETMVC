using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeStore.Domain.Entities;

namespace SportsStore.Domain.Concrete
{
    class EFDbContext: DbContext  //This class then automatically defines a property for each table in
    {
        public DbSet<Product> Products { get; set; }
    }

    //The name (Products) of the property specifies the table, and the type parameter (Product) of the DbSet result specifies the model type
    // that the Entity Framework should use to represent rows in that table , meaning that the Entity Framework should use the Product model type to represent
   // rows in the Products table
}
