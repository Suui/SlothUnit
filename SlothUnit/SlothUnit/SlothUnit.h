#pragma once

#ifdef SLOTHUNIT_EXPORTS
#define SLOTHUNIT_API __declspec(dllexport)
#else
#define SLOTHUNIT_API __declspec(dllimport)
#endif

namespace SlothUnit
{
	#define TestClass(...) Attribute(TEST_CLASS_MUTHAFUCKA, __VA_ARGS__)
	#define Test(...) Attribute(Test, __VA_ARGS__)

#if defined(__SLOTH_UNIT_PARSER__)

	#define Attribute(...) __attribute__((annotate(#__VA_ARGS__)))

#else

	#define Attribute(...)

#endif

	class SLOTHUNIT_API Assertion
	{
		int givenNumber;

	public:

		explicit Assertion(int givenNumber);

		bool ToBe(int expectedNumber);
	};

	SLOTHUNIT_API Assertion Expect(int givenNumber);
}
