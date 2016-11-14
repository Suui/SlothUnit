using System;
using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using SlothUnit.Parser.Core;
using SlothUnit.Parser.Infrastructure;
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
			SlothGenerator.GenerateMainFile();

			var generatedFile = RetrieveFile(GeneratedFolderPath, NameOfThe.MainFile);
			File.ReadAllText(generatedFile).Should().Be(ExpectedCodeFor.TheMainFile);
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
			File.ReadAllText(generatedFile).Should().Be(ExpectedCodeFor.AClassWithASingleTestMethod);
		}

		[Test]
		public void generate_the_file_for_a_class_with_multiple_methods()
		{
			var testFiles = SlothParser.RetrieveTestFilesIn(CodeGenerationTestPath);

			SlothGenerator.Generate(testFiles);

			var generatedFile = RetrieveFile(GeneratedFolderPath, "ClassWithMultipleTestMethods.generated.h");
			File.ReadAllText(generatedFile).Should().Be(ExpectedCodeFor.AClassWithMultipleTestMethods);
		}

		private static string RetrieveFile(string folderPath, string fileName)
		{
			if (File.Exists(Path.Combine(folderPath, fileName)))
				return Directory.GetFiles(folderPath)
								.Single(path => path.Equals(Path.Combine(folderPath, fileName)));
			
			throw new Exception($"The file with path {Path.Combine(folderPath, fileName)} was not generated." );
		}
	}
}
