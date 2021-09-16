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
            Console.WriteLine("Enter input file path:");
            string inputPath = Console.ReadLine();
            Console.WriteLine("Enter output file path:");
            string outputPath = Console.ReadLine();

            string text = File.ReadAllText(inputPath);
            
            SyntaxTree tree = CSharpSyntaxTree.ParseText(text);

            var Mscorlib = MetadataReference.CreateFromFile(typeof(object).Assembly.Location);
   
            var type = typeof(IChecker);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p));

            foreach (var arg in args)
            {
                var pairedChecker = types.FirstOrDefault(type => type.CustomAttributes.Where(att => att.ConstructorArguments.Where(constArg => constArg.Value.ToString() == arg).Count() != 0).Count() != 0);
                if (pairedChecker != null)
                {
                    var compilation = CSharpCompilation.Create("RoslynRewrite",
                    syntaxTrees: new[] { tree },
                    references: new[] { Mscorlib });
                    var model = compilation.GetSemanticModel(tree);
                    IChecker instance = (IChecker)Activator.CreateInstance(pairedChecker);
                    tree = CSharpSyntaxTree.ParseText(instance.Check(tree, model).ToString());
                }  
            }

            var root = tree.GetRoot().NormalizeWhitespace();
            var ret = root.ToFullString();

            await File.WriteAllTextAsync(outputPath, ret);

        }


    }
}
