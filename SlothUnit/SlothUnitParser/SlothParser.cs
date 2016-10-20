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

		private CXCursor CurrentClassCursor { get; set; }
		private CXCursor CurrentMethodCursor { get; set; }

		private List<CXCursor> Classes { get; } = new List<CXCursor>();

		public TestFile TryGetTestFileFrom(string filePath)
		{
			CXUnsavedFile unsavedFile;

			var index = clang.createIndex(0, 0);
			var arguments = new[] { "-x", "c++", "-std=c++11", "-D__SLOTH_UNIT_PARSER__" };

			var translationUnit = clang.createTranslationUnitFromSourceFile(index, filePath, arguments.Length, arguments, 0, out unsavedFile);
			var cursor = clang.getTranslationUnitCursor(translationUnit);

			clang.visitChildren(cursor, PrettyPrint, new CXClientData(IntPtr.Zero));

			var testFile = new TestFile("")
			{
				TestClasses = new TestClasses()
			};
			testFile.TestClasses.Add(new TestClass
			{
				Name = clang.getCursorSpelling(Classes.Single()).ToString()
			});

			clang.disposeTranslationUnit(translationUnit);
			clang.disposeIndex(index);
			return testFile;
		}

		private CXChildVisitResult PrettyPrint(CXCursor cursor, CXCursor parent, IntPtr client_data)
		{
			if (cursor.kind == CXCursorKind.CXCursor_ClassDecl)
			{
				Classes.Add(cursor);
				Console.WriteLine(clang.getCursorSpelling(cursor).ToString());
				clang.visitChildren(cursor, PrintClassChildren, new CXClientData(IntPtr.Zero));
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
}