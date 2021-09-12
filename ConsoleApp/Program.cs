using ConsoleApp.Checkers;
using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.Formatting;
using Microsoft.CodeAnalysis.MSBuild;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {

            //        SyntaxTree tree = CSharpSyntaxTree.ParseText(
            //@"using System;
            //using System.Collections.Generic;
            //using System.Text;

            //    static void Main(string[] args)
            //    {
            //    for(int i=0; i<10; i++)
            //    int a = i;
            //    }");

            Console.WriteLine("Enter input file path:");
            string inputPath = Console.ReadLine();
            Console.WriteLine("Enter output file path:");
            string outputPath = Console.ReadLine();

            string text = File.ReadAllText(inputPath);
            //D:\Programming\Home\VS\ConsoleApp\ConsoleApp\test.cs
            //string text = File.ReadAllText(@"D:\\Programming\\Home\\VS\\ConsoleApp\\ConsoleApp\\test.cs");
            SyntaxTree tree = CSharpSyntaxTree.ParseText(text);

            var Mscorlib = MetadataReference.CreateFromFile(typeof(object).Assembly.Location);
            var compilation = CSharpCompilation.Create("RoslynRewrite",
              syntaxTrees: new[] { tree },
              references: new[] { Mscorlib });
            var model = compilation.GetSemanticModel(tree);

            var type = typeof(IChecker);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p));

            SyntaxNode newTree = null;
            foreach (var arg in args)
            {
                var pairedChecker = types.FirstOrDefault(type => type.CustomAttributes.Where(att => att.ConstructorArguments.Where(constArg => constArg.Value.ToString() == arg).Count() != 0).Count() != 0);
                if (pairedChecker != null)
                {
                    IChecker instance = (IChecker)Activator.CreateInstance(pairedChecker);
                    newTree = instance.Check(tree, model);
                }  
            }

            var tree1 = CSharpSyntaxTree.ParseText(newTree.ToString());
            var root = tree1.GetRoot().NormalizeWhitespace();
            var ret = root.ToFullString();

            await File.WriteAllTextAsync(outputPath, ret);
            //Console.WriteLine(ret);

        }


    }
}
