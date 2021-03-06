﻿using FluentAssertions;
using NUnit.Framework;
using SlothUnit.CodeGenerator.Infrastructure;


namespace SlothUnit.CodeGenerator.Test
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
			StringHelper.RemoveTrailingSlashIn(@"Any\Path\").Should().Be(@"Any\Path");
			StringHelper.RemoveTrailingSlashIn(@"Any\Path").Should().Be(@"Any\Path");
		}

		[Test]
		public void remove_double_quotes_to_evaluate_path_with_spaces()
		{
			StringHelper.RemoveSurroundingQuotesIn("D:\\Projects\\Programming\\Cpp\\Installation Test\\Installation Test\"")
						.Should()
						.Be("D:\\Projects\\Programming\\Cpp\\Installation Test\\Installation Test");
		}
	}
}
