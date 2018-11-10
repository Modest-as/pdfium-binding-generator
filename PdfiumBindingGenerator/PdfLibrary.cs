using System.IO;
using CppSharp;
using CppSharp.AST;
using CppSharp.Generators;

namespace PdfiumBindingGenerator
{
    internal class PdfLibrary : ILibrary
    {
        private readonly string _includePath;
        private readonly string[] _headers;

        public PdfLibrary(string includePath, string[] headers)
        {
            _includePath = includePath;
            _headers = headers;
        }
 
        public void Setup(Driver driver)
        {
            var options = driver.Options;
            options.GeneratorKind = GeneratorKind.CSharp;

            var parserOptions = driver.ParserOptions;
            parserOptions.AddIncludeDirs(_includePath);

            var module = options.AddModule("PdfiumWrapper");
            module.SharedLibraryName = "pdfium";

            module.OutputNamespace = "Docnet.Core.Bindings";

            foreach (var header in _headers)
            {
                module.Headers.Add(Path.Combine(_includePath, header));
            }
        }

        public void SetupPasses(Driver driver) { }

        public void Preprocess(Driver driver, ASTContext ctx) { }

        public void Postprocess(Driver driver, ASTContext ctx) { }
    }
}