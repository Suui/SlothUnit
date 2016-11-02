#pragma once
#include <vector>
#include "TestMethod.h"

class TestClass
{
public:

	std::string Path;
	std::string Name;
	std::vector<TestMethod> TestMethods;


	TestClass(const std::string& Path, const std::string& Name, const std::vector<TestMethod>& TestMethods)
		: Path(Path),
		  Name(Name),
		  TestMethods(TestMethods) {}
};
