using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit;
using NUnit.Framework;
using ImpromptuInterface;
namespace MockFakeStub
{
    class Program
    {
        public interface ILog
        {
            bool write(string msg);
        }

        public class  Consolelog :ILog
        {
            public bool write(string msg)
            {
                Console.WriteLine(msg);
                return true;
            }
        }

        public class  BankAccount
        {
            public int Balance { get; set; }
            private readonly ILog log;

            public BankAccount(ILog log)
            {
                this.log = log;
            }

            public void Deposit(int amount)
            {
                if(log.write($"Depositing{amount}"))
                {
                    Balance += amount;
                }
            }
        }
        static void Main(string[] args)
        {
        }

        //Fake Object. DO Nothing. null log to feed up  requirement of BankAccount,  , remove the dependency 
        public class NullLog : ILog
        {
            public bool write(string msg)
            {
                return true;

            }
        }

        // dynamic object, fake object required  

        public class Null<T>:  DynamicObject where T : class 
        {
            // current object, the fake T. T is interface here 
            public static T instance => new Null<T>().ActLike<T>();

            #region Overrides of DynamicObject
            // retrun default value of methode 
            public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
            {
                result = Activator.CreateInstance(typeof(T).GetMethod(binder.Name).ReturnType);
                return true;
            }

            #endregion
        }
        [TestFixture]
        public class  BankAccountTest
        {
            private BankAccount ba;
            [Test]
            public void DepositIntegrationTest()
            {
                ba =new BankAccount(new Consolelog() ){ Balance = 100 };
                ba.Deposit(100);
                Assert.That(ba.Balance, Is.EqualTo(200));
            }

            [Test]
            // static fake object , null log object, 
            public void DepositionUnitTest()
            {
                ba= new BankAccount(new NullLog()) {Balance = 100};
                ba.Deposit(100);
                Assert.That(ba.Balance,Is.EqualTo(200));
            }

            //ImpromptuInterface , multi dynamic  fake object

            [Test]
            public void DepositionUnitTestWithDynamicFake()
            {
                var log = Null<ILog>.instance;
                ba = new BankAccount(log) {Balance = 100};
                ba.Deposit(100);
                Assert.That(ba.Balance, Is.EqualTo(200));

            }
        }
    }
}
