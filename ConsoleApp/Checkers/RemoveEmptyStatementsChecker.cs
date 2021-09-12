using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.Checkers
{
    [CommandLineArgument("-removeEmpty")]
    public class RemoveEmptyStatementsChecker : IChecker
    {
        public SyntaxNode Check(SyntaxTree tree, SemanticModel semanticModel)
        {
            var root = tree.GetRoot();
            var rewritten = new RewriterRemoveEmptyStatements().Visit(root);
            return rewritten;
        }
    }
}
