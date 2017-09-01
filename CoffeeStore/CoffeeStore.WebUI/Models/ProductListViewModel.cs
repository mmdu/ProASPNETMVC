using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.Core.Smtp;
using CoffeeStore.Domain.Entities;
namespace CoffeeStore.WebUI.Models
{
    public class ProductListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}