using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace LenguajeS
{
    public static class MathExtensions
    {
        private static List<BigInteger> primes = new List<BigInteger> {2};

        public static BigInteger ApproximateNthPrime(BigInteger nn)
        {
            BigInteger n = nn;
            BigInteger p;
            if (nn >= 7022)
            {
                p = n * (BigInteger) BigInteger.Log(n) + n * (BigInteger) (Math.Log(BigInteger.Log(n)) - 0.9385);
            }
            else if (nn >= 6)
            {
                p = n * (BigInteger) BigInteger.Log(n) + n * (BigInteger) Math.Log(BigInteger.Log(n));
            }
            else if (nn > 0)
            {
                p = new BigInteger[] {2, 3, 5, 7, 11}[(int) nn - 1];
            }
            else
            {
                p = 0;
            }

            return p;
        }

        public static int ApproximateNthPrime(int nn)
        {
            double n = nn;
            double p;
            if (nn >= 7022)
            {
                p = n * Math.Log(n) + n * (Math.Log(Math.Log(n)) - 0.9385);
            }
            else if (nn >= 6)
            {
                p = n * Math.Log(n) + n * Math.Log(Math.Log(n));
            }
            else if (nn > 0)
            {
                p = new int[] {2, 3, 5, 7, 11}[(int) nn - 1];
            }
            else
            {
                p = 0;
            }

            return (int) p;
        }

// Find all primes up to and including the limit
        public static BitArray SieveOfEratosthenes(int limit)
        {
            BitArray bits = new BitArray(limit + 1, true);
            bits[0] = false;
            bits[1] = false;
            for (int i = 0; i * i <= limit; i++)
            {
                if (bits[i])
                {
                    for (int j = i * i; j <= limit; j += i)
                    {
                        bits[j] = false;
                    }
                }
            }

            return bits;
        }

        public static bool IsPrime(this BigInteger num)
        {
            if (num > primes.Last())
            {
                primes = GetPrimes(num);
            }

            return primes.Contains(num);
        }

        public static List<BigInteger> GetPrimes(BigInteger n)
        {
            BigInteger limit = ApproximateNthPrime(n);
            BitArray bits = SieveOfEratosthenes((int) limit);
            List<BigInteger> primes = new List<BigInteger>();
            for (int i = 0, found = 0; i < limit && found < n; i++)
            {
                if (bits[i])
                {
                    primes.Add(i);
                    found++;
                }
            }

            return primes;
        }

        public static bool Divides(this BigInteger potentialFactor, BigInteger i)
        {
            return i % potentialFactor == 0;
        }

        public static IEnumerable<BigInteger> Factors(this BigInteger n)
        {
            List<BigInteger> list = new List<BigInteger>();

            int count = 0;

            // count the number of times 2 divides  
            while (!(n % 2 > 0))
            {
                // equivalent to n=n/2; 
                n >>= 1;

                count++;
            }

            // if 2 divides it 
            if (count > 0)
                list.Add(2);

            // check for all the possible 
            // numbers that can divide it 
            for (BigInteger i = 3; BigInteger.Pow(i, 2) <= n; i += 2)
            {
                count = 0;
                while (n % i == 0)
                {
                    count++;
                    n = n / i;
                }

                if (count > 0)
                    list.Add(i);
            }

            // if n at the end is a prime number. 
            if (n > 2)
                list.Add(n);

            return list;
        }


        public static bool IsPrime(this ulong num)
        {
            return ((BigInteger) num).Factors().All(t => t == 1 || t == num);
        }

        public static BitArray SieveOfSundaram(int limit)
        {
            limit /= 2;
            BitArray bits = new BitArray(limit + 1, true);
            for (int i = 1; 3 * i + 1 < limit; i++)
            {
                for (int j = 1; i + j + 2 * i * j <= limit; j++)
                {
                    bits[i + j + 2 * i * j] = false;
                }
            }

            return bits;
        }

        public static List<int> GetFirstNPrimers(int n)
        {
            int limit = ApproximateNthPrime(n);
            BitArray bits = SieveOfSundaram(limit);
            List<int> primes = new List<int>();
            primes.Add(2);
            for (int i = 1, found = 1; 2 * i + 1 <= limit && found < n; i++)
            {
                if (bits[i])
                {
                    primes.Add(2 * i + 1);
                    found++;
                }
            }

            return primes;
        }
    }
}