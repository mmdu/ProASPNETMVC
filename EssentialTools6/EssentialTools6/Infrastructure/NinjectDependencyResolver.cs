using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using  EssentialTools6.Models;
using Ninject;
using Ninject.Web.Common;

namespace EssentialTools6.Infrastructure
{
    // custom dependency resolver , creat a instance of class to service request, use Ninject whenever create an object-including instance of controllers, 
    public class NinjectDependencyResolver : IDependencyResolver  // SYSTEM.MVC framework get object to service request 
    {


        private IKernel Kernel;

        public NinjectDependencyResolver( IKernel kernelParam)
        {
            Kernel = kernelParam;
            AddBinding();
        }

        public object GetService(Type serviceType)
        {
            return Kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return Kernel.GetAll(serviceType);
        }
        private void AddBinding()   // bind and to confgirue relationship of IValueCa and LinqValueCal 
        {
            Kernel.Bind<IValueCalculator>().To<LinqValueCalculator>().InRequestScope();
           // Kernel.Bind<IDiscountHelper>().To<DefaultDiscountHelper>().WithPropertyValue("DiscountSize",20M);
            Kernel.Bind<IDiscountHelper>().To<DefaultDiscountHelper>().WithConstructorArgument("discountParam", 50M);
            Kernel.Bind<IDiscountHelper>().To<FlexibleDiscountHelper>().WhenInjectedInto<LinqValueCalculator>();
        }
    }
}