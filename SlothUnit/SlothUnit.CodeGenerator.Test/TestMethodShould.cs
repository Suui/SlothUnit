using System.IO;
using FluentAssertions;
using NUnit.Framework;
using SlothUnit.CodeGenerator.Core;
using SlothUnit.CodeGenerator.Core.Elements;
using SlothUnit.CodeGenerator.Test.Helpers;


namespace SlothUnit.CodeGenerator.Test
{
	[TestFixture]
	public class TestMethodShould : FileSystemTest
	{
		[Test]
		public void contain_the_properties_set_in_the_attributes()
		{
			var filePath = Path.Combine(TestProjectPath, "TestMethodShould.h");
			var testFile = new SlothParser().TryGetTestFileFrom(filePath);
			var testMethod = testFile.TestClasses.Single().TestMethods[0];

			testMethod.TestProperties.ToList().Should().Contain(new TestProperty("Test"));
			testMethod.TestProperties.ToList().Should().Contain(new TestProperty("OneProperty"));
			testMethod.TestProperties.ToList().Should().Contain(new TestProperty("TwoProperties"));
			testMethod.TestProperties.ToList().Should().Contain(new TestProperty("ThreeProperties"));
		}
	}
}
