using System;
using System.Linq;
using SlothUnit.CodeGenerator.Core;


namespace SlothUnit.CodeGenerator
{
	class Program
	{
		private static void Main(string[] args)
		{
			if (args.Length == 1)
			{
				Console.WriteLine("SlothUnit: Parsing project files...");
				var rootPath = args.Single();
				var testFiles = SlothParser.RetrieveTestFilesIn(rootPath);

				Console.WriteLine("SlothUnit: Generating code...");
				var slothGenerator = SlothGenerator.For(rootPath);
				slothGenerator.GenerateMainFile();
				slothGenerator.GenerateIncludedTestsFile();
				slothGenerator.Generate(testFiles);
			}
			else
			{
				Console.WriteLine("Error: No root path parameter specified");
			}
		}
	}
}
