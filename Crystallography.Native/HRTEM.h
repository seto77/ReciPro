/*--------------*/
/* HRTEM.h */
/*--------------*/
#pragma once
using namespace std;

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

	// 260610Cl 追加: 等間隔グリッド版 (行方向の位相回転再帰で sincos を行頭のみに削減)
	HRTEM_API void _HRTEMSolverQuasiGrid(
		int gDim, int lDim, int width, int rowCount,
		double startX, double startY, double stepX, double stepY,
		double gPsi[], double gVec[], double gLenz[], double results[]);

	// 260610Cl 追加
	HRTEM_API void _HRTEMSolverTccGrid(
		int gDim, int lDim, int width, int rowCount,
		double startX, double startY, double stepX, double stepY,
		double gPsi[], double gVec[], double gLenz[], double results[]);

} // extern "C"