using System;
using System.Numerics;
using System.Security.Cryptography;

namespace Cryptography
{
    public static class BigIntegerExtension
    {
        public static BigInteger FastPowModulo(this BigInteger x, BigInteger n, BigInteger modulo)
        {
            BigInteger count = 1;
            if (n == 0)
            {
                return 1;
            }

            while (n != 0)
            {
                if (n % 2 == 0)
                {
                    n /= 2;
                    x = (x * x) % modulo;
                }
                else
                {
                    n--;
                    count = (count * x) % modulo;
                }
            }
            return count;
        }

        public static BigInteger Sqrt(this BigInteger n)
        {
            if (n == 0)
            {
                return 0;
            }

            if (n > 0)
            {
                int bitLength = Convert.ToInt32(Math.Ceiling(BigInteger.Log(n, 2)));
                BigInteger root = BigInteger.One << (bitLength / 2);

                while (!n.IsSqrt(root))
                {
                    root += n / root;
                    root /= 2;
                }

                return root;
            }

            throw new ArithmeticException("NaN");
        }

        public static bool IsPrime(this BigInteger number)
        {
            if (number <= 1 || number % 2 == 0)
            {
                return false;
            }

            if (number == 2)
            {
                return true;
            }

            BigInteger sqrt = number.Sqrt();
            for (int i = 3; i <= sqrt; ++i)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool MillerRabinTest(this BigInteger n, long r)
        {
            BigInteger nn = n - 1;
            if (n == 2 || n == 3)
            {
                return true;
            }

            if (n < 2 || n % 2 == 0)
            {
                return false;
            }

            BigInteger d = n - 1;
            long s = 0;

            while (d % 2 == 0)
            {
                d /= 2;
                s++;
            }

            BigInteger first;
            BigInteger second;

            for (long tryCount = 2L; tryCount < r + 1; tryCount++)
            {
                first = BigInteger.ModPow(tryCount, d, n);
                if (first >= 1 && first <= nn)
                {
                    continue;
                }

                bool notFinded = true;
                for (long i = 1L; tryCount <= s; i++)
                {
                    second = BigInteger.ModPow(first, 2, n);
                    if (second == nn)
                    {
                        notFinded = false;
                        break;
                    }

                    first = second;
                }

                if (notFinded)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool SolovayStrassenTest(this BigInteger n, int k)
        {
            RNGCryptoServiceProvider rng = new();
            byte[] array = new byte[n.GetByteCount()];
            for (int i = 0; i < k; i++)
            {
                rng.GetBytes(array);
                array[^1] &= 0x7F; //force sign bit to positive
                BigInteger a = new BigInteger(array) % n;
                if (a == 0)
                {
                    a = 1;
                }

                BigInteger jacabi = JacabiChar.Calc(a, n);
                if (jacabi.Sign == -1)
                {
                    jacabi = n + jacabi;
                }

                if (!(BigInteger.GreatestCommonDivisor(n, a) == 1 && BigInteger.ModPow(a, (n - 1) / 2, n) == jacabi))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool IsSqrt(this BigInteger n, BigInteger root)
        {
            BigInteger lowerBound = root * root;
            BigInteger upperBound = (root + 1) * (root + 1);

            return n >= lowerBound && n < upperBound;
        }

        public static BigInteger GetRandom(int bytesCount)
        {
            Random random = new();
            RNGCryptoServiceProvider rng = new();
            byte[] bytes = new byte[bytesCount];
            bytes[^1] = (byte)random.Next(1, 255);
            rng.GetBytes(bytes, 0, bytes.Length - 1);
            bytes[^1] &= 0x7F;
            BigInteger number = new BigInteger(bytes);
            if (number.IsEven)
            {
                number++;
            }

            while (!number.SolovayStrassenTest(1000))
            {
                number += 2;
            }

            return number;
        }
    }
}