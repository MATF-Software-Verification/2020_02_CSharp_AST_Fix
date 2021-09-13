using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp
{
    class test_switch_02
    {

        public void test()
        {
            char ch;
            Console.WriteLine("Enter an alphabet");
            ch = Convert.ToChar(Console.ReadLine());

            switch (Char.ToLower(ch)) {
                case 'a':
                case 'e':
                case 'i':
                case 'o':
                case 'u':
                    Console.WriteLine("Vowel");
                    break;
                default:
                    Console.WriteLine("Not a vowel");
                    break;
            }
        }
    }
}
