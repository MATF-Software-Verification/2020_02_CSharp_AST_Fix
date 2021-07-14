using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.Checkers
{
    public class SwapForWithWhileChecker : IChecker
    {
        public SyntaxNode Check(SyntaxTree tree)
        {
            var rewritten = new RewriterForToWhile().Visit(tree.GetRoot());
            return rewritten;
        }
    }
}
