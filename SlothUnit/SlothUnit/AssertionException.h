#pragma once
#include <stdexcept>

class AssertionException : public std::runtime_error
{
public:

	explicit AssertionException(std::string exceptionMessage) : runtime_error(exceptionMessage) {}
};
