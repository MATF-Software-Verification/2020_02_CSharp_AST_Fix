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

        public bool FindSymbolDeclarationsInAncestors(CSharpSyntaxNode currentNode, string symbolToFind)
        {

            var show =currentNode
                .Parent.ChildNodes();
            return currentNode
                .Parent.ChildNodes() // get direct siblings
                .SelectMany(node => // find different declarations
                    (node as VariableDeclarationSyntax)?.Variables.Select(v => v.Identifier.ValueText)
                    ?? (node as FieldDeclarationSyntax)?.Declaration?.Variables.Select(v => v.Identifier.ValueText)
                    ?? (node as LocalDeclarationStatementSyntax)?.Declaration?.Variables.Select(v => v.Identifier.ValueText)
                    ?? new[] {
                (node as PropertyDeclarationSyntax)?.Identifier.ValueText,
                (node as MethodDeclarationSyntax)?.Identifier.ValueText,
                (node as ClassDeclarationSyntax)?.Identifier.ValueText,
                        })
                .Any(member => string.Equals(member, symbolToFind));
        }
    }
}
