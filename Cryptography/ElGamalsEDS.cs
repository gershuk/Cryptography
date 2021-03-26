using System.Numerics;

namespace Cryptography
{
    public class ElGamalsEDS
    {
        public BigInteger P { get; }
        public BigInteger G { get; }

        public ElGamalsEDS (int bytesCount)
        {
            P = BigIntegerExtension.GetRandomPrime(bytesCount, b => b.SolovayStrassenTest(1000));
            G = BigIntegerExtension.GetGroupGenerator(P);
        }

        public ElGamalsEDS (BigInteger p, BigInteger g) => (P, G) = (p, g);

        public (BigInteger a, BigInteger b, BigInteger x, BigInteger y) Sign (BigInteger h)
        {
            var x = BigIntegerExtension.GetRandom(P.GetByteCount()) % P;
            var y = BigInteger.ModPow(G, x, P);
            var k = BigIntegerExtension.GetRandomPrime(P.GetByteCount(), b => b.SolovayStrassenTest(1000));
            var a = BigInteger.ModPow(G, k, P);
            var _p_1 = P - 1;
            (_, var k_1, _) = BigIntegerExtension.FindGcd(k, _p_1);
            k_1 = k_1 > 0 ? k_1 : k_1 + _p_1;
            var b = ((h - x * a) * k_1) % _p_1;
            b = b > 0 ? b : b + _p_1;

            return (a, b, x, y);
        }

        public bool CheckSign (BigInteger h, BigInteger a, BigInteger b, BigInteger y) => (BigInteger.ModPow(y, a, P) * BigInteger.ModPow(a, b, P)) % P == BigInteger.ModPow(G, h, P);
    }
}