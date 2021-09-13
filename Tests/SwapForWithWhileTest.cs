using ConsoleApp.Checkers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass]
    public class SwapForWithWhileTest
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
    for (int k = 1; k < 10; k++)
    {
        Console.WriteLine(k);
    }

    int term = 6;
    for (int i = 1; i <= term; i++)
    {
        for (int j = term; j >= i; j--)
        {
            Console.WriteLine(j);
        }
        Console.WriteLine(i);
    }
}
";

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
        public void SwapForWithWhileSuccess()
        {
            SwapForWithWhileChecker checker = new SwapForWithWhileChecker();
            SyntaxNode newTree = checker.Check(SyntaxTree, SemanticModel);

            var tree1 = CSharpSyntaxTree.ParseText(newTree.ToString());
            var root = tree1.GetRoot().NormalizeWhitespace();
            var ret = root.ToFullString();

            string correctResult = @"using System;

static void Main(string[] args)
{
    {
        int k = 1;
        while (k < 10)
        {
            Console.WriteLine(k);
            k++;
        }
    }

    int term = 6;
    {
        int i = 1;
        while (i <= term)
        {
            {
                int j = term;
                while (j >= i)
                {
                    Console.WriteLine(j);
                    j--;
                }
            }

            Console.WriteLine(i);
            i++;
        }
    }
}";

            Assert.AreEqual(correctResult, ret);

        }
    }
}
