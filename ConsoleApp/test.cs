using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp
{
    class test
    {
        public void Test()
        {
            int Limit(int max, int[] array)
            {
                return Math.Min(max, array.Length);
            }

            int[] values = {10, 20, 30, 40}; ; ; ;
            var x = 3;
            var y = 11.0;
            while (x < y) ;

            for (int i = 0; i < Limit(2, values); i++)
            {
                for (int j = 0; j < Limit(10, values); j++)
                {
                    Console.WriteLine("LIMIT 10: " + values[j]);
                    for (int k = 0; k < Limit(10, values); k++)
                    {
                        Console.WriteLine("LIMIT 10: " + values[k]);
                        if (k == 3) ;
                    }
                }

                Console.WriteLine("LIMIT 2: " + values[i]);
            }

            int n = 1;
            switch (n)
            {
                case 1:
                    Console.WriteLine(1);
                    break;
                default:
                    Console.WriteLine(0);
                    break;
            }

            var s = "1234"; ;
            for (int i = 0; i < Limit(10, values); i++)
            {
                Console.WriteLine("LIMIT 10: " + values[i]);
            }
        }
    }
}