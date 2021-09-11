using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp.Checkers
{
    class RewriterSwitchToIf : CSharpSyntaxRewriter
    {
        public override SyntaxNode VisitSwitchStatement(SwitchStatementSyntax node)
        {
            var result = (SwitchStatementSyntax)base.VisitSwitchStatement(node);
            // var declarationNode = SyntaxFactory.ParseStatement($"{(syntaxNode.Declaration != null ? syntaxNode.Declaration.ToString() + ";" : "")}");
            // var statement = syntaxNode.Statement.ToString();

            // var whileNode = SyntaxFactory.WhileStatement(
                // syntaxNode.Condition ?? SyntaxFactory.ParseExpression("true"),
                // SyntaxFactory.ParseStatement(
                    // $"{{{FixParenthesis(statement)}{(syntaxNode.Incrementors.Count != 0 ? syntaxNode.Incrementors.ToString().Replace(',', ';') + ";" : "")}\n}}")
                // );
            // var newNode = SyntaxFactory.ParseStatement($"{{{declarationNode}\n{whileNode}}}");
            // return newNode;

            // conversion
            // find true statement
            var trueSection = node.Sections
                    .First(f => f.Labels.First().ToString().Contains("true"));
            var falseSection = node.Sections
                    .First(f => f.Labels.First().ToString().Contains("false"));

            var trueStatement = trueSection.Statements.Count == 1
                            ? trueSection.Statements.First()
                            : SyntaxFactory.Block(trueSection.Statements);
            var falseStatement = falseSection.Statements.Count == 1
                            ? falseSection.Statements.First()
                            : SyntaxFactory.Block(falseSection.Statements);

            var ifStatement = SyntaxFactory.IfStatement(node.Expression,
                trueStatement,
                SyntaxFactory.ElseClause(falseStatement));

            // remove all breaks
            var breakRemover = new BreakRemover();

            result = (SwitchStatementSyntax)breakRemover.Visit(ifStatement);

            return result;
        }
    }

}
