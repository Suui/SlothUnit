using System;
using System.Collections.Generic;
using System.Linq;
using SlothUnit.Parser.Core.Elements;


namespace SlothUnit.Parser.Core.Collections
{
	public class TestClasses
	{
		private List<TestClass> Classes { get; }
		public int Count => Classes.Count;

		public TestClasses(List<TestClass> testClasses)
		{
			Classes = testClasses;
		}

		public bool Any() => Classes.Any();

		public TestClass Single() => Classes.Single();

		public void Add(TestClass testClass) => Classes.Add(testClass);

		public void ForEach(Action<TestClass> action) => Classes.ForEach(action);
	}
}