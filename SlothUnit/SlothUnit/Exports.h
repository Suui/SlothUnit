#pragma once

#ifdef SLOTHUNIT_EXPORTS
#define SLOTHUNIT_API __declspec(dllexport)
#else
#define SLOTHUNIT_API __declspec(dllimport)
#endif
