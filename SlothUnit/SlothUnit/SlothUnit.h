#pragma once

#include "Exports.h"
#include "Assertion.h"
#include <functional>

namespace SlothUnit
{
	typedef std::function<void()> TestFunction;
	#define Test(...) Attribute(Test, __VA_ARGS__)

#if defined(__SLOTH_UNIT_PARSER__)

	#define Attribute(...) __attribute__((annotate(#__VA_ARGS__)))

#else

	#define Attribute(...)

#endif
}
