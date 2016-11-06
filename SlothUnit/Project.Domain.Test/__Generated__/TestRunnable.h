#pragma once
#include "../../SlothUnit/SlothUnit.h"
#include <string>
#include <unordered_map>

class TestRunnable
{
protected:

	std::string path;
	std::string name;
	std::unordered_map<std::string, SlothUnit::TestFunction> testFunctions;

public:

	virtual ~TestRunnable() {}

	virtual void Run() = 0;

	virtual std::string Path() = 0;

	virtual std::string Name() = 0;
};
