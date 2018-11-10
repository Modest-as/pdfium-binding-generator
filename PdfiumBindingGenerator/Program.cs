using System;
using System.IO;
using CppSharp;

namespace PdfiumBindingGenerator
{
    public class Program
    {
        private static readonly string[] Headers =
        {
            "fpdfview.h",
            "fpdf_text.h",
            "fpdf_save.h",
            "fpdf_ppo.h",
            "fpdf_edit.h",
        };

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

            ConsoleDriver.Run(new PdfLibrary(includeDir, Headers));

            Console.ReadLine();
        }
    }
}
