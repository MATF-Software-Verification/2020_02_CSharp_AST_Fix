using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp
{
    [System.AttributeUsage(System.AttributeTargets.Class |
                       System.AttributeTargets.Struct)]
    public class CommandLineArgument : System.Attribute
    {
        private string name;

        public CommandLineArgument(string name)
        {
            this.name = name;
        }
    }
}
