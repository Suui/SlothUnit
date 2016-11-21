#pragma once
#include "../SlothUnit/SlothUnit.h"

using namespace SlothUnit;

class AnotherDisplayShould
{
public:

	Test()
	void not_show_a_passing_string_test()
	{
		Expect(std::string("Oh long johnson")).ToBe("Oh long johnson");
		Expect("Upps I dropped").ToBe("Upps I dropped");
	}

	Test()
	void show_a_failing_string_test()
	{
		Expect(std::string("Og lonh johnson")).ToBe("Oh long johnson");
	}

	Test()
	void show_another_failing_string_test()
	{
		Expect("Ups I droped").ToBe("Upps I dropped");
	}

	Test()
	void not_show_a_passing_float_test()
	{
		Expect(1000.f).ToBe(1000.f);
		
		// Default tolerance is epsilon
		Expect(1000.f + 0.00001f).ToBe(1000.f);
	}

	Test()
	void show_a_failing_float_test()
	{
		// Default tolerance is epsilon
		Expect(1000.f + 0.0001f).ToBe(1000.f);
	}
};