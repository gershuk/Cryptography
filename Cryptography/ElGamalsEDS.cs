using System.Numerics;

namespace Cryptography
{
    public class ElGamalsEDS
    {
        private readonly BigInteger _p;
        private readonly BigInteger _g;

        public ElGamalsEDS(int bytesCount)
        {
            _p = BigIntegerExtension.GetRandomPrime(bytesCount, b => b.SolovayStrassenTest(1000));
            _g = BigIntegerExtension.GetGroupGenerator(_p);
        }

        public ElGamalsEDS(BigInteger p, BigInteger g) => (_p, _g) = (p, g);

        public (BigInteger a, BigInteger b) Sign(BigInteger h, BigInteger x)
        {
            var k = BigIntegerExtension.GetRandomPrime(_p.GetByteCount(), b => b.SolovayStrassenTest(1000));
            var a = BigInteger.ModPow(_g, k, _p);
            var _p_1 = _p - 1;
            (_, var k_1, _) = BigIntegerExtension.FindGcd(k, _p_1);
            k_1 = k_1 > 0 ? k_1 : k_1 + _p_1;
            var b = ((h - x * a) * k_1) % _p_1;
            b = b > 0 ? b : b + _p_1;

            return (a, b);
        }

        public bool CheckSign(BigInteger h, BigInteger a, BigInteger b, BigInteger y) => (BigInteger.ModPow(y, a, _p) * BigInteger.ModPow(a, b, _p)) % _p == BigInteger.ModPow(_g, h, _p);
    }
}
