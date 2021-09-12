using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.Checkers
{
    [CommandLineArgument("-switchToIf")]
    public class SwapSwitchToIf : IChecker
    {
        public SyntaxNode Check(SyntaxTree tree, SemanticModel semanticModel)
        {
            var rewritten = new RewriterSwitchToIf().Visit(tree.GetRoot());
            return rewritten;
        }
    }
}
