using System;
using System.Collections.Generic;
using System.Linq;
using ClangSharp;


namespace SlothUnitParser
{
	public class SlothParser
	{
		private List<TestFile> TestFiles { get; } = new List<TestFile>();

		public static List<TestFile> RetrieveTestFilesIn(string rootDirectory)
		{
			return new List<TestFile>();
		}

		private CXCursor CurrentMethodCursor { get; set; }

		private List<CXCursor> Classes { get; } = new List<CXCursor>();

		public TestFile TryGetTestFileFrom(string filePath)
		{
			var clangWrapper = new ClangWrapper();
			var cursor = clangWrapper.GetCursorForFile(filePath);
			
			clang.visitChildren(cursor, PrettyPrint, new CXClientData(IntPtr.Zero));

			var testFile = new TestFile("")
			{
				TestClasses = new TestClasses()
			};
			testFile.TestClasses.Add(new TestClass
			{
				Name = clang.getCursorSpelling(Classes.Single()).ToString()
			});

			clangWrapper.Dispose();
			return testFile;
		}

		private CXChildVisitResult PrettyPrint(CXCursor cursor, CXCursor parent, IntPtr client_data)
		{
			if (cursor.kind == CXCursorKind.CXCursor_ClassDecl)
			{
				Classes.Add(cursor);
				Console.WriteLine(clang.getCursorSpelling(cursor).ToString());
			}

			return CXChildVisitResult.CXChildVisit_Continue;
		}

		private CXChildVisitResult PrintClassChildren(CXCursor cursor, CXCursor parent, IntPtr client_data)
		{
			if (cursor.kind == CXCursorKind.CXCursor_CXXMethod)
			{
				CurrentMethodCursor = cursor;
				Console.WriteLine(clang.getCursorSpelling(cursor).ToString());
				clang.visitChildren(cursor, PrintFunctionChildren, new CXClientData(IntPtr.Zero));
			}

			return CXChildVisitResult.CXChildVisit_Continue;
		}

		private CXChildVisitResult PrintFunctionChildren(CXCursor cursor, CXCursor parent, IntPtr client_data)
		{
			if (cursor.kind == CXCursorKind.CXCursor_AnnotateAttr)
			{
				CXFile file;
				uint line, column, offset;
				clang.getExpansionLocation(clang.getCursorLocation(cursor), out file, out line, out column, out offset);
				TestFiles.Add(new TestFile(clang.getFileName(file).ToString()));
				var propertyString = clang.getCursorSpelling(cursor).ToString();
				Console.WriteLine(propertyString);
			}

			return CXChildVisitResult.CXChildVisit_Continue;
		}
	}

	public class ClangWrapper
	{
		private string[] Arguments { get; }= { "-x", "c++", "-std=c++11", "-D__SLOTH_UNIT_PARSER__" };
		private CXIndex Index { get; set; }
		private CXTranslationUnit TranslationUnit { get; set; }

		public CXCursor GetCursorForFile(string filePath)
		{
			CXUnsavedFile unsavedFile;
			Index = clang.createIndex(0, 0);
			TranslationUnit = clang.createTranslationUnitFromSourceFile(Index, filePath, Arguments.Length, Arguments, 0, out unsavedFile);
			return clang.getTranslationUnitCursor(TranslationUnit);
		}

		public void Dispose()
		{
			clang.disposeTranslationUnit(TranslationUnit);
			clang.disposeIndex(Index);
		}
	}
}