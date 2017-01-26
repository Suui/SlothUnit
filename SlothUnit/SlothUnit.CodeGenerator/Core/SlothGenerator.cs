using System.Collections.Generic;
using System.IO;
using SlothUnit.CodeGenerator.Core.Collections;
using SlothUnit.CodeGenerator.Infrastructure;


namespace SlothUnit.CodeGenerator.Core
{
	public class SlothGenerator
	{
		private string RootPath { get; }
		private string GeneratedFolderPath { get; }

		public static SlothGenerator For(string rootPath)
		{
			var generatedFolderPath = Path.Combine(rootPath, NameOfThe.GeneratedFolder);

			if (!Directory.Exists(generatedFolderPath))
				Directory.CreateDirectory(generatedFolderPath);

			return new SlothGenerator(rootPath, generatedFolderPath);
		}

		private SlothGenerator(string rootPath, string generatedFolderPath)
		{
			RootPath = rootPath;
			GeneratedFolderPath = generatedFolderPath;
		}

		public void GenerateMainFile()
		{
			GenerateFile(NameOfThe.MainFile,
@"#include ""../SlothUnit/SlothUnit.h""
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
			GenerateFile(NameOfThe.IncludedTestsFile, "#pragma once\n");
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

				var destinationPath = testFile.Path.Remove(0, RootPath.Length+1);
				var filePath = destinationPath.Substring(0, destinationPath.LastIndexOf('.'));
				GenerateFile(filePath + ".generated.h", content);
			});
		}

		private void GenerateFile(string filePath, string content)
		{
			if (filePath.LastIndexOf('\\') > 0)
			{
				var folder = filePath.Substring(0, filePath.LastIndexOf('\\'));
				if (!Directory.Exists(Path.Combine(GeneratedFolderPath, folder)))
					Directory.CreateDirectory(Path.Combine(GeneratedFolderPath, folder));
			}

			File.WriteAllText(Path.Combine(GeneratedFolderPath, filePath), content);

			if (!filePath.Contains(NameOfThe.MainFile) && !filePath.Contains(NameOfThe.IncludedTestsFile))
			{
				File.AppendAllLines(Path.Combine(GeneratedFolderPath, NameOfThe.IncludedTestsFile), 
									new List<string> { $@"#include ""{filePath}""" });
			}
		}
	}
}