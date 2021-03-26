using System.Numerics;

namespace Cryptography
{
    public class DiffieHellman
    {
        private readonly BigInteger _a;
        public BigInteger K { get; protected set; }
        private readonly BigInteger _p;

        public BigInteger A { get; protected set; }

        public void SetB (BigInteger b) => K = BigInteger.ModPow(b, _a, _p);

        public DiffieHellman (BigInteger a, BigInteger g, BigInteger p) => (_a, A, _p) = (a, BigInteger.ModPow(g, a, p), p);
    }
}