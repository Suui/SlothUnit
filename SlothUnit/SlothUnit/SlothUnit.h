#pragma once

#include "TestRunnable.h"
#include "TestClass.h"
#include "SlothTests.h"
#include "TestRegistrar.h"
#include "Assertion.h"

namespace SlothUnit
{
	#define Test(...) Attribute(Test, __VA_ARGS__)

#if defined(__SLOTH_UNIT_PARSER__)

	#define Attribute(...) __attribute__((annotate(#__VA_ARGS__)))

#else

	#define Attribute(...)

#endif
}
