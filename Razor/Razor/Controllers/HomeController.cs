using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Razor.Models;

namespace Razor.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View(myProduct);
        }

        private Product myProduct = new Product
        {
            ProductID = 1,
            Name = "Banana",
            Description = "fruit imported",
            Category = "Fruit",
            Price = 11
        };

        public ActionResult NameandPrice()
        {
            return View(myProduct);
        }

        public ActionResult DemoExpression()
        {
            ViewBag.ProductCount = 1;
            ViewBag.ExpressShip = true;
            ViewBag.ApplyDiscount = false;
            ViewBag.Supplier = null;
            return View(myProduct);
        }

        public ActionResult DemoArray()
        {    Product[] array ={        
                new Product { Name = "weww", Price = 2323 },           
               new Product { Name = "2weww", Price = 3323 },            
               new Product { Name = "3weww", Price = 4323 },            
               new Product { Name = "4weww", Price = 5323 },            
               new Product { Name = "5weww", Price = 6323 },            
               new Product { Name = "7weww", Price = 7323 }
             };
            return View(array);
        }
    }
}