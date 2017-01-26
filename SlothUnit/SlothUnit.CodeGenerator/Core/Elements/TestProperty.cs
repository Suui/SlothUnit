namespace SlothUnit.CodeGenerator.Core.Elements
{
	public class TestProperty
	{
		public string Value { get; }

		public TestProperty(string property)
		{
			Value = property;
		}

		protected bool Equals(TestProperty other)
		{
			return string.Equals(Value, other.Value);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((TestProperty) obj);
		}

		public override int GetHashCode()
		{
			return (Value != null ? Value.GetHashCode() : 0);
		}
	}
}