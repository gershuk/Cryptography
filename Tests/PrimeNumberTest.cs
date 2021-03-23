using Cryptography;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Collections.Generic;
using System.Numerics;

namespace Tests
{
    [TestClass]
    public class PrimeNumberTest
    {
        private static List<BigInteger> _numbers;

        [TestInitialize]
        public void Init()
        {
            const int bytesCount = 32;
            const int randCount = 1000;
            const int primCount = 40;

            _numbers = new(randCount + primCount);

            for (int i = 0; i < randCount; ++i)
            {
                _numbers.Add(BigIntegerExtension.GetRandom(bytesCount));
            }

            for (int i = 0; i < primCount; ++i)
            {
                _numbers.Add(BigIntegerExtension.GetRandomPrime(bytesCount, (b) => b.SolovayStrassenTest(1000)));
            }
        }

        [TestMethod]
        public void MillerRabinTest()
        {
            foreach (BigInteger number in _numbers)
            {
                number.MillerRabinTest(1000);
            }
        }

        [TestMethod]
        public void SolovayStrassenTest()
        {
            foreach (BigInteger number in _numbers)
            {
                number.SolovayStrassenTest(1000);
            }
        }

        [TestMethod]
        public void SimpleDivTest()
        {
            if (_numbers[0].GetByteCount() > 8)
                throw new Exception("To big number");
            foreach (BigInteger number in _numbers)
            {
                number.IsPrime();
            }
        }
    }
}