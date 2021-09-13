using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp
{
    class test_for_02
    {
        public void Test()
        {

            for (; ; )
            {
                Console.WriteLine("#");
            }

            int term = 6;
            for (int i = 1; i <= term; i++)
            {
                for (int j = term; j >= i; j--)
                {
                    Console.WriteLine("* ");
                }
                Console.WriteLine("\n");
            }
        }
    }
}
