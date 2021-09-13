using ConsoleApp.Checkers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass]
    public class RemoveEmptyStatementsTest
    {
        private string inputText;
        private SyntaxTree SyntaxTree;
        private SemanticModel SemanticModel;

        [TestInitialize]
        public void Setup()
        {
            inputText = @"
using System;
static void Main(string[] args)
{
    for (int j = 0; j <= 10; j++) ;
    int x = 3;;
    if (x < 5) ;
    Console.WriteLine(2);;
}";

            SyntaxTree = CSharpSyntaxTree.ParseText(inputText);

            var Mscorlib = MetadataReference.CreateFromFile(typeof(object).Assembly.Location);
            var compilation = CSharpCompilation.Create("RoslynRewrite",
              syntaxTrees: new[] { SyntaxTree },
              references: new[] { Mscorlib });
            SemanticModel = compilation.GetSemanticModel(SyntaxTree);

        }
        [TestCleanup]
        public void Cleanup()
        {
        }

        [TestMethod]
        public async Task RemoveEmptySuccess()
        {
            RemoveEmptyStatementsChecker checker = new RemoveEmptyStatementsChecker();
            SyntaxNode newTree = checker.Check(SyntaxTree, SemanticModel);
            //var ret = newTree.ToFullString();
            var tree1 = CSharpSyntaxTree.ParseText(newTree.ToString());
            var root = tree1.GetRoot().NormalizeWhitespace();
            var ret = root.ToFullString();

            string correctResult = @"using System;

static void Main(string[] args)
{
    for (int j = 0; j <= 10; j++)
    {
    }

    int x = 3;
    if (x < 5)
    {
    }

    Console.WriteLine(2);
}";

            Assert.AreEqual(correctResult, ret);

        }
    }
}
