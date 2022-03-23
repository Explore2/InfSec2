using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Text;

namespace InfSec2
{
    
    class Program
    {
        private static List<char> alphabet;
        
        private const int UTF8Coef = 143;
        private static byte UTF8HelpingByte = 0;

        static void GenerateAplhabet()
        {
            Console.OutputEncoding = Encoding.UTF8;
            alphabet = new List<char>();
            for (int i = 0; i < 6; i++)
            {
                alphabet.Add((char) ('а'+ i));
            }
            alphabet.Add('ё');
            for (int i = 8; i < 34; i++)
            {
                alphabet.Add((char)('а' + i - 2));
            }
            alphabet.Add(' ');
            for (int i = 0; i < 10; i++)
            {
                alphabet.Add((char)('0' + i));
            }
        }

        static int[] GetIntsFromChar(string text)
        {
            int[] arr = new int[text.Length];
            for(int i = 0; i < text.Length; i++)
            {
                arr[i] = alphabet.IndexOf(text[i]);
            }

            return arr;
        } 
        static int PowMod(int number, int power, int module)
        {
            ulong ret = 1;
            for (int i = 0; i < power; i++)
            {
                ret = (ret * (ulong) number) % (ulong) module;
            }
            return (int)ret;
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
            } while (N <= 45 || N >= 350);

            return (N, p, q);
        }


        static int EulerFunction(int p, int q)
        {
            return (p - 1) * (q - 1);
        }

        static int SolveD(int phi, int e)
        {
            double d;
            int k = 1;
            do
            {
                d = (double)(k * phi + 1) / e;
                k++;
            } while (d != (int) d);

            return (int) d;
        }

        static int[] EncryptDecrypt(int e, int n, int[] text)
        {
            int[] encryptedText = new int[text.Length];
            
            for (int i = 0; i < text.Length; i++)
            {
                encryptedText[i] = PowMod(text[i], e, n);
            }
            return encryptedText;
        }

        static void Main(string[] args)
        {
            GenerateAplhabet();
            string inputText = "кафси";
            int[] inputNums = GetIntsFromChar(inputText);
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
            int[] arr = EncryptDecrypt(e, n, inputNums);
            Console.Write("Encrypted: ");
            foreach (var num in arr)
            {
                Console.Write(num + " ");
            }

            Console.Write("\n\n");
            int[] arr2 = EncryptDecrypt(d, n, arr);
            foreach (var num in arr2)
            {
                Console.WriteLine(num + 1 + " " + alphabet[num]);
            }
        }
    }
}
