using System.Numerics;

namespace Cryptography
{
    public class ElGamalsCipher
    {
        public BigInteger P { get; }
        public BigInteger G { get; }

        public ElGamalsCipher (int bytesCount)
        {
            P = BigIntegerExtension.GetRandomPrime(bytesCount, b => b.SolovayStrassenTest(1000));
            G = BigIntegerExtension.GetGroupGenerator(P);
        }

        public ElGamalsCipher (BigInteger p, BigInteger g) => (P, G) = (p, g);

        public (BigInteger a, BigInteger b, BigInteger x) Encode (BigInteger M)
        {
            var x = BigIntegerExtension.GetRandom(P.GetByteCount()) % P;
            var k = BigIntegerExtension.GetRandomPrime(P.GetByteCount(), b => b.SolovayStrassenTest(1000));
            var y = BigInteger.ModPow(G, x, P);
            var a = BigInteger.ModPow(G, k, P);
            var b = (BigInteger.ModPow(y, k, P) * M) % P;

            return (a, b, x);
        }

        public BigInteger Decode (BigInteger a, BigInteger b, BigInteger x) => (b * BigInteger.ModPow(a, P - 1 - x, P)) % P;
    }
}