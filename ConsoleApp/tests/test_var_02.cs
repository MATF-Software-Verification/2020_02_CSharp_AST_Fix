using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp
{
    class test_var_02
    {
		public void Test()
		{
			// i is compiled as an int
			var i = 5;

			// s is compiled as a string
			var s = "Hello";

			// l is compiled as int[]
			var l = new[] { 1, 2, 7, 9};

			// list is compiled as List<int>
			var list = new List<int>();
		}
	}
}
