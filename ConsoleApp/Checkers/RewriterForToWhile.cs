using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp
{
    class RewriterForToWhile : CSharpSyntaxRewriter
    {
        public override SyntaxNode VisitForStatement(ForStatementSyntax node)
        {
            var syntaxNode = (ForStatementSyntax)base.VisitForStatement(node);
            var declarationNode = SyntaxFactory.ParseStatement($"{(syntaxNode.Declaration != null ? syntaxNode.Declaration.ToString() + ";" : "")}");
            var statement = syntaxNode.Statement.ToString();

            var whileNode = SyntaxFactory.WhileStatement(
                syntaxNode.Condition ?? SyntaxFactory.ParseExpression("true"), 
                SyntaxFactory.ParseStatement(
                    $"{{{FixParenthesis(statement)}{(syntaxNode.Incrementors.Count != 0 ? syntaxNode.Incrementors.ToString().Replace(',', ';') + ";" : "")}\n}}")
                );
            var newNode = SyntaxFactory.ParseStatement($"{{{declarationNode}\n{whileNode}}}");
            return newNode;
        }


        public string FixParenthesis(string statement) {
            if (statement[0] == '{')
            {
                statement = statement.Substring(1, statement.Length-2);
            }
            return statement;
        }
      
    }
}
