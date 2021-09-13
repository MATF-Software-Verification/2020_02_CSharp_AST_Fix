using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.Checkers
{
    [CommandLineArgument("-switchToIf")]
    public class SwapSwitchWithIfElseChecker : IChecker
    {
        public SyntaxNode Check(SyntaxTree tree, SemanticModel semanticModel)
        {
            var rewritten = new RewriterSwitchToIfElse().Visit(tree.GetRoot());
            return rewritten;
        }
    }
}
