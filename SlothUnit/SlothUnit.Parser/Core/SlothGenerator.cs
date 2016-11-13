using System.IO;


namespace SlothUnit.Parser.Core
{
	public class SlothGenerator
	{
		public string RootPath { get; set; }

		public SlothGenerator(string rootPath)
		{
			RootPath = rootPath;
		}

		public void GenerateFolder()
		{
			Directory.CreateDirectory(Path.Combine(RootPath, "__Generated__"));
		}

		public void GenerateMainFile()
		{
			const string name = "__Main__.generated.cpp";
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
			var generatedFolder = Path.Combine(RootPath, "__Generated__");
			File.WriteAllText(Path.Combine(generatedFolder, name), content);
		}
	}
}