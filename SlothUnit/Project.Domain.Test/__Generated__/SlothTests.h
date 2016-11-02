#pragma once
#include <vector>
#include "TestClass.h"

#define DeclareTestClass(className) #className()

class SlothTests
{
public:

	std::vector<TestClass> TestClasses;

	static void Run();
};
