using System.IO;


namespace SlothUnit.Parser.Core
{
	public class SlothGenerator
	{
		private string GeneratedFolder { get; }

		public SlothGenerator(string rootPath)
		{
			GeneratedFolder = Path.Combine(rootPath, "__Generated__");
		}

		public void GenerateFolder()
		{
			Directory.CreateDirectory(GeneratedFolder);
		}

		public void GenerateMainFile()
		{
			const string fileName = "__Main__.generated.cpp";
			const string content =
@"#include ""../../SlothUnit/SlothUnit.h""
#include ""__Tests__.generated.h""

using namespace SlothUnit;

int main()
{
	SlothTests::ExecuteAll();
	return 0;
}
";
			GenerateFile(fileName, content);
		}

		public void GenerateIncludedTestsFile()
		{
			const string fileName = "__Tests__.generated.h";
			const string content = @"#pragma once";
			GenerateFile(fileName, content);
		}

		private void GenerateFile(string fileName, string content)
		{
			File.WriteAllText(Path.Combine(GeneratedFolder, fileName), content);
		}
	}
}