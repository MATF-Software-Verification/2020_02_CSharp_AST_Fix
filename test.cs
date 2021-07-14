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

            int[] values = {10, 20, 30, 40};
            {
                int i = 0;
                while (i < Limit(2, values))
                {
                    {
                        int j = 0;
                        while (j < Limit(10, values))
                        {
                            Console.WriteLine("LIMIT 10: " + values[j]);
                            {
                                int k = 0;
                                while (k < Limit(10, values))
                                {
                                    Console.WriteLine("LIMIT 10: " + values[k]);
                                    k++;
                                }
                            }

                            j++;
                        }
                    }

                    Console.WriteLine("LIMIT 2: " + values[i]);
                    i++;
                }
            }

            {
                int i = 0;
                while (i < Limit(10, values))
                {
                    Console.WriteLine("LIMIT 10: " + values[i]);
                    i++;
                }
            }
        }
    }
}