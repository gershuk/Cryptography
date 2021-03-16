using System.Numerics;

namespace Cryptography
{
    internal static class DiffieHellman
    {
        public static void MakeTest(BigInteger a, BigInteger b, BigInteger g, BigInteger p)
        {
            BigInteger A = g.FastPowModulo(a, p);
            BigInteger B = g.FastPowModulo(b, p);
            BigInteger K1 = B.FastPowModulo(a, p);
            BigInteger K2 = A.FastPowModulo(b, p);
        }
    }
}