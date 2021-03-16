using Cryptography;

using System;
using System.Numerics;
using System.Security.Cryptography;

Random random = new();
RNGCryptoServiceProvider rng = new();

Console.WriteLine("Введите число байт на ключи");
var count = Convert.ToInt32(Console.ReadLine());

var bytes = new byte[count];
bytes[^1] = (byte)random.Next(1, 255);
rng.GetBytes(bytes, 0, bytes.Length - 1);
bytes[^1] &= 0x7F;
var first = new BigInteger(bytes);
if (first.IsEven) first++;

while (!first.SolovayStrassenTest(1000))
    first += 2;

var second = first + 2;

while (!second.SolovayStrassenTest(1000))
    second += 2;

(var d, var n) = RsaCipher.MakeEncryptFile(first, second, "input.txt", "output.txt");
RsaCipher.MakeDecipherFile(d, n, "output.txt", "test.txt");