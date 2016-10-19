#include "Assertion.h"
#include <iostream>

namespace SlothUnit
{
	Assertion::Assertion(int givenNumber) : givenNumber(givenNumber)
	{
	}

	bool Assertion::ToBe(int expectedNumber)
	{
		std::cout << "Expected " << expectedNumber << ", obtained " << givenNumber << std::endl;
		return givenNumber == expectedNumber ? true : false;
	}

	SLOTHUNIT_API Assertion Expect(int givenNumber)
	{
		return Assertion(givenNumber);
	}
}