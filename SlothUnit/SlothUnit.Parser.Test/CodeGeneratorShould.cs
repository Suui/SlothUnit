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
			const string mainFileName = "__Main__.cpp";
			var generatedFolderPath = Path.Combine(TestProjectDir, "__Generated__");
			var slothGenerator = new SlothGenerator(TestProjectDir);

			slothGenerator.GenerateMainFile();

			var generatedFile = Directory.GetFiles(generatedFolderPath)
										 .Single(filename => filename.Equals(mainFileName));
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

		public void GenerateFolder()
		{
			Directory.CreateDirectory(Path.Combine(RootPath, "__Generated__"));
		}

		public void GenerateMainFile()
		{
			
		}
	}
}
