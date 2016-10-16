#pragma once

#ifdef SLOTHUNIT_EXPORTS
#define SLOTHUNIT_API __declspec(dllexport)
#else
#define SLOTHUNIT_API __declspec(dllimport)
#endif

namespace SlothUnit
{
	#define Test()

	class SLOTHUNIT_API Assertion
	{
		int givenNumber;

	public:

		explicit Assertion(int givenNumber);

		bool ToBe(int expectedNumber);
	};

	SLOTHUNIT_API Assertion Expect(int givenNumber);
}
