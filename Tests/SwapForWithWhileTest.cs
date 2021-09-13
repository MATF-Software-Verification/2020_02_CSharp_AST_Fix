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
    for (int i = 0; i < 10; i++)
    {
        for (int j = 0; j < 10; j++)
        {
            Console.WriteLine(j);
            for (int k = 0; k < 10; k++)
            {
                Console.WriteLine(k);
            }
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
        public async Task SwapForWithWhileSuccess()
        {
            SwapForWithWhileChecker checker = new SwapForWithWhileChecker();
            SyntaxNode newTree = checker.Check(SyntaxTree, SemanticModel);
            
            var tree1 = CSharpSyntaxTree.ParseText(newTree.ToString());
            var root = tree1.GetRoot().NormalizeWhitespace();
            var ret = root.ToFullString();

            string correctResult = @"
using System;

static void Main(string[] args)
{
    {
        int i = 0;
        while (i < 10)
        {
            {
                int j = 0;
                while (j < 10)
                {
                    Console.WriteLine(j);
                    {
                        int k = 0;
                        while (k < 10)
                        {
                            Console.WriteLine(k);
                            k++;
                        }
                    }

                    j++;
                }
            }

            Console.WriteLine(i);
            i++;
        }
    }
}
";

            Assert.AreEqual(correctResult, ret);

        }
    }
}