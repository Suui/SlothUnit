using System.IO;
using SlothUnit.Parser.Core.Collections;
using SlothUnit.Parser.Infrastructure;


namespace SlothUnit.Parser.Core
{
	public class SlothGenerator
	{
		private string GeneratedFolder { get; }

		public static SlothGenerator For(string rootPath)
		{
			var generatedFolderPath = Path.Combine(rootPath, NameOfThe.GeneratedFolder);

			if (!Directory.Exists(generatedFolderPath))
				Directory.CreateDirectory(generatedFolderPath);

			return new SlothGenerator(generatedFolderPath);
		}

		private SlothGenerator(string generatedFolderPath)
		{
			GeneratedFolder = generatedFolderPath;
		}

		public void GenerateMainFile()
		{
			GenerateFile(NameOfThe.MainFile,
@"#include <SlothUnit.h>
#include ""__Tests__.generated.h""

using namespace SlothUnit;

int main()
{
	SlothTests::ExecuteAll();
	return 0;
}
");
		}

		public void GenerateIncludedTestsFile()
		{
			GenerateFile(NameOfThe.IncludedTestsFile, @"#pragma once");
		}

		public void Generate(TestFiles testFiles)
		{
			testFiles.ForEach(testFile =>
			{
				var testClasses = testFile.TestClasses.GeneratedCode(testFile.Path);
				var content =
$@"#pragma once
#include ""{testFile.Path}""

{testClasses}";

				var fileName = testFile.Name.Substring(0, testFile.Name.LastIndexOf('.'));
				GenerateFile(fileName + ".generated.h", content);
			});
		}

		private void GenerateFile(string fileName, string content)
		{
			File.WriteAllText(Path.Combine(GeneratedFolder, fileName), content);
		}
	}
}