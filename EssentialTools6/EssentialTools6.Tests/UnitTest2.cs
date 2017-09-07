using System;
using System.CodeDom;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EssentialTools6.Models;
using  System.Linq;
using System.Security.Cryptography.X509Certificates;
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
            //create a mock IDiscountHelper implementation
            Mock<IDiscountHelper> mock= new Mock<IDiscountHelper>();
            //specify the way that MCOK behaves,Setup method to add a method to my mock object.
            //When I call the Setup method, Moq passes me the interface that I have asked it to implement
            //d ApplyDiscount method,  only method in the IDiscountHelper interface, and the method I need to test the
            //LinqValueCalculator class.
            //The It class defines a number of methods that are used with generic type parameters
            mock.Setup( m =>m.ApplyDiscount(It.IsAny<decimal>())).Returns<decimal>(total =>total);
                     //lambda expression, Moq passes me a value of the type I receive in the ApplyDiscount
                     //I create a pass-through method , in which I return the value that is passed to the mock
                     //ApplyDiscount method without performing any operations on it.


             var target=new LinqValueCalculator(mock.Object);

                          // Object property returns an implementation of the IDiscountHelper interface
                          //  where the ApplyDiscount method returns the value of the decimal parameter it is passed.
                  // action
           var result = target.ValueProducts(products);

            Assert.AreEqual(products.Sum(e=>e.Price), result);


            //---------------------- Complex Example----------------------------------
         


        }

        private Product[] createProduct(decimal value)
        {
            return new[] { new Product { Price = value } };
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void Pass_through_Variable_Discount()
        {
            // arrange
            Mock< IDiscountHelper> mock= new Mock<IDiscountHelper>();
            mock.Setup(m => m.ApplyDiscount(It.IsAny<decimal>())).Returns<decimal>(total => total);
            mock.Setup(m => m.ApplyDiscount(It.Is<decimal>(v => v==0))).Throws<System.ArgumentOutOfRangeException>();
            mock.Setup(m => m.ApplyDiscount(It.Is<decimal>(v => v>100))).Returns<decimal>(total => (total*0.9M));
            mock.Setup(m => m.ApplyDiscount(It.IsInRange<decimal>(10,100, Range.Inclusive))).Returns<decimal>(total => (total - 5));

            var target = new LinqValueCalculator(mock.Object);

            // act
            decimal FiveDollarDiscount = target.ValueProducts(createProduct(5));
            decimal TenDollarDiscount = target.ValueProducts(createProduct(10));
            decimal FiftyDollarDiscount = target.ValueProducts(createProduct(50));
            decimal HundredDollarDiscount = target.ValueProducts(createProduct(100));
            decimal FiveHundredDollarDiscount = target.ValueProducts(createProduct(500));

            // assert
            Assert.AreEqual(5, FiveDollarDiscount, "$5 Fail");
            Assert.AreEqual(5, TenDollarDiscount, "$10 Fail");
            Assert.AreEqual(45, FiftyDollarDiscount, "$50 Fail");
            Assert.AreEqual(95, HundredDollarDiscount, "$100 Fail");
            Assert.AreEqual(450, FiveHundredDollarDiscount, "$500 Fail");
            target.ValueProducts(createProduct(0));
        }

    }
}
