using ConsoleApp.Checkers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass]
    public class SwapVarWithExplicitTypeTest
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
  var i = 5;
  if(i<10)
   var a = i + 1.3;
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
        public async Task SwapVarWithExplicitTypeSuccess()
        {
            SwapVarWithExplicitTypeChecker varChecker = new SwapVarWithExplicitTypeChecker();
            SyntaxNode newTree = varChecker.Check(SyntaxTree, SemanticModel);
            var ret = newTree.ToFullString();

            string correctResult = @"
using System;
 static void Main(string[] args)
 {
  int i = 5;
  if(i<10)
   double a = i + 1.3;
 }";

            Assert.AreEqual(correctResult, ret);

        }
    }
}
