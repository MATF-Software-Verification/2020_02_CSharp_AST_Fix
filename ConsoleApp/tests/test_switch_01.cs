using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp
{
    class test_switch_01
    {
        public void test()
        {
            
            int day = 4;
            switch (day)
            {
                case 1:
                    int bay = 3;
                    switch (bay)
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
                            Console.WriteLine("Saturday");
                            break;
                        case 7:
                        case 8:
                        default:
                            Console.WriteLine("Default");
                            break;
                    }
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
                    Console.WriteLine("Saturday");
                    break;
                case 7:
                case 8:
                    Console.WriteLine("Sunday");
                    break;
                default:
                    int hay = 3;
                    switch (hay)
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
                            Console.WriteLine("Saturday");
                            break;
                        case 7:
                        case 8:
                        default:
                            break;
                    }
                    break;
            }
        }
    }
}