using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.Checkers
{
    [CommandLineArgument("-varToType")]
    public class SwapVarWithExplicitTypeChecker : IChecker
    {

        public SyntaxNode Check(SyntaxTree tree, SemanticModel semanticModel)
        {
            var rewritten = new RewriterVarToExplicitType(semanticModel).Visit(tree.GetRoot());
            return rewritten;
        }
    }
}
