using System;
using System.Data.SqlTypes;
using System.Text;

namespace InfSec2
{
    class Program
    {
        
        private const int UTF8Coef = 143; 
        static ulong PowMod(ulong number, int power, int module)
        {
            ulong ret = 1;
            for (int i = 0; i < power; i++)
            {
                ret = (ret * number) % (ulong) module;
            }
            return ret;
        }
        static bool CheckIfNumIsPrime(int number)
        {
            var divLimit = (int)Math.Pow(number, 0.5f);
            for (int i = 2; i <= divLimit; i++)
            {
                if (number % i == 0) return false;
            }

            return true;
        }
        
        static int GreatestCommonDivisor(int firstNum, int secondNum)
        {
            while (firstNum != 0 && secondNum != 0)
            {
                if (firstNum > secondNum)
                {
                    firstNum = firstNum % secondNum;
                }
                else
                {
                    secondNum = secondNum % firstNum;
                }
            }

            return firstNum + secondNum;
        }
        static int PrimeNumberGenerator(int topLimit)
        {
            int simpleNumber;
            Random random = new Random();
            do
            {
                simpleNumber = random.Next(2, topLimit);
            } while (!CheckIfNumIsPrime(simpleNumber));
            return simpleNumber;
        }
        static (int, int, int) NGenerator()
        {
            int N,p,q;
            do
            {
                p = PrimeNumberGenerator(20);
                q = PrimeNumberGenerator(20);
                N = p * q;
            } while (N <= 45);

            return (N, p, q);
        }


        static int EulerFunction(int p, int q)
        {
            return (p - 1) * (q - 1);
        }

        static int SolveD(int phi, int e)
        {
            decimal d;
            int k = 1;
            do
            {
                d = (decimal)(k * phi + 1) / e;
                k++;
            } while (d % 1 != 0);

            return (int) d;
        }

        static ulong[] EncryptDecrypt(int e, int n, ulong[] text)
        {
            ulong[] encryptedText = new ulong[text.Length];
            
            for (int i = 0; i < text.Length; i++)
            {
                encryptedText[i] = PowMod(text[i], e, n);
            }
            return encryptedText;
        }

        static void Main(string[] args)
        {
            Console.Write(Encoding.UTF8.GetChars(new byte[] {208, BitConverter.GetBytes(7 + UTF8Coef)[0]})[0] + "\n");
            return;
            int n, p, q, phi, d;
            (n, p, q) = NGenerator();
            phi = EulerFunction(p, q);
            int e;
            Random random = new Random();
            do
            {
                e = random.Next(0, n);
            } while (GreatestCommonDivisor(e, n) != 1);

            Console.WriteLine("Open key: " + (e, n));
            d = SolveD(phi, e);
            Console.WriteLine("Closed key: " + (d, n));
            ulong[] arr = EncryptDecrypt(5, 91, new ulong[] {12, 1, 22, 19, 10});
            foreach (var var in arr)
            {
                Console.Write(var + " ");
            }

            Console.WriteLine("\n\n");
            ulong[] arr2 = EncryptDecrypt(29, 91, arr);
            var i = 1;
            foreach (ulong var in arr2)
            { 
                
                //Console.WriteLine(var + UTF8Coef);
                if (var + UTF8Coef == 150)
                {
                    Console.Write(Encoding.UTF8.GetChars(new byte[] {208, 129}));
                }
                else
                {
                    Console.Write(Encoding.UTF8.GetChars(new byte[] {208, BitConverter.GetBytes(2 + UTF8Coef)[0]})[0] + "\n");
                    
                }
                i += 2;
            }
        }
    }
}
