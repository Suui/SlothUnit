#pragma once
#include "Exports.h"
#include <string>

namespace SlothUnit
{
	class SLOTHUNIT_API Assertion
	{
		bool givenBoolean = false;
		std::string givenString = "";
		int givenInteger = 0;
		float givenFloat = 0.f;

	public:

		explicit Assertion(bool givenBoolean);

		explicit Assertion(std::string givenString);

		explicit Assertion(int givenInteger);

		explicit Assertion(float givenFloat);

		bool ToBeTrue();

		bool ToBeFalse();

		bool ToBe(std::string expectedString);

		bool ToBe(int expectedInteger);

		bool ToBe(float expectedFloat);
	};

	SLOTHUNIT_API Assertion Expect(bool givenBoolean);

	SLOTHUNIT_API Assertion Expect(std::string givenString);

	SLOTHUNIT_API Assertion Expect(char* givenString);

	SLOTHUNIT_API Assertion Expect(int givenInteger);

	SLOTHUNIT_API Assertion Expect(float givenFloat);
}
