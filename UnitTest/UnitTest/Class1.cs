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

        public bool Withdraw(int amount)
        {
          //  Balance -= amount;
            if (Balance>=amount)
            {
                Balance -= amount;
                return true;
            }
            return false;
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

        [TestFixture]
        public class  DataDrivenTest
        {

            private BankAcount ba;

            [SetUp]
            public void Setup()
            {
                ba = new BankAcount(100);
            }

            [Test]
            [TestCase(50,true, 50)]
            [TestCase(100,true, 0)]
            [TestCase(1000, false, 100)]
            public void TestMultipleWithdrawalScenarios(int amountToWithdraw, bool shouldSuccess, int expectBalnce)
            {
                var result = ba.Withdraw(amountToWithdraw);
                Warn.If(!result,"Failed for some reason");
                Assert.Multiple(() =>
                {
                    Assert.That(result, Is.EqualTo(shouldSuccess));
                    Assert.That(expectBalnce,Is.EqualTo(ba.Balance));
                });
            }

        }

}
}
