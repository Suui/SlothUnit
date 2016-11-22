#pragma once
#include "AssertionException.h"
#include <sstream>

namespace SlothUnit
{
	class Assertion
	{
		bool givenBoolean = false;
		std::string givenString = "";
		int givenInteger = 0;
		float givenFloat = 0.f;

	public:

		explicit Assertion(bool givenBoolean) : givenBoolean(givenBoolean) {}

		explicit Assertion(std::string givenString) : givenString(givenString) {}

		explicit Assertion(int givenInteger) : givenInteger(givenInteger) {}

		explicit Assertion(float givenFloat) : givenFloat(givenFloat) {}

		bool ToBeTrue();

		bool ToBeFalse();

		bool ToBe(std::string expectedString);

		bool ToBe(int expectedInteger);

		bool ToBe(float expectedFloat);
	};

	bool Assertion::ToBeTrue()
	{
		if (givenBoolean) return true;

		throw AssertionException(std::string("Expected true, obtained false"));
	}

	bool Assertion::ToBeFalse()
	{
		if (!givenBoolean) return true;

		throw AssertionException(std::string("Expected false, obtained true"));
	}

	bool Assertion::ToBe(std::string expectedString)
	{
		if (givenString == expectedString) return true;

		std::string exceptionMessage = "Expected \"" + expectedString + "\", obtained \"" + std::string(givenString) + "\"";
		throw AssertionException(exceptionMessage);
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

	inline Assertion Expect(bool givenBoolean) { return Assertion(givenBoolean); }

	inline Assertion Expect(std::string givenString) { return Assertion(givenString); }

	inline Assertion Expect(char* givenString) { return Assertion(std::string(givenString)); }

	inline Assertion Expect(int givenInteger) { return Assertion(givenInteger); }

	inline Assertion Expect(float givenFloat) { return Assertion(givenFloat); }
}
