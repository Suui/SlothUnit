﻿using FluentAssertions;
using NUnit.Framework;
using SlothUnit.Parser.Infrastructure;


namespace SlothUnit.Parser.Test
{
	[TestFixture]
	public class StringHelperShould
	{
		[Test]
		public void add_a_slash_for_every_slash_in_a_string_to_write_a_cpp_path()
		{
			StringHelper.CppPathFor(@"Csharp\Beautiful\Path").Should().Be(@"Csharp\\Beautiful\\Path");
		}
	}
}
