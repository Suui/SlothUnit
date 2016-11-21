#pragma once
#include <stdexcept>

struct AssertionException : std::runtime_error
{
	explicit AssertionException(const std::string& exceptionMessage) : runtime_error(exceptionMessage) {}
};
