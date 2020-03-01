/*--------------*/
/* HRTEM.h */
/*--------------*/
#pragma once

extern "C" {
#ifdef HRTEM_EXPORTS
#define HRTEM_API __declspec(dllexport)
#else
#define HRTEM_API __declspec(dllimport)
#endif
	HRTEM_API void _HRTEMSolverQuasi(
		int gDim,
		int lDim,
		int rDim,
		double gPsi[],
		double gVec[],
		double gLenz[],
		double rVec[],
		double results[]);

	HRTEM_API void _HRTEMSolverTcc(
		int gDim,
		int lDim,
		int rDim,
		double gPsi[],
		double gVec[],
		double gLenz[],
		double rVec[],
		double results[]);

} // extern "C"