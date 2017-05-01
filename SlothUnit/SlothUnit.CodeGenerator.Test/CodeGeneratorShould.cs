using System;
using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using SlothUnit.CodeGenerator.Core;
using SlothUnit.CodeGenerator.Infrastructure;
using SlothUnit.CodeGenerator.Test.Helpers;
using File = System.IO.File;

/* TODO
	- Test the number generation for the registrars (registrar_0 ... registrar_X)? Seems a bit awful, delaying it
*/

namespace SlothUnit.CodeGenerator.Test
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
		public void not_generate_the_main_file_if_a_custom_main_method_is_present_in_the_test_project()
		{
			var customMainFilePath = Path.Combine(TestProjectPath, "CustomMain.cpp");
			File.WriteAllText(customMainFilePath, @"int main() { return 0; }");
			
			SlothGenerator.GenerateMainFile();

			Directory.GetFiles(GeneratedFolderPath)
					 .Contains(Path.Combine(GeneratedFolderPath, NameOfThe.MainFile)).Should().BeFalse();
		}

		[Test]
		public void generate_the_included_tests_file()
		{
			SlothGenerator.GenerateIncludedTestsFile();

			var generatedFile = RetrieveFile(GeneratedFolderPath, NameOfThe.IncludedTestsFile);
			File.ReadAllText(generatedFile).Should().Be("#pragma once\n");
		}

		[Test]
		public void generate_the_file_starting_with_pragma_include_and_global_variable()
		{
			var testFiles = SlothParser.RetrieveTestFilesIn(CodeGenerationTestPath);

			SlothGenerator.Generate(testFiles);

			var generatedFile = RetrieveFile(Path.Combine(GeneratedFolderPath, "CodeGeneration"), "StartOfTheFile.generated.h");
			File.ReadAllText(generatedFile).Should().StartWithEquivalent(ExpectedCodeFor.TheStartOfTheFile);
		}

		[Test]
		public void generate_the_file_for_a_class_with_a_single_method()
		{
			var testFiles = SlothParser.RetrieveTestFilesIn(CodeGenerationTestPath);

			SlothGenerator.Generate(testFiles);

			var generatedFile = RetrieveFile(Path.Combine(GeneratedFolderPath, "CodeGeneration"), "ClassWithASingleTestMethod.generated.h");
			File.ReadAllText(generatedFile).Should().EndWithEquivalent(ExpectedCodeFor.AClassWithASingleTestMethod);
		}

		[Test]
		public void generate_the_file_for_a_class_with_multiple_methods()
		{
			var testFiles = SlothParser.RetrieveTestFilesIn(CodeGenerationTestPath);

			SlothGenerator.Generate(testFiles);

			var generatedFile = RetrieveFile(Path.Combine(GeneratedFolderPath, "CodeGeneration"), "ClassWithMultipleTestMethods.generated.h");
			File.ReadAllText(generatedFile).Should().EndWithEquivalent(ExpectedCodeFor.AClassWithMultipleTestMethods);
		}

		[Test]
		public void generate_the_file_for_a_class_with_multiple_classes()
		{
			var testFiles = SlothParser.RetrieveTestFilesIn(CodeGenerationTestPath);

			SlothGenerator.Generate(testFiles);

			var generatedFile = RetrieveFile(Path.Combine(GeneratedFolderPath, "CodeGeneration"), "FileWithMultipleTestClasses.generated.h");
			File.ReadAllText(generatedFile).Should().ContainEquivalentOf(ExpectedCodeFor.AClassInTheMiddleOfMultipleTestClasses);
			File.ReadAllText(generatedFile).Should().EndWithEquivalent(ExpectedCodeFor.AClassInTheEndOfMultipleTestClasses);
		}

		[Test]
		public void generate_the_file_in_an_equivalent_subfolder()
		{
			var testFiles = SlothParser.RetrieveTestFilesIn(CodeGenerationTestPath);

			SlothGenerator.Generate(testFiles);

			File.Exists(Path.Combine(GeneratedFolderPath, "CodeGeneration", "SubFolder", "TestFileInASubFolder.generated.h"))
				.Should().BeTrue();
		}

		[Test]
		public void add_the_generated_files_to_the_included_tests_file()
		{
			var testFiles = SlothParser.RetrieveTestFilesIn(CodeGenerationTestPath);

			SlothGenerator.GenerateIncludedTestsFile();
			SlothGenerator.Generate(testFiles);

			var includedTestsFile = RetrieveFile(GeneratedFolderPath, NameOfThe.IncludedTestsFile);
			File.ReadAllText(includedTestsFile).Contains(@"#include ""CodeGeneration\SubFolder\RetrievedIncludeFile.generated.h""")
											   .Should().BeTrue();
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
