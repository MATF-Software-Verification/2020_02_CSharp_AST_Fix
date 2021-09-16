using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;


namespace ConsoleApp.Checkers
{
    class RewriterRemoveEmptyStatements : CSharpSyntaxRewriter
    {

        public override SyntaxNode VisitEmptyStatement(EmptyStatementSyntax node)
        {
            if (node.Parent.Kind() == SyntaxKind.WhileStatement 
                || node.Parent.Kind() == SyntaxKind.IfStatement 
                || node.Parent.Kind() == SyntaxKind.ForStatement 
                || node.Parent.Kind() == SyntaxKind.ElseClause)
            {
                return SyntaxFactory.Block();
            }
            return null;
        }

    }
}
