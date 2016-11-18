#include "Assertion.h"
#include "AssertionException.h"
#include <sstream>

namespace SlothUnit
{
	Assertion::Assertion(int givenNumber) : givenNumber(givenNumber) {}

	bool Assertion::ToBe(int expectedNumber)
	{
		if (givenNumber == expectedNumber) return true;

		std::ostringstream exceptionStream;
		exceptionStream << "Expected " << givenNumber << ", obtained " << expectedNumber;
		throw AssertionException(exceptionStream.str());
	}

	SLOTHUNIT_API Assertion Expect(int givenNumber)
	{
		return Assertion(givenNumber);
	}
}
