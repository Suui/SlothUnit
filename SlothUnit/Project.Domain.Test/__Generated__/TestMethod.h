#pragma once
#include <string>

class TestMethod
{
public:

	std::string Name;

	explicit TestMethod(const std::string& Name) : Name(Name) {}
};
