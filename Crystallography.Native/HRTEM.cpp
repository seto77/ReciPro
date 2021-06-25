// HRTEM.cpp : DLL アプリケーション用にエクスポートされる関数を定義します。
//

#include "stdafx.h"

/*----------------*/
/* HRTEM.cpp */
/*----------------*/
// HRTEM.cpp : DLL アプリケーション用にエクスポートされる関数を定義します。
//

#define HRTEM_EXPORTS

//#include <complex>
#include <stdio.h>
#include <string.h>
#include <vector>
#include "math.h"

#include "HRTEM.h"

using namespace std;

const double two_pi = 2 * 3.141592653589793238462643383279;

extern "C" {

	HRTEM_API void _HRTEMSolverQuasi(
		int gDim,
		int lDim,
		int rDim,
		double gPsi[],
		double gVec[],
		double gLenz[],
		double rVec[],
		double results[])
	{

			for (auto r = 0; r < rDim; ++r) // 前置インクリメントの方が速いらしい
			{
				vector<double>sumReal(lDim);
				vector<double>sumImag(lDim);
				const auto rX = rVec[r * 2] * two_pi, rY = rVec[r * 2 + 1] * two_pi;
				for (auto g = 0; g < gDim; ++g)
				{
					const auto phase = gVec[g * 2] * rX + gVec[g * 2 + 1] * rY;
					const auto real = cos(phase);
					const auto imag = sin(phase);

					const auto fReal = gPsi[g * 2] * real - gPsi[g * 2 + 1] * imag;
					const auto fImag = gPsi[g * 2] * imag + gPsi[g * 2 + 1] * real;

					for (auto l = 0; l < lDim; ++l) {
						sumReal[l] += fReal * gLenz[(g * lDim + l) * 2] - fImag * gLenz[(g * lDim + l) * 2 + 1];
						sumImag[l] += fImag * gLenz[(g * lDim + l) * 2] + fReal * gLenz[(g * lDim + l) * 2 + 1];
					}
				}
				for (auto l = 0; l < lDim; ++l)
					results[l * rDim + r] = sumReal[l] * sumReal[l] + sumImag[l] * sumImag[l];
			}
		
	}

	HRTEM_API void _HRTEMSolverTcc(
		int gDim,
		int lDim,
		int rDim,
		double gPsi[],
		double gVec[],
		double gLenz[],
		double rVec[],
		double results[])
	{
		for (auto r = 0; r < rDim; ++r)
		{
			double real = 0, imag = 0;
			const auto rX = rVec[r * 2] * two_pi, rY = rVec[r * 2 + 1] * two_pi;
			vector<double>sumReal(lDim);
			for (auto g = 0; g < gDim; ++g)
			{
				if (g==0 || gVec[g * 2 - 2] != gVec[g * 2] || gVec[g * 2 - 1] != gVec[g * 2 + 1])//直前のgVecと違うときのみ計算
				{
					const auto phase = gVec[g * 2] * rX + gVec[g * 2 + 1] * rY;
					real = cos(phase);
					imag = sin(phase);
				}

				const auto fReal = gPsi[g * 2] * real - gPsi[g * 2 + 1] * imag;
				const auto fImag = gPsi[g * 2] * imag + gPsi[g * 2 + 1] * real;

				for (auto l = 0; l < lDim; ++l)
					sumReal[l] += fReal * gLenz[(g * lDim + l) * 2] - fImag * gLenz[(g * lDim + l) * 2 + 1];
			}

			for (auto l = 0; l < lDim; ++l)
				results[l * rDim + r] = abs(sumReal[l]);
		}
	}

} // extern "C"