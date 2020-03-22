/*--------------*/
/* PseudoVoigt.h */
/*--------------*/
#pragma once

extern "C" {
#ifdef PseudoVoigt_EXPORTS
#define PseudoVoigt_API __declspec(dllexport)
#else
#define PseudoVoigt_API __declspec(dllimport)
#endif
	PseudoVoigt_API void _PseudoVoigtDiff(double x[], double x0, double y0, double hx, double hy, double t, double eta, double a, double results[]);
	PseudoVoigt_API void _PseudoVoigt(double x[], double x0, double y0, double hx, double hy, double t, double eta, double a, double results[]);
} // extern "C"