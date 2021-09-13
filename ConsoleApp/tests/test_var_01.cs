using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp
{
    class test_var_01
    {
        public void Test()
        {
            var list = new List<int> { 0, 1, 2 };

            for (var x = 1; x < 10; x++) {
                Console.WriteLine(x);
            }

            foreach (var item in list) {
                Console.WriteLine(item);
            }

        }
    }
}