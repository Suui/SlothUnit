using System.IO;
using SlothUnit.Parser.Core.Collections;


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
			var testFile = testFiles.Single(file => file.Name.Equals("ClassWithASingleTestMethod.h"));
			var testClass = testFile.TestClasses.Single();
			var testMethod = testClass.TestMethods.Single();
			var content =
$@"#pragma once
#include ""{testFile.Path}""

auto registrar = TestRegistrar(TestClass<{testClass.Name}>
(
	""{testFile.Path}"",
	""{testClass.Name}"",
	{testClass.Name}(),
	{{
		{{ ""{testMethod.Name}"", &{testClass.Name}::{testMethod.Name} }}
	}}
));
";
			var fileName = testFile.Name.Substring(0, testFile.Name.LastIndexOf('.'));
			GenerateFile(fileName + ".generated.h", content);
		}

		private void GenerateFile(string fileName, string content)
		{
			File.WriteAllText(Path.Combine(GeneratedFolder, fileName), content);
		}
	}
}