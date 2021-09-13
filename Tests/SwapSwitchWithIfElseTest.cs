using ConsoleApp.Checkers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass]
    public class SwapSwitchWithIfElseTest
    {
        private string inputText;
        private SyntaxTree SyntaxTree;
        private SemanticModel SemanticModel;

        [TestInitialize]
        public void Setup()
        {
            inputText = @"using System;

namespace ConsoleApp
{
    class test_switch_02
    {
        public void test()
        {
            int n = 1;
            switch (n)
            {
                case 1:
                    Console.WriteLine(1);
                    break;
                default:
                    Console.WriteLine(0);
                    break;
            }   
        }
    }
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
        public async Task SwapSwitchWithIfElseSuccess()
        {
            SwapSwitchWithIfElseChecker switchChecker = new SwapSwitchWithIfElseChecker();
            SyntaxNode newTree = switchChecker.Check(SyntaxTree, SemanticModel);
            
            var tree1 = CSharpSyntaxTree.ParseText(newTree.ToString());
            var root = tree1.GetRoot().NormalizeWhitespace();
            var ret = root.ToFullString();

            string correctResult = @"using System;

namespace ConsoleApp
{
    class test_switch_02
    {
        public void test()
        {
            int n = 1;
            if (n == 1)
            {
                Console.WriteLine(1);
            }
            else
            {
                Console.WriteLine(0);
            }
        }
    }
}";

            Assert.AreEqual(correctResult, ret);

        }
    }
}
