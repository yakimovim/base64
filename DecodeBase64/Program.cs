using System;
using System.IO;

namespace DecodeBase64
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: debase64.exe <input base64-encoded file> <output file>");
                Console.WriteLine("Example: debase64.exe file.base64 file.zip");
                return;
            }

            var inputFile = args[0];
            var outputFile = args[1];

            if (inputFile == outputFile)
            {
                Console.WriteLine("Can't convert to the same file");
                return;
            }

            if (!File.Exists(inputFile))
            {
                Console.WriteLine($"Input file '{inputFile}' is not found.");
                return;
            }

            if (File.Exists(outputFile))
            {
                Console.Write($"Output file '{outputFile}' already exists. Do you want to rewrite it (Y/N)? ");
                var answer = Console.ReadLine();

                if (answer?.ToLowerInvariant() == "y")
                {
                    File.Delete(outputFile);
                }
                else
                {
                    return;
                }
            }

            try
            {
                var inputFileContent = File.ReadAllText(inputFile);

                var outputFileContent = Convert.FromBase64String(inputFileContent);

                File.WriteAllBytes(outputFile, outputFileContent);

                Console.WriteLine($"File '{inputFile}' is decoded into '{outputFile}'.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"There is an error during conversion: {e.Message}");
                throw;
            }
        }
    }
}
