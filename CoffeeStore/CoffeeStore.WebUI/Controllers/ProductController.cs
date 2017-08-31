using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoffeeStore.Domain.Entities;
using CoffeeStore.Domain.Abstract;

namespace CoffeeStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product ,Depended on IProductRepository, need Ninject,
        private IProductRepository repository;  
        public ProductController(IProductRepository productRepository)
        {
            this.repository = productRepository;
        }
        public ActionResult List()
        {
            return View(repository.Products);
        }
    }
}