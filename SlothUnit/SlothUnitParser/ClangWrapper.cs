﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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

		public List<TestMethod> RetrieveTestMethodsIn(CXCursor classCursor)
		{
			return RetrieveMethodCursorsIn(classCursor)
				  .Select(methodCursor => TestMethod.BuildFrom(this, methodCursor))
				  .Where(Method.IsTestMethod)
				  .ToList();
		}

		public List<TestProperty> RetrieveTestPropertiesIn(CXCursor methodCursor)
		{
			var attributes =  RetrieveAttributeCursorsIn(methodCursor)
							 .Select(attributeCursor => GetCursorName(attributeCursor).Split(','))
							 .Where(properties => properties.Contains("Test"))
							 .ToList();

			if (attributes.Any())
				return attributes.Aggregate((properties, next) => properties.Concat(next).ToArray())
								 .Select(property => new TestProperty(property))
								 .ToList();

			return new List<TestProperty>();
		}

		public List<CXCursor> RetrieveClassCursors()
		{
			var classCursors = new List<CXCursor>();

			CXCursorVisitor classVisitor = (cursor, parent, data) =>
			{
				if (cursor.kind == CXCursorKind.CXCursor_ClassDecl)
				{
					if (ItIsNotAnIncludedClass(cursor))
						classCursors.Add(cursor);
				}

				return CXChildVisitResult.CXChildVisit_Continue;
			};

			clang.visitChildren(Cursor, classVisitor, new CXClientData());
			return classCursors;
		}

		private bool ItIsNotAnIncludedClass(CXCursor classCursor)
		{
			return FilePath == GetCursorFilePath(classCursor);
		}

		public List<CXCursor> RetrieveMethodCursorsIn(CXCursor classCursor)
		{
			var methodCursors = new List<CXCursor>();

			CXCursorVisitor methodVisitor = (cursor, parent, data) =>
			{
				if (cursor.kind == CXCursorKind.CXCursor_CXXMethod)
					methodCursors.Add(cursor);

				return CXChildVisitResult.CXChildVisit_Continue;
			};

			clang.visitChildren(classCursor, methodVisitor, new CXClientData());
			return methodCursors;
		}

		private List<CXCursor> RetrieveAttributeCursorsIn(CXCursor methodCursor)
		{
			var attributeCursors = new List<CXCursor>();

			CXCursorVisitor methodVisitor = (cursor, parent, data) =>
			{
				if (cursor.kind == CXCursorKind.CXCursor_AnnotateAttr)
					attributeCursors.Add(cursor);

				return CXChildVisitResult.CXChildVisit_Continue;
			};

			clang.visitChildren(methodCursor, methodVisitor, new CXClientData());
			return attributeCursors;
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