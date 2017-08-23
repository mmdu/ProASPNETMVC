                                                                                                                                                                                                                                                         using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;                                                                                                                                                                                                                                                        using EssentialTools6.Models;

namespace EssentialTools6.Controllers
{
    public class HomeController : Controller
    {
        private IValueCalculator calc;
        // GET: Home
        public HomeController(IValueCalculator calcParam)   // declaim a dependency here  interface
        {
            calc = calcParam;
        }

        private  Product [] products = {
            new Product { Name = "Kayak", Category = "Watersports", Price = 32333 },
            new Product {Name = "Lifejacket", Category = "Watersports", Price = 233},
            new Product {Name = "Soccer ball", Category = "Soccer", Price = 72},
            new Product {Name = "Corner flag", Category = "Soccer", Price = 43}
        };
        public ActionResult Index()
        {
            // instance of Ninject kernel , no need new to get object,kernel can do it too, 
            //  IKernel ninjectKernel = new StandardKernel();
            // ninjectKernel.Bind<IValueCalculator>().To<LinqValueCalculator>();  

            //IValueCalculator calc = new LinqValueCalculator();
            // LinqValueCalculator calc = new LinqValueCalculator();
            //Ninject use type parameter to create a relationship, 
            // set type parameter using interface,  call TO methode using implemention class  instantiated , 
            // Ivaluecalculator interface should be resolved by creating an instance of the linqValueCalculator class, 
            //----------------------------------------------------------------------------------------------------------          


            //  IValueCalculator calc = ninjectKernel.Get<IValueCalculator>();
            // Get  using interface , results is instance of implementtation type from TO, 

            ShoppingCart cart = new ShoppingCart(calc) {Products = products};
            decimal totalValue = cart.CalculateProductTotal();
            return View(totalValue);
        }
    }
}