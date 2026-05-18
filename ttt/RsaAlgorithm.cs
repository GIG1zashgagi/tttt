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
    }
}