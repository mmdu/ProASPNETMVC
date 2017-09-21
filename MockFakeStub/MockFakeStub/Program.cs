using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Security.Policy;
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
            public   ILog log;

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
            public static T Instance => new Null<T>().ActLike<T>();

            #region Overrides of DynamicObject
            // retrun default value of methode 
            public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
            {
                result = Activator.CreateInstance(typeof(T).GetMethod(binder.Name).ReturnType);
                return true;
            }

            #endregion
        }


        // stubs, with results , fake object,
        public class NullLogWithResult: ILog
        {
            private bool expectedResult;

            public NullLogWithResult(bool expectedResult)
            {
                this.expectedResult = expectedResult;
            }

            #region Implementation of ILog

            public bool write(string msg)
            {
                return expectedResult;
            }

            #endregion
        }

        public class LogMock : ILog
        {
            private bool expectedResult;
            public Dictionary<string, int> MethodCallCount;

            public LogMock(bool expectedResult)
            {
                this.expectedResult = expectedResult;
                MethodCallCount=new Dictionary<string, int>();
            }

            #region Implementation of ILog

            public bool write(string msg)
            {
                AddorIncrement(nameof(write));
                return expectedResult;

            }

            #endregion

            private void AddorIncrement(string methodName)
            {
                if (MethodCallCount.ContainsKey(methodName))
                {
                    MethodCallCount[methodName]++;
                }
                else
                {
                    MethodCallCount.Add(methodName,1);
                }
            }
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

            //[Test]
            //public void DepositionUnitTestWithDynamicFake()
            //{
            //    var log = Null<ILog>.Instance;
            //    ba = new BankAccount(log) {Balance = 100};
            //    ba.Deposit(100);
            //    Assert.That(ba.Balance, Is.EqualTo(200));

            //}

            [Test]
            public void DepositionUnitTestWithStub()
            {
                var log = new NullLogWithResult(true);
                ba = new BankAccount(log) { Balance = 100 };
                ba.Deposit(100);
                Assert.That(ba.Balance, Is.EqualTo(200));

            }

            [Test]
            public void DepositionWithMock()
            {
                var log=new LogMock(true);
                var ba=new BankAccount(log) {Balance = 100};
                ba.Deposit(100);
                Assert.Multiple(() =>
                {
                    Assert.That(ba.Balance, Is.EqualTo(200));
                    Assert.That(log.MethodCallCount[nameof(LogMock.write)], Is.EqualTo(1));

                });
            }

        }
    }
}
