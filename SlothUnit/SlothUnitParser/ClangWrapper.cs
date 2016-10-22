using System;
using System.Collections.Generic;
using System.Linq;
using ClangSharp;


namespace SlothUnitParser
{
	public class ClangWrapper
	{
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

			return new ClangWrapper(index, translationUnit, cursor);
		}

		public ClangWrapper(CXIndex index, CXTranslationUnit translationUnit, CXCursor cursor)
		{
			Index = index;
			TranslationUnit = translationUnit;
			Cursor = cursor;
		}

		public static List<TestClass> GetTestClassesIn(string filePath)
		{
			var clangWrapper = For(filePath);

			var classesInFile = clangWrapper.RetrieveClassCursors()
											.Select(classCursor => TestClass.BuildFrom(classCursor, clangWrapper))
											.Where(Class.IsTestClass)
											.ToList();
			clangWrapper.Dispose();
			return classesInFile;
		}

		public List<CXCursor> RetrieveClassCursors()
		{
			var classCursors = new List<CXCursor>();

			CXCursorVisitor visitor = (cursor, parent, data) =>
			{
				if (cursor.kind == CXCursorKind.CXCursor_ClassDecl)
					classCursors.Add(cursor);

				return CXChildVisitResult.CXChildVisit_Continue;
			};

			clang.visitChildren(Cursor, visitor, new CXClientData());
			return classCursors;
		}

		public List<CXCursor> GetTestMethodsIn(CXCursor cxCursor)
		{
			var testMethodCursors = new List<CXCursor>();

			CXCursorVisitor visitor = (cursor, parent, data) =>
			{
				if (cursor.kind == CXCursorKind.CXCursor_CXXMethod)
				{
					CXCursorVisitor methodVisitor = (cursorX, parentX, dataX) =>
					{
						if (cursorX.kind == CXCursorKind.CXCursor_AnnotateAttr)
						{
							var properties = GetCursorName(cursorX).Split(',').ToList();

							if (properties.Contains("Test"))
								testMethodCursors.Add(cursorX);
						}

						return CXChildVisitResult.CXChildVisit_Continue;
					};

					clang.visitChildren(cursor, methodVisitor, new CXClientData());
				}

				return CXChildVisitResult.CXChildVisit_Continue;
			};

			clang.visitChildren(cxCursor, visitor, new CXClientData());
			return testMethodCursors;
		}

		private void Dispose()
		{
			clang.disposeTranslationUnit(TranslationUnit);
			clang.disposeIndex(Index);
		}

		public static string GetCursorName(CXCursor cursor)
		{
			return clang.getCursorSpelling(cursor).ToString();
		}

		public static int GetCursorLine(CXCursor cursor)
		{
			CXFile file;
			uint line, column, offset;
			clang.getExpansionLocation(clang.getCursorLocation(cursor), out file, out line, out column, out offset);
			return Convert.ToInt32(line);
		}

		public static string GetCursorFilePath(CXCursor cursor)
		{
			CXFile file;
			uint line, column, offset;
			clang.getExpansionLocation(clang.getCursorLocation(cursor), out file, out line, out column, out offset);
			return clang.getFileName(file).ToString();
		}
	}
}