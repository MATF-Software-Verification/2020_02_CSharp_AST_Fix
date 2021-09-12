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

        //public override SyntaxNode VisitConditionalExpression(ConditionalExpressionSyntax node)
        //{
        //    var syntaxNode = (ConditionalExpressionSyntax)base.VisitConditionalExpression(node);
        //    var whenTrue = syntaxNode.WhenTrue;
        //    var whenFalse = syntaxNode.WhenFalse;
        //    var condition = syntaxNode.Condition;


        //    var ifNode = SyntaxFactory.IfStatement(condition,
        //                                           SyntaxFactory.ExpressionStatement(whenTrue),
        //                                           SyntaxFactory.ElseClause(
        //                                                SyntaxFactory.ExpressionStatement(whenFalse))
        //                                           );

        //    return ifNode;
        //}



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


        //[return: NotNullIfNotNull("node")]
        //public override SyntaxNode Visit(SyntaxNode node)
        //{


        //    if (node == null)
        //    {
        //        return node;
        //    }

        //    var newNode = base.Visit(node);


        //    var children = newNode.ChildNodes();
        //    var grandChildren = children.SelectMany(child => child?.ChildNodes());

        //    foreach (var child in grandChildren)
        //    {
        //        if (child.Kind() == SyntaxKind.ConditionalExpression)
        //        {
        //            var conditionalChild = (ConditionalExpressionSyntax)child;
        //            var whenTrue = conditionalChild.WhenTrue;
        //            var whenFalse = conditionalChild.WhenFalse;
        //            var condition = conditionalChild.Condition;


        //            var trueChild = newNode.ReplaceNode(child, SyntaxFactory.ParseExpression(SyntaxFactory.ExpressionStatement(whenTrue).ToString()));
        //            var falseChild = newNode.ReplaceNode(child, SyntaxFactory.ParseExpression(SyntaxFactory.ExpressionStatement(whenFalse).ToString()));

        //            var ifNode = SyntaxFactory.IfStatement(condition,
        //                                                   SyntaxFactory.Block(SyntaxFactory.ParseStatement(trueChild.ToString())),
        //                                                   SyntaxFactory.ElseClause(
        //                                                       SyntaxFactory.Block(SyntaxFactory.ParseStatement(falseChild.ToString()))
        //                                                    )
        //                                                   );
        //            newNode = ifNode;
        //        }

        //    }

        //    return newNode;
        //}
    }
}
