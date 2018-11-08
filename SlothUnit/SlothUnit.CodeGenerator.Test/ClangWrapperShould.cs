using System.IO;
using System.Linq;
using ClangSharp;
using FluentAssertions;
using NUnit.Framework;
using SlothUnit.CodeGenerator.Core;
using SlothUnit.CodeGenerator.Test.Helpers;


namespace SlothUnit.CodeGenerator.Test
{
	[TestFixture]
	class ClangWrapperShould : FileSystemTest
	{
		private ClangWrapper ClangWrapper;
		private CXCursor ClassCursor;
		private string FilePath;

		[SetUp]
		public void given_a_class_cursor_in_a_file()
		{
			FilePath = Path.Combine(TestProjectPath, "ClangWrapperShould.h");
			ClangWrapper = ClangWrapper.For(FilePath);
			ClassCursor = ClangWrapper.RetrieveClassCursors().Single();
		}

		[Test]
		public void retrieve_the_filepath_for_the_file_a_cursor_is_in()
		{
			ClangWrapper.GetCursorFilePath(ClassCursor).Should().Be(FilePath);
		}

		[Test]
		public void retrieve_the_name_of_a_cursor()
		{
			ClangWrapper.GetCursorName(ClassCursor).Should().Be("ClangWrapperShould");
		}

		[Test]
		public void retrieve_the_line_of_a_cursor()
		{
			ClangWrapper.GetCursorLine(ClassCursor).Should().Be(6);
		}

	    [Test]
	    public void retrieve_a_methods_return_type()
	    {
	        var methodCursor = ClangWrapper.RetrieveMethodCursorsIn(ClassCursor).Single();

            ClangWrapper.GetMethodCursorReturnType(methodCursor).Should().Be("int");
	        ClangWrapper.GetCursorName(methodCursor).Should().Be("a_method_with_a_return_type_int");
	    }
	}
}
