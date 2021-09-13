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
            inputText = @"
using System;
static void Main(string[] args)
{
    char ch = 'i';

    switch (ch) {
        case 'a':
        case 'e':
        case 'i':
        case 'o':
        case 'u':
            Console.WriteLine('Vowel');
            break;
        default:
            Console.WriteLine('Not a vowel');
            break;
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
        public async Task SwapSwitchWithIfElseSuccess()
        {
            SwapSwitchWithIfElseChecker switchChecker = new SwapSwitchWithIfElseChecker();
            SyntaxNode newTree = switchChecker.Check(SyntaxTree, SemanticModel);
            
            var tree1 = CSharpSyntaxTree.ParseText(newTree.ToString());
            var root = tree1.GetRoot().NormalizeWhitespace();
            var ret = root.ToFullString();

            string correctResult = @"using System;
static void Main(string[] args)
{
    char ch = 'i';

    if (ch == ch == ch == ch == 'a' || ch == 'e' || ch == 'i' || ch == 'o' || ch == 'u')
    {
        Console.WriteLine('Vowel');
    }
    else
    {
        Console.WriteLine('Not a vowel');
    }
}
";

            Assert.AreEqual(correctResult, ret);

        }
    }
}
