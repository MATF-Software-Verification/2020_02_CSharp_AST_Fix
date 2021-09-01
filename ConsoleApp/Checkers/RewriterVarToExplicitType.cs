using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.Checkers
{
    class VarRewriter: CSharpSyntaxRewriter
    {
        private readonly SemanticModel SemanticModel;

        public VarRewriter(SemanticModel model)
        {
            SemanticModel = model;
        }

        public override SyntaxNode VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax node)
        {
            var symbolInfo = SemanticModel.GetSymbolInfo(node.Declaration.Type);
            var typeSymbol = symbolInfo.Symbol;
            var type = typeSymbol.ToDisplayString(
              SymbolDisplayFormat.MinimallyQualifiedFormat);

            var declaration = SyntaxFactory
                .LocalDeclarationStatement(
                    SyntaxFactory
                        .VariableDeclaration(SyntaxFactory.IdentifierName(
                          SyntaxFactory.Identifier(type)))
                            .WithVariables(node.Declaration.Variables)
                            .NormalizeWhitespace()
                    )
                    .WithTriviaFrom(node);
            return declaration;
        }
    }
}
