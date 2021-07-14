using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp
{
    public interface IChecker
    {
        public SyntaxNode Check(SyntaxTree tree);
    }
}
