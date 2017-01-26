namespace SlothUnit.CodeGenerator.Infrastructure
{
	public static class RegistrarId
	{
		private static long Id;

		public static long Next()
		{
			Id += 1;
			return Id;
		}
	}
}