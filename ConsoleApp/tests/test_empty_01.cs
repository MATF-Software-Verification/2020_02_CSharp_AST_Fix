using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp
{
    class Program1
    {
        public bool print()
        {
            Console.WriteLine("Steven Clark");
            return true;
        }
        static void Test(string[] args)
        {
            int i = 0;
            Program1 p = new Program1();
            while (p.print())
            {
                ;
                //Empty Statement
            }
            Console.WriteLine("Hello, world!");;
            Console.WriteLine("i = {0}", i);
            Console.ReadLine();

            while (p.print()) ;

            if (p.print()) ;

            for (int j = 0; j <= 10; j++) ;
            for (; ; );


        }

        static void Mtestain(string[] args)
        {
            ; ; ;
            { }

            if (true)
                if (true) ; else;
            else;
            //code



            int day = 4;
            switch (day)
            {
                case 1:
                    Console.WriteLine("Monday");
                    break;
                case 2:
                    Console.WriteLine("Tuesday");
                    break;
                case 3:
                    Console.WriteLine("Wednesday");
                    break;
                case 4:
                    Console.WriteLine("Thursday");
                    break;
                case 5:
                    Console.WriteLine("Friday");
                    break;
                case 6:
                    ;
                    break;
                case 7:
                    Console.WriteLine("Sunday");
                    break;
            }
        }



    }
}