using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.Checkers
{
    public class SwapTernaryWithIfElseChecker : IChecker
    {

        public SyntaxNode Check(SyntaxTree tree, SemanticModel semanticModel)
        {
            var rewritten = new RewriterTernaryToIfElse().Visit(tree.GetRoot());
            return rewritten;
        }
    }
}
