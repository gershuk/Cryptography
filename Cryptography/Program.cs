using Cryptography;

//Console.WriteLine("Введите число байт на ключи");

//var first = BigIntegerExtension.GetRandom(Convert.ToInt32(Console.ReadLine()));
//if (first.IsEven) first++;

//while (!first.SolovayStrassenTest(1000))
//    first += 2;

//var second = first + 2;

//while (!second.SolovayStrassenTest(1000))
//    second += 2;

//(var d, var n) = RsaCipher.MakeEncryptFile(first, second, "input.txt", "output.txt");
//RsaCipher.MakeDecipherFile(d, n, "output.txt", "test.txt");

//var p = BigIntegerExtension.GetRandomPrime(16, (b) => b.SolovayStrassenTest(1000));
//var generator = BigIntegerExtension.GetGroupGenerator(p);
//DiffieHellman d1 = new(BigIntegerExtension.GetRandom(4), generator, p);
//DiffieHellman d2 = new(BigIntegerExtension.GetRandom(4), generator, p);
//d1.SetB(d2.A);
//d2.SetB(d1.A);
//var y = d1._k == d2._k;

var e1 = new ElGamalsEDS(4);
var (a, b, x, y) = e1.Sign(5);
e1.CheckSign(5, a, b, y);