using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ConsoleApp
{
    class test_empty_02
    {
        void doSomething()
        {
            ; // empty statement in body
        }

        void doSomethingElse()
        {
            Console.WriteLine("Hello, world!"); ;   // double ;
        }

        static void Test(string[] args) {

            // This code prints from 1 to 10
            for (int i = 0; i < 10; Console.WriteLine(++i)) {
                ; ; ; ; ; ; ; ; ; ; ; ; ; ; ; ;
            }
            
        }
    }
}
