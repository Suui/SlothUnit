﻿using System.IO;
using FluentAssertions;
using NUnit.Framework;
using SlothUnit.CodeGenerator.Core;
using SlothUnit.CodeGenerator.Core.Elements;
using SlothUnit.CodeGenerator.Test.Helpers;


namespace SlothUnit.CodeGenerator.Test
{
	[TestFixture]
	public class TestFileShould : FileSystemTest
	{
		private const string FileName = "TestClassShould.h";
		private string FilePath;
		private TestFile TestFile;

		[SetUp]
		public void given_a_test_file()
		{
			FilePath = Path.Combine(TestProjectPath, FileName);
			TestFile = new SlothParser().TryGetTestFileFrom(FilePath);
		}

		[Test]
		public void be_retrieved_from_file_path()
		{
			TestFile.Name.Should().Be(FileName);
			TestFile.Path.Should().Be(FilePath);
		}

		[Test]
		public void only_retrieve_classes_with_test_methods_and_ignore_the_included_ones()
		{
			var testClass = TestFile.TestClasses.Single();

			testClass.Name.Should().Be("TestClassShould");
			testClass.Path.Should().Be(FilePath);
			testClass.Line.Should().Be(6);
		}
	}
}
