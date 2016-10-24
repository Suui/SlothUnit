using System;
using System.Collections.Generic;
using System.Linq;
using ClangSharp;


namespace SlothUnitParser
{
	public class ClangWrapper
	{
		public string FilePath { get; set; }
		private CXIndex Index { get; }
		private CXTranslationUnit TranslationUnit { get; }
		private CXCursor Cursor { get; }

		public static ClangWrapper For(string filePath)
		{
			var arguments = new[] { "-x", "c++", "-std=c++11", "-D__SLOTH_UNIT_PARSER__" };
			CXUnsavedFile unsavedFile;
			var index = clang.createIndex(0, 0);
			var translationUnit = clang.createTranslationUnitFromSourceFile(index, filePath, arguments.Length, arguments, 0, out unsavedFile);
			var cursor = clang.getTranslationUnitCursor(translationUnit);

			return new ClangWrapper(filePath, index, translationUnit, cursor);
		}

		public ClangWrapper(string filePath, CXIndex index, CXTranslationUnit translationUnit, CXCursor cursor)
		{
			FilePath = filePath;
			Index = index;
			TranslationUnit = translationUnit;
			Cursor = cursor;
		}

		public List<TestClass> RetrieveTestClasses()
		{
			return RetrieveClassCursors()
				  .Select(classCursor => TestClass.BuildFrom(this, classCursor))
				  .Where(Class.IsTestClass)
				  .ToList();
		}

		public List<CXCursor> RetrieveClassCursors()
		{
			var classCursors = new List<CXCursor>();

			CXCursorVisitor classVisitor = (cursor, parent, data) =>
			{
				if (cursor.kind == CXCursorKind.CXCursor_ClassDecl)
				{
					if (ItIsAnIncludedClass(cursor))
						classCursors.Add(cursor);
				}

				return CXChildVisitResult.CXChildVisit_Continue;
			};

			clang.visitChildren(Cursor, classVisitor, new CXClientData());
			return classCursors;
		}

		private bool ItIsAnIncludedClass(CXCursor classCursor)
		{
			return FilePath == GetCursorFilePath(classCursor);
		}

		public List<CXCursor> RetrieveTestMethodsIn(CXCursor classCursor)
		{
			var testMethodCursors = new List<CXCursor>();

			CXCursorVisitor methodVisitor = (cursor, parent, data) =>
			{
				if (cursor.kind == CXCursorKind.CXCursor_CXXMethod)
				{
					if (ItIsATestMethod(cursor))
						testMethodCursors.Add(cursor);
				}

				return CXChildVisitResult.CXChildVisit_Continue;
			};

			clang.visitChildren(classCursor, methodVisitor, new CXClientData());
			return testMethodCursors;
		}

		private bool ItIsATestMethod(CXCursor methodCursor)
		{
			var isTestMethod = false;

			CXCursorVisitor methodVisitor = (cursor, parent, data) =>
			{
				if (cursor.kind == CXCursorKind.CXCursor_AnnotateAttr)
				{
					var properties = GetCursorName(cursor).Split(',').ToList();
					if (properties.Contains("Test"))
						isTestMethod = true;
				}

				return CXChildVisitResult.CXChildVisit_Continue;
			};

			clang.visitChildren(methodCursor, methodVisitor, new CXClientData());
			return isTestMethod;
		}

		public void Dispose()
		{
			clang.disposeTranslationUnit(TranslationUnit);
			clang.disposeIndex(Index);
		}

		public string GetCursorName(CXCursor cursor)
		{
			return clang.getCursorSpelling(cursor).ToString();
		}

		public int GetCursorLine(CXCursor cursor)
		{
			CXFile file;
			uint line, column, offset;
			clang.getExpansionLocation(clang.getCursorLocation(cursor), out file, out line, out column, out offset);
			return Convert.ToInt32(line);
		}

		public string GetCursorFilePath(CXCursor cursor)
		{
			CXFile file;
			uint line, column, offset;
			clang.getExpansionLocation(clang.getCursorLocation(cursor), out file, out line, out column, out offset);
			return clang.getFileName(file).ToString();
		}
	}
}