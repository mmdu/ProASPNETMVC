using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoffeeStore.Domain.Entities;
using CoffeeStore.Domain.Abstract;
using CoffeeStore.WebUI.Models;

namespace CoffeeStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product ,Depended on IProductRepository, need Ninject,
        private IProductRepository repository;
        public int PageSize = 4;
        public ProductController(IProductRepository productRepository)
        {
            this.repository = productRepository;
        }
        public ActionResult List(int page =1)
        {
            ProductListViewModel model = new ProductListViewModel
            {
                Products = repository.Products.OrderBy(p => p.ProductID).Skip((page - 1) * PageSize).Take(PageSize),
                PagingInfo =
                    new PagingInfo
                    {
                        CurrrentPage = page,
                        ItemPerPage = PageSize,
                        TotalItems = repository.Products.Count()
                    }
            };

            return View(model);
            //return View(repository.Products.OrderBy(p=>p.ProductID).Skip((page-1)*PageSize).Take(PageSize));
        }
    }
}