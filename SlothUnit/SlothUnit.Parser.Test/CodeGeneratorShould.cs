using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using SlothUnit.Parser.Core;
using SlothUnit.Parser.Test.Helpers;
using File = System.IO.File;


/* TODO
	- __Tests__ file including all generated / +Recurse
*/

namespace SlothUnit.Parser.Test
{
	[TestFixture]
	public class CodeGeneratorShould : FileSystemTest
	{
		private SlothGenerator SlothGenerator { get; set; }
		private string GeneratedFolderPath { get; } = Path.Combine(TestProjectPath, NameOfThe.GeneratedFolder);

		[SetUp]
		public void given_a_sloth_generator()
		{
			SlothGenerator = SlothGenerator.For(TestProjectPath);
		}

		[TearDown]
		public void delete_generated_elements()
		{
			var generatedFolderPath = Path.Combine(TestProjectPath, NameOfThe.GeneratedFolder);

			if (Directory.Exists(generatedFolderPath))
				Directory.Delete(generatedFolderPath, true);
		}

		[Test]
		public void generate_the_folder_containing_the_generated_files()
		{
			Directory.GetDirectories(TestProjectPath)
					 .Contains(GeneratedFolderPath).Should().BeTrue();
		}

		[Test]
		public void generate_the_main_file()
		{
			var slothUnitTestDir = Path.Combine(SolutionPath, "SlothUnit.Parser.Test");

			SlothGenerator.GenerateMainFile();

			var expectedMainFile = RetrieveFile(slothUnitTestDir, NameOfThe.MainFile);
			var generatedFile = RetrieveFile(GeneratedFolderPath, NameOfThe.MainFile);
			File.ReadAllText(generatedFile).Should().Be(File.ReadAllText(expectedMainFile));
		}

		[Test]
		public void generate_the_included_tests_file()
		{
			SlothGenerator.GenerateIncludedTestsFile();

			var generatedFile = RetrieveFile(GeneratedFolderPath, NameOfThe.IncludedTestsFile);
			File.ReadAllText(generatedFile).Should().Be("#pragma once");
		}

		[Test]
		public void generate_the_file_for_a_class_with_a_single_method()
		{
			var testFiles = SlothParser.RetrieveTestFilesIn(CodeGenerationTestPath);

			SlothGenerator.Generate(testFiles);

			var generatedFile = RetrieveFile(GeneratedFolderPath, "ClassWithASingleTestMethod.generated.h");
			File.ReadAllText(generatedFile).Should().Be(GeneratedCodeFor.ClassWithASingleTestMethod);
		}

		private static string RetrieveFile(string folderPath, string fileName)
		{
			return Directory.GetFiles(folderPath)
							.Single(path => path.Equals(Path.Combine(folderPath, fileName)));
		}
	}
}
