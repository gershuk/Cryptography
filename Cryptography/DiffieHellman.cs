using System.Numerics;

namespace Cryptography
{
    public class DiffieHellman
    {
        private readonly BigInteger _a;
        public BigInteger _k;
        private readonly BigInteger _p;

        public BigInteger A { get; }

        public void SetB(BigInteger b)
        {
            _k = BigInteger.ModPow(b, _a, _p);
        }

        public DiffieHellman(BigInteger a, BigInteger g, BigInteger p)
        {
            (_a, A, _p) = (a, BigInteger.ModPow(g, a, p), p);
        }
    }
}