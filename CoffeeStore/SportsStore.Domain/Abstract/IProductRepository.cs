using System.Collections.Generic;
using CoffeeStore.Domain.Entities;

namespace CoffeeStore.Domain.Abstract
{
    public interface IProductRepository
    {
        IEnumerable<Product> Products { get; }
    }

}
