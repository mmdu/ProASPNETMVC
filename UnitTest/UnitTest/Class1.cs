using System;
using System.Collections.Generic;
using System.Linq;
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
            
        }

        public void Withdraw(int amount)
        {
            
        }

        [TestFixture]
        public class BankAcountTests
        {
            [Test]
            public void BankAcountShouldIncreaseOnPositiveDeposit()
            {
                // Assert.That(2+2, Is.EqualTo(6));
                Warn.If(2+2 !=5);
                Warn.If(2+2, Is.Not.EqualTo(5));
                Warn.If(()=>2+2, Is.Not.EqualTo(5).After(5000));

                Warn.Unless(2 + 2 == 5);
                Warn.Unless(2 + 2, Is.EqualTo(5));
                Warn.Unless(() => 2 + 2, Is.EqualTo(5).After(5000));
            }

        }

}
}
