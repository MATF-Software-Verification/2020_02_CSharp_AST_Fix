using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.Checkers
{
    [CommandLineArgument("-forToWhile")]
    public class SwapForWithWhileChecker : IChecker
    {
        public SyntaxNode Check(SyntaxTree tree, SemanticModel semanticModel)
        {
            var rewritten = new RewriterForToWhile().Visit(tree.GetRoot());
            return rewritten;
        }
    }
}
