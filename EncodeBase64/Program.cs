using System;
using System.IO;

namespace EncodeBase64
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: enbase64.exe <input file> <output base64-encoded file>");
                Console.WriteLine("Example: enbase64.exe file.zip file.base64");
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
                var inputFileContent = File.ReadAllBytes(inputFile);

                var outputFileContent = Convert.ToBase64String(inputFileContent);

                File.WriteAllText(outputFile, outputFileContent);

                Console.WriteLine($"File '{inputFile}' is encoded into '{outputFile}'.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"There is an error during conversion: {e.Message}");
                throw;
            }
        }
    }
}
