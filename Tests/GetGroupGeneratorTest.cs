using Cryptography;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Collections.Generic;
using System.Numerics;

namespace Tests
{
    [TestClass]
    public class GetGroupGeneratorTest
    {
        private static List<BigInteger> numbers;

        [TestInitialize]
        public void Init()
        {
            const int bytesCount = 8;
            const int primCount = 100;

            numbers = new(primCount);

            for (int i = 0; i < primCount; ++i)
            {
                numbers.Add(BigIntegerExtension.GetRandomPrime(bytesCount, (b) => b.SolovayStrassenTest(1000)));
            }
        }

        [TestMethod]
        public void GroupGeneratorByDivision()
        {
            foreach (var number in numbers)
                BigIntegerExtension.GetGroupGenerator(number);
        }
    }
}
