using System;

namespace ConsoleApp
{
    class test_switch_02
    {
        public void test()
        {
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
        }
    }
}
