using FluentAssertions;
using NUnit.Framework;
using SlothUnit.CodeGenerator.Infrastructure;


namespace SlothUnit.CodeGenerator.Test
{
	[TestFixture]
	public class RegistrarIdShould
	{
		[Test]
		public void return_the_next_id()
		{
			var id = RegistrarId.Next();
			RegistrarId.Next().Should().Be(id + 1);
			RegistrarId.Next().Should().Be(id + 2);
		}
	}
}
