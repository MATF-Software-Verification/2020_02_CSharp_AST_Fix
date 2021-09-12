using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.Checkers
{
    public class SwapEmptyStatement : IChecker
    {

        public SyntaxNode Check(SyntaxTree tree, SemanticModel semanticModel)
        {
            var rewritten = new RewriterRemoveEmptyStatement().Visit(tree.GetRoot());
            return rewritten;
        }
    }
}
