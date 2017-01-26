using System;
using System.Linq;
using SlothUnit.Parser.Core;


namespace SlothUnit.Parser
{
	class Program
	{
		private static void Main(string[] args)
		{
			if (args.Length == 1)
			{
				var rootPath = args.Single();
				var testFiles = SlothParser.RetrieveTestFilesIn(rootPath);
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
