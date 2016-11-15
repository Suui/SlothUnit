using FluentAssertions;
using NUnit.Framework;
using SlothUnit.Parser.Infrastructure;


namespace SlothUnit.Parser.Test
{
	[TestFixture]
	public class StringHelperShould
	{
		[Test]
		public void retrieve_a_file_name_from_a_path()
		{
			StringHelper.GetFileNameFrom(@"AnyPath\ToMy\FileName").Should().Be("FileName");
		}

		[Test]
		public void add_a_slash_for_every_slash_in_a_string_to_write_a_cpp_path()
		{
			StringHelper.CppPathFor(@"Csharp\Beautiful\Path").Should().Be(@"Csharp\\Beautiful\\Path");
		}

		[Test]
		public void remove_trailing_slash_of_a_given_path()
		{
			StringHelper.RemoveTrailingSlashFor(@"Any\Path\").Should().Be(@"Any\Path");
			StringHelper.RemoveTrailingSlashFor(@"Any\Path").Should().Be(@"Any\Path");
		}
	}
}
