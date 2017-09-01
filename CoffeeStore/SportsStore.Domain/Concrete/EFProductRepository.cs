using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeStore.Domain.Entities;
using CoffeeStore.Domain.Abstract;

namespace SportsStore.Domain.Concrete
{
    public class EFProductRepository:IProductRepository
    {
        private EFDbContext context=new EFDbContext();
        public IEnumerable<Product> Products {
            get { return context.Products; } 
        }
    }
}
