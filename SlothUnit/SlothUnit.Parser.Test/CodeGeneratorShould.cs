using System;
using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;


/* TODO
	- __Generated__ folder
	- __Main__ file including slothunit and __Tests__
	- __Tests__ file including all generated / +Recurse
*/

namespace SlothUnit.Parser.Test
{
	[TestFixture]
	public class CodeGeneratorShould : FileSystemTest
	{
		private string GeneratedFolderPath { get; } = Path.Combine(TestProjectDir, "__Generated__");

		[TearDown]
		public void delete_generated_elements()
		{
			try
			{
				var generatedFolder = Directory.GetDirectories(TestProjectDir)
											   .Single(path => path.Equals(GeneratedFolderPath));
				Directory.Delete(generatedFolder, true);
			}
			catch (InvalidOperationException) {}
		}

		[Test]
		public void generate_the_folder_containing_the_generated_files()
		{
			var slothGenerator = new SlothGenerator(TestProjectDir);

			slothGenerator.GenerateFolder();

			Directory.GetDirectories(TestProjectDir)
					 .Contains(GeneratedFolderPath).Should().BeTrue();
		}

		[Test]
		public void generate_the_main_file()
		{
			const string mainFileName = "__Main__.generated.cpp";
			var slothGenerator = new SlothGenerator(TestProjectDir);

			slothGenerator.GenerateFolder();
			slothGenerator.GenerateMainFile();

			var slothUnitTestDir = Path.Combine(SolutionDir, "SlothUnit.Parser.Test");
			var expectedMainFile = Directory.GetFiles(slothUnitTestDir)
											.Single(path => path.Equals(Path.Combine(slothUnitTestDir, mainFileName)));
			var generatedFile = Directory.GetFiles(GeneratedFolderPath)
										 .Single(filename => filename.Equals(Path.Combine(GeneratedFolderPath, mainFileName)));
			File.ReadAllText(generatedFile).Should().Be(File.ReadAllText(expectedMainFile));
		}
	}

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
