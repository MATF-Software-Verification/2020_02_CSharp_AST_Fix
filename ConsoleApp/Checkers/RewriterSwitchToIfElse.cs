using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp.Checkers
{
    class RewriterSwitchToIfElse : CSharpSyntaxRewriter
    {
        public override SyntaxNode VisitSwitchStatement(SwitchStatementSyntax node)
        {
            var syntaxNode = (SwitchStatementSyntax)base.VisitSwitchStatement(node);

            var expression = syntaxNode.Expression.ToString();
            var labels = syntaxNode.Sections.Select(s => s.Labels.ToArray());

            var statements = syntaxNode.Sections.Select(s => s.Statements.Where(statement => statement.Kind() != SyntaxKind.BreakStatement));
            StatementSyntax newNode = null;
            if (labels.Count() == 1 && labels.ElementAt(0).Select(l => l.Kind()).Contains(SyntaxKind.DefaultSwitchLabel))
            {
                newNode = SyntaxFactory.ParseStatement(SyntaxFactory.IfStatement(SyntaxFactory.ParseExpression("true"),
                    SyntaxFactory.Block(SyntaxFactory.ParseStatement(statements.ElementAt(0).Aggregate("", (x, y) => x.ToString() + " " + y.ToString())))).ToString());
            }
            else
            {
                newNode = SyntaxFactory.ParseStatement(CreateIfStatement(expression, labels, statements));
            }

            return newNode;
        }


        private string CreateIfStatement(string expression, IEnumerable<IEnumerable<SwitchLabelSyntax>> labels, IEnumerable<IEnumerable<StatementSyntax>> statements)
        {
            var labelValues = labels.Select(l => l.Select(lb => lb.Kind() == SyntaxKind.DefaultSwitchLabel ? "true" : ((CaseSwitchLabelSyntax)lb).Value.ToString()));
            var labelsWithoutCurrent = labels.Skip(1);
            var statementsWithoutCurrent = statements.Skip(1);

            IfStatementSyntax ifStatement = SyntaxFactory.IfStatement(
                SyntaxFactory.ParseExpression(
                    labelValues.ElementAt(0).Count() != 1 
                        ? labelValues.ElementAt(0).Aggregate((x, y) =>  expression + "==" + x + " || " + expression + "==" + y)
                        : labelValues.ElementAt(0).Select(l => expression + "==" + l.ToString()).First()),
                SyntaxFactory.Block(SyntaxFactory.ParseStatement(statements.ElementAt(0).Aggregate("", (x, y) => x.ToString() + " " + y.ToString()))),
                labelsWithoutCurrent.Count() != 0 && statementsWithoutCurrent.FirstOrDefault().FirstOrDefault() != null
                    ? labelsWithoutCurrent.ElementAt(0).Select(l => l.Kind()).Contains(SyntaxKind.DefaultSwitchLabel)
                        ? SyntaxFactory.ElseClause(SyntaxFactory.ParseStatement("{" + statementsWithoutCurrent.ElementAt(0).Aggregate("", (x, y) => x.ToString() + " " + y.ToString()) + "}"))
                        : SyntaxFactory.ElseClause(SyntaxFactory.ParseStatement(" " + CreateIfStatement(expression, labelsWithoutCurrent, statementsWithoutCurrent)))
                    : null
                );

            return ifStatement.ToString();
        }

    }

}
