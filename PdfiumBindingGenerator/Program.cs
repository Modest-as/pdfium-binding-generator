using System;
using System.IO;
using CppSharp;
using CppSharp.AST;
using CppSharp.Generators;

namespace PdfiumBindingGenerator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                throw new ArgumentOutOfRangeException(nameof(args));
            }

            var includeDir = args[0];

            if (!Directory.Exists(includeDir))
            {
                throw new DirectoryNotFoundException(includeDir);
            }

            ConsoleDriver.Run(new PdfLbrary(includeDir));

            Console.ReadLine();
        }
    }

    internal class PdfLbrary : ILibrary
    {
        private readonly string includePath;

        public PdfLbrary(string includePath)
        {
            this.includePath = includePath;
        }
 
        public void Setup(Driver driver)
        {
            var options = driver.Options;
            options.GeneratorKind = GeneratorKind.CSharp;

            var parserOptions = driver.ParserOptions;
            parserOptions.AddIncludeDirs(includePath);

            var module = options.AddModule("pdfium");

            foreach (var file in Directory.GetFiles(includePath, "*.h"))
            {
                module.Headers.Add(file);
            }
        }

        public void SetupPasses(Driver driver) { }

        public void Preprocess(Driver driver, ASTContext ctx) { }

        public void Postprocess(Driver driver, ASTContext ctx) { }
    }
}
