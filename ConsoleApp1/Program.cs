using System;

namespace ConsoleApp1
{
    internal class Program
    {
            static void Main(string[] args)
            {
            int index = 6;
            int val = 44;
            int[] a = new int[5];
            try
            {
                a[index] = val;
            }
            catch (IndexOutOfRangeException e)
            {
                Console.Write("Index out of bounds ");
            }
            Console.Write("Remaining program");
        }
        }
    }

