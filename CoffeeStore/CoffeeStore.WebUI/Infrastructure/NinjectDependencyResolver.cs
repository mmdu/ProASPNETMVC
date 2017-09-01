using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using Moq;
using CoffeeStore.Domain.Abstract;
using CoffeeStore.Domain.Entities; 
using SportsStore.Domain.Concrete;

namespace CoffeeStore.WebUI.Infrastructure
{
     
        public class NinjectDependencyResolver : IDependencyResolver
        {
            private IKernel kernel;

            public NinjectDependencyResolver(IKernel kernelParam)
            {
                kernel = kernelParam;
                AddBindings();
            }

            public object GetService(Type serviceType)
            {
                return kernel.TryGet(serviceType);
            }

            public IEnumerable<object> GetServices(Type serviceType)
            {
                return kernel.GetAll(serviceType);
            }

            private void AddBindings()
            {
            //  kernel.Bind<IProductRepository>().To<EFProductRepository>();
            //    Mock<IProductRepository> mock = new Mock<IProductRepository>();
            //    mock.Setup(m => m.Products).Returns(new List<Product> {
            //        new Product {Name = "ODACIO", Price = 1.1M},
            //        new Product {Name = "STORMIO", Price = 1.1m},new Product {Name = "DIAVOLITTO", Price = 0.85M},
            //        new Product {Name = "GIORNIO", Price = 1.4M},new Product {Name = "CARAMELIZIO", Price = 1.5M}
            //    }
            //    );
            //    kernel.Bind<IProductRepository>().ToConstant(mock.Object);
            //
                kernel.Bind<IProductRepository>().To<EFProductRepository>();
            }
        }
    //It tells Ninject to create instances of the EFProductRepository class to service
     // requests for the IProductRepository interface
}