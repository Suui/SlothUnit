﻿using System;
using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using SlothUnit.Parser.Core;


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

			var expectedMainFile = RetrieveExpectedFile(mainFileName);
			var generatedFile = RetrieveGeneratedFile(mainFileName);
			File.ReadAllText(generatedFile).Should().Be(File.ReadAllText(expectedMainFile));
		}

		[Test]
		public void generate_the_included_tests_file()
		{
			var slothGenerator = new SlothGenerator(TestProjectDir);

			slothGenerator.GenerateFolder();
			slothGenerator.GenerateIncludedTestsFile();

			var generatedFile = RetrieveGeneratedFile("__Tests__.generated.h");
			File.ReadAllText(generatedFile).Should().Be("#pragma once");
		}

		private static string RetrieveExpectedFile(string mainFileName)
		{
			var slothUnitTestDir = Path.Combine(SolutionDir, "SlothUnit.Parser.Test");
			return Directory.GetFiles(slothUnitTestDir)
							.Single(path => path.Equals(Path.Combine(slothUnitTestDir, mainFileName)));
		}

		private string RetrieveGeneratedFile(string fileName)
		{
			return Directory.GetFiles(GeneratedFolderPath)
							.Single(name => name.Equals(Path.Combine(GeneratedFolderPath, fileName)));
		}
	}
}
