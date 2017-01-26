using System;
using System.Collections.Generic;
using System.Linq;
using SlothUnit.CodeGenerator.Core.Elements;
using SlothUnit.CodeGenerator.Infrastructure;


namespace SlothUnit.CodeGenerator.Core.Collections
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

		public string GeneratedCode(string filePath)
		{
			var generatedCode = "";
			Classes.ForEach(@class =>
			{
				var testMethods = @class.TestMethods.GeneratedCode(@class.Name);
				generatedCode +=
$@"auto registrar_{RegistrarId.Next()} = TestRegistrar(TestClass<{@class.Name}>
(
	""{StringHelper.CppPathFor(filePath)}"",
	""{@class.Name}"",
	{@class.Name}(),
	{{
		{testMethods}
	}}
));

";
			});
			return generatedCode.Substring(0, generatedCode.LastIndexOf('\r'));
		}
	}
}