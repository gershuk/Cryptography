using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Numerics;
using System.Security.Cryptography;
using Cryptography;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {

        static BigInteger[] numbers = new[] { new BigInteger(1) };

        [TestMethod]
        public void TestMethod1()
        {

            BigInteger bigInteger = BigInteger.Parse("1729001512212121275121321413111111743651");
            bigInteger.MillerRabinTest(2);
        }

        [TestMethod]
        public void TestMethod2()
        {
            BigInteger bigInteger = BigInteger.Parse("1729001512212121275121321413111111743651");
            bigInteger.IsPrime();
        }
    }
}
