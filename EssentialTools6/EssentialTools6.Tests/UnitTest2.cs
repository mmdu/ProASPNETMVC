using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EssentialTools6.Models;
using  System.Linq;
using Moq;
namespace EssentialTools6.Tests
{
    [TestClass]
    public class UnitTest2
    {

        private Product[] products =
        {
            new Product {Name = "Kayak", Category = "Watersports", Price = 32333},
            new Product {Name = "Lifejacket", Category = "Watersports", Price = 233},
            new Product {Name = "Soccer ball", Category = "Soccer", Price = 72},
            new Product {Name = "Corner flag", Category = "Soccer", Price = 43}
        };
        [TestMethod]
        public void Sum_Products_Correctly()
        //{
        //    var disounter=new MinimumDiscountHelper();
        //   var target = new LinqValueCalculator(disounter);
        //    var goalTotal = products.Sum(e => e.Price);

        {    // Arrange
            Mock<IDiscountHelper> mock= new Mock<IDiscountHelper>();
            mock.Setup( m =>m.ApplyDiscount(It.IsAny<decimal>())).Returns<decimal>(total =>total);
                     //lambda expression, Moq passes me a value of the type I receive in the ApplyDiscount
                     //I create a pass-through method , in which I return the value that is passed to the mock
                     //ApplyDiscount method without performing any operations on it.


             var target=new LinqValueCalculator(mock.Object);

                          //e Object property returns an implementation of the IDiscountHelper interface
                          //  where the ApplyDiscount method returns the value of the decimal parameter it is passed.
                  // action
           var result = target.ValueProducts(products);

            Assert.AreEqual(products.Sum(e=>e.Price), result);

        }
    }
}
