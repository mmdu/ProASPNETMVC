using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Internal.Commands;

namespace UnitTest
{
    [TestFixture]
    public class BankAcount 
    {
        public int Balance { get; private set ; }

        public BankAcount(int startingBalance)
        {
            Balance = startingBalance;
        }

        public void Deposit(int amount)
        {
            if(amount<0)
                throw new ArgumentException("deposition must be positive", nameof(amount) );
            Balance += amount;
        }

        public void Withdraw(int amount)
        {
            Balance -= amount;
        }

        [TestFixture]
        public class BankAcountTests
        {
            private BankAcount ba;

            [SetUp]
            public void Setup()
            {
                 ba=new BankAcount(100);
            }
            [Test]
            public void BankAcountShouldIncreaseOnPositiveDeposit()
            {
                // Assert.That(2+2, Is.EqualTo(6));
                //    Warn.If(2+2 !=5);
                //    Warn.If(2+2, Is.Not.EqualTo(5));
                // //   Warn.If(()=>2+2, Is.EqualTo(5).After(5000));

                //    Warn.Unless(2 + 2 == 5);
                //    Warn.Unless(2 + 2, Is.Not.EqualTo(5));
                ////    Warn.Unless(() => 2 + 2, Is.EqualTo(5).After(5000));

                ba.Deposit(100);
                Assert.That(ba.Balance,Is.EqualTo(200));
            }

            //[Test]
            //public void Mythode()
            //{
            //    ba.Withdraw(200);
            //    Assert.Multiple(() =>
            //        {
            //            Assert.That(ba.Balance, Is.LessThan(1));
            //            Assert.That(ba.Balance, Is.EqualTo(0));
            //        }
            //    );
            //}
            [Test]
            public void BankAccountShouldThrowPositiveAmount()
            {
                
                var ar = Assert.Throws<ArgumentException>(() => ba.Deposit(-1));
                StringAssert.StartsWith("deposition must be positive", ar.Message);
            }

        }

}
}
