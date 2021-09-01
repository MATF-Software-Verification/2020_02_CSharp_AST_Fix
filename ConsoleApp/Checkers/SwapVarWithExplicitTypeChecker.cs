using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.Checkers
{
    public class VarChecker : IChecker
    {

        public SyntaxNode Check(SyntaxTree tree, SemanticModel semanticModel)
        {
            var rewritten = new VarRewriter(semanticModel).Visit(tree.GetRoot());
            return rewritten;
        }
    }
}
