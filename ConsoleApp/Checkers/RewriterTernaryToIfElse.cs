using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Text;


namespace ConsoleApp.Checkers
{
    class RewriterTernaryToIfElse : CSharpSyntaxRewriter
    {

        public override SyntaxNode VisitConditionalExpression(ConditionalExpressionSyntax node)
        {
            var syntaxNode = (ConditionalExpressionSyntax)base.VisitConditionalExpression(node);
            var whenTrue = syntaxNode.WhenTrue;
            var whenFalse = syntaxNode.WhenFalse;
            var condition = syntaxNode.Condition;

            
            var ifNode = SyntaxFactory.IfStatement(condition,
                                                   SyntaxFactory.ExpressionStatement(whenTrue),
                                                   SyntaxFactory.ElseClause(
                                                        SyntaxFactory.ExpressionStatement(whenFalse))
                                                   );

            return ifNode;
        }
    }
}
