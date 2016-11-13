using System.IO;


namespace SlothUnit.Parser.Core
{
	public class SlothGenerator
	{
		private string GeneratedFolder { get; }

		public SlothGenerator(string rootPath)
		{
			GeneratedFolder = Path.Combine(rootPath, NameOfThe.GeneratedFolder);
		}

		public void GenerateFolder()
		{
			Directory.CreateDirectory(GeneratedFolder);
		}

		public void GenerateMainFile()
		{
			GenerateFile(NameOfThe.MainFile,
@"#include ""../../SlothUnit/SlothUnit.h""
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

		private void GenerateFile(string fileName, string content)
		{
			File.WriteAllText(Path.Combine(GeneratedFolder, fileName), content);
		}
	}
}