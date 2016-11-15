using FluentAssertions;
using NUnit.Framework;
using SlothUnit.Parser.Infrastructure;


namespace SlothUnit.Parser.Test
{
	[TestFixture]
	public class RegistrarIdShould
	{
		[Test]
		public void return_the_next_id()
		{
			RegistrarId.Next().Should().Be(1);
			RegistrarId.Next().Should().Be(2);
			RegistrarId.Next().Should().Be(3);
		}
	}
}
