using System.IO;
using FluentAssertions;
using NUnit.Framework;
using SlothUnit.Parser.Core;
using SlothUnit.Parser.Core.Elements;


namespace SlothUnit.Parser.Test
{
	[TestFixture]
	public class TestMethodShould : FileSystemTest
	{
		[Test]
		public void contain_the_properties_set_in_the_attributes()
		{
			var filePath = Path.Combine(TestProjectDir, "TestMethodShould.h");
			var testFile = new SlothParser().TryGetTestFileFrom(filePath);
			var testMethod = testFile.TestClasses.Single().TestMethods[0];

			testMethod.TestProperties.ToList().Should().Contain(new TestProperty("Test"));
			testMethod.TestProperties.ToList().Should().Contain(new TestProperty("OneProperty"));
			testMethod.TestProperties.ToList().Should().Contain(new TestProperty("TwoProperties"));
			testMethod.TestProperties.ToList().Should().Contain(new TestProperty("ThreeProperties"));
		}
	}
}
