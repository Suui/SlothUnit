#pragma once

#include "Exports.h"

namespace SlothUnit
{
	class SLOTHUNIT_API Assertion
	{
		int givenNumber;

	public:

		explicit Assertion(int givenNumber);

		bool ToBe(int expectedNumber);
	};

	SLOTHUNIT_API Assertion Expect(int givenNumber);
}
