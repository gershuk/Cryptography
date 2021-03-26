using System;
using System.IO;
using System.Numerics;
using System.Text;

namespace Cryptography
{
    internal static class RsaCipher
    {
        public static (BigInteger d, BigInteger n) MakeEncryptFile (BigInteger p, BigInteger q, string pathIn, string pathOut)
        {
            checked
            {
                var n = p * q;
                var m = (p - 1) * (q - 1);
                var d = CalcD(m);
                var e = CalcE(d, m);

                using StreamReader reader = new(pathIn);
                using StreamWriter writer = new(pathOut);

                writer.WriteLine(Encode(reader.ReadToEnd(), e, n));

                return (d, n);
            }
        }

        public static void MakeDecipherFile (BigInteger d, BigInteger n, string pathIn, string pathOut)
        {
            checked
            {
                using StreamReader reader = new(pathIn);
                using StreamWriter writer = new(pathOut);

                var result = Dedoce(reader.ReadToEnd(), d, n);

                writer.WriteLine(result);
            }
        }

        private static BigInteger CalcD (BigInteger m) =>
            //var d = m - 1;
            //var sqrt = m.Sqrt();
            //for (int i = 2; i <= sqrt; ++i)
            //{
            //    if ((m % i == 0) && (d % i == 0))
            //    {
            //        d--;
            //        i = 2;
            //    }
            //}

            BigIntegerExtension.GetRandomPrime(m.GetByteCount(), (b) => b.SolovayStrassenTest(1000));

        private static BigInteger CalcE (BigInteger d, BigInteger m)
        {
            (var _, var e, var _) = BigIntegerExtension.FindGcd(d, m);
            return e > 0 ? e : e + m;
        }

        private static string Encode (string text, BigInteger e, BigInteger n)
        {
            StringBuilder stringBuilder = new();

            foreach (var character in text)
            {
                stringBuilder.Append(((BigInteger) character).FastPowModulo(e, n) + Environment.NewLine);
            }

            return stringBuilder.ToString();
        }

        private static string Dedoce (string text, BigInteger d, BigInteger n)
        {
            StringBuilder stringBuilder = new();

            foreach (var item in text.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries))
            {
                stringBuilder.Append((char) (BigInteger.Parse(item).FastPowModulo(d, n)));
            }

            return stringBuilder.ToString();
        }
    }
}