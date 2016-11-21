#include "Assertion.h"
#include "AssertionException.h"
#include <sstream>
#include <iostream>

namespace SlothUnit
{
	Assertion::Assertion(bool givenBoolean) : givenBoolean(givenBoolean) {}

	Assertion::Assertion(std::string givenString) : givenString(givenString) {}

	Assertion::Assertion(int givenInteger) : givenInteger(givenInteger) {}

	Assertion::Assertion(float givenFloat) : givenFloat(givenFloat) {}

	bool Assertion::ToBeTrue()
	{
		if (givenBoolean) return true;

		throw AssertionException("Expected true, obtained false");
	}

	bool Assertion::ToBeFalse()
	{
		if (!givenBoolean) return true;

		throw AssertionException("Expected false, obtained true");
	}

	bool Assertion::ToBe(std::string expectedString)
	{
		if (givenString == expectedString) return true;

		throw AssertionException("Expected \"" + expectedString + "\", obtained \"" + givenString + "\"");
	}

	bool Assertion::ToBe(int expectedInteger)
	{
		if (givenInteger == expectedInteger) return true;

		std::ostringstream exceptionStream;
		exceptionStream << "Expected " << expectedInteger << ", obtained " << givenInteger;
		throw AssertionException(exceptionStream.str());
	}

	bool Assertion::ToBe(float expectedFloat)
	{
		if (abs(givenFloat - expectedFloat) < std::numeric_limits<float>::epsilon()) return true;

		std::ostringstream exceptionStream;
		exceptionStream.precision(20);
		exceptionStream << "Expected " << expectedFloat << ", obtained " << givenFloat;
		throw AssertionException(exceptionStream.str());
	}

	Assertion Expect(bool givenBoolean)
	{
		return Assertion(givenBoolean);
	}

	Assertion Expect(std::string givenString)
	{
		return Assertion(givenString);
	}

	Assertion Expect(char* givenString)
	{
		return Assertion(std::string(givenString));
	}

	Assertion Expect(int givenInteger)
	{
		return Assertion(givenInteger);
	}

	Assertion Expect(float givenFloat)
	{
		return Assertion(givenFloat);
	}
}
