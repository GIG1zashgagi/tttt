using System;               
using System.Numerics;      
using System.Text;          
using System.Collections.Generic;

namespace ttt
{
    public class RsaAlgorithm
    {
        private static Random _random = new Random();

        public static bool IsPrime(int number)
        {
            if (number < 2) return false;

            if (number == 2) return true;

            if (number % 2 == 0) return false;

            int limit = (int)Math.Sqrt(number);
            for (int i = 3; i <= limit; i += 2)
            {
                if (number % i == 0) return false;  
            }
            return true;  
        }
        public static int GeneratePrime(int min = 7, int max = 997)
        {
            int candidate;
            do
            {
                candidate = _random.Next(min, max + 1);  
            } while (!IsPrime(candidate));  
            return candidate;
        }
        public static BigInteger GCD(BigInteger a, BigInteger b)
        {
            while (b != 0)
            {
                BigInteger temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }
        public static BigInteger ModInverse(BigInteger e, BigInteger phi)
        {
            BigInteger t = 0, newT = 1;
            BigInteger r = phi, newR = e;

            while (newR != 0)
            {
                BigInteger quotient = r / newR;
                BigInteger tempT = newT;
                newT = t - quotient * newT;
                t = tempT;

                BigInteger tempR = newR;
                newR = r - quotient * newR;
                r = tempR;
            }

            if (r > 1) throw new Exception("e и φ(n) не взаимно просты");
            if (t < 0) t += phi;
            return t;
        }
        public static void GenerateKeys(int p, int q, out BigInteger publicKey, out BigInteger privateKey, out BigInteger modulus)
        {
            if (!IsPrime(p)) throw new ArgumentException($"p = {p} не является простым числом");
            if (!IsPrime(q)) throw new ArgumentException($"q = {q} не является простым числом");

            modulus = p * q;

            BigInteger phi = (p - 1) * (q - 1);

            publicKey = 65537;
            if (GCD(publicKey, phi) != 1)
            {
                for (publicKey = 3; publicKey < phi; publicKey++)
                {
                    if (GCD(publicKey, phi) == 1)
                        break;
                }
                if (publicKey >= phi) throw new Exception("Не удалось найти подходящее значение e");
            }

            privateKey = ModInverse(publicKey, phi);
        }
    }
}