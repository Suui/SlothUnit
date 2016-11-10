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
		[Test]
		public void generate_the_main_file()
		{
			var folderName = "__Generated__";
			const string mainFileName = "__Main__.cpp";
			var slothGenerator = new SlothGenerator(TestProjectDir);

			slothGenerator.GenerateMainFile();

			var generatedFile = Directory.GetFiles(TestProjectDir).Single(filename => filename.Equals(mainFileName));
			var expectedMainFile = Directory.GetFiles(Path.Combine(SolutionDir, "SlothUnit.Parser.Test"))
											.Single(fileName => fileName.Equals(mainFileName));
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

		public void GenerateMainFile()
		{
			
		}
	}
}
