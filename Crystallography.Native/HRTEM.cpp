// HRTEM.cpp : DLL �A�v���P�[�V�����p�ɃG�N�X�|�[�g�����֐����`���܂��B
//

#include "stdafx.h"

/*----------------*/
/* HRTEM.cpp */
/*----------------*/
// HRTEM.cpp : DLL �A�v���P�[�V�����p�ɃG�N�X�|�[�g�����֐����`���܂��B
//

#define HRTEM_EXPORTS

//#include <complex>
#include <stdio.h>
#include <string.h>
#include <vector>
#include <complex>   // 260610Cl 追加 (grid 版の位相回転再帰用)
#include <algorithm> // 260610Cl 追加 (std::fill)
#include "math.h"

#include "HRTEM.h"

using namespace std;

const double two_pi = 2 * 3.141592653589793238462643383279;
constexpr int PHASE_REANCHOR = 256; // 260610Cl 追加: 行方向の位相回転再帰でこの画素数ごとに Exp で再アンカー (位相ドリフト ~256·eps 抑制)

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

			for (auto r = 0; r < rDim; ++r) // �O�u�C���N�������g�̕��������炵��
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
				if (g==0 || gVec[g * 2 - 2] != gVec[g * 2] || gVec[g * 2 - 1] != gVec[g * 2 + 1])//���O��gVec�ƈႤ�Ƃ��̂݌v�Z
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

	// 260610Cl 追加 (Phase2 候補B-native): 等間隔グリッド版 Quasi-coherent solver。
	// 旧 _HRTEMSolverQuasi は画素ごとの座標配列を受け取り、画素×ビームごとに cos/sin を計算し、
	// 画素ごとに vector<double> を2本 heap 確保していた。等間隔グリッド前提 (始点+刻み) を受け取り、
	// 行方向は位相回転再帰 (256 画素ごとに再アンカー、ドリフト ~1e-13) で sincos を行頭のみに削減。
	// 画素 (x, row) の座標は (startX + x*stepX, startY + row*stepY)。
	// results のレイアウトは results[l*rowCount*width + row*width + x] (旧版と同じ l-major)。
	HRTEM_API void _HRTEMSolverQuasiGrid(
		int gDim, int lDim, int width, int rowCount,
		double startX, double startY, double stepX, double stepY,
		double gPsi[], double gVec[], double gLenz[], double results[])
	{
		vector<complex<double>> phasor(gDim), step(gDim);
		vector<double> sumReal(lDim), sumImag(lDim);
		for (auto g = 0; g < gDim; ++g)
			step[g] = polar(1.0, two_pi * gVec[g * 2] * stepX);//x が 1 画素進むときの位相回転

		for (auto row = 0; row < rowCount; ++row)
		{
			const auto rY = (startY + row * stepY) * two_pi;
			for (auto x = 0; x < width; ++x)
			{
				if (x % PHASE_REANCHOR == 0)//行頭と PHASE_REANCHOR 画素ごとに位相を再アンカー (ドリフト抑制)
				{
					const auto rX = (startX + x * stepX) * two_pi;
					for (auto g = 0; g < gDim; ++g)
						phasor[g] = polar(1.0, gVec[g * 2] * rX + gVec[g * 2 + 1] * rY);
				}
				fill(sumReal.begin(), sumReal.end(), 0.0);
				fill(sumImag.begin(), sumImag.end(), 0.0);
				for (auto g = 0; g < gDim; ++g)
				{
					const auto real = phasor[g].real();
					const auto imag = phasor[g].imag();
					const auto fReal = gPsi[g * 2] * real - gPsi[g * 2 + 1] * imag;
					const auto fImag = gPsi[g * 2] * imag + gPsi[g * 2 + 1] * real;
					for (auto l = 0; l < lDim; ++l)
					{
						sumReal[l] += fReal * gLenz[(g * lDim + l) * 2] - fImag * gLenz[(g * lDim + l) * 2 + 1];
						sumImag[l] += fImag * gLenz[(g * lDim + l) * 2] + fReal * gLenz[(g * lDim + l) * 2 + 1];
					}
					phasor[g] *= step[g];//次の x へ位相を回転
				}
				const auto r = row * width + x;
				for (auto l = 0; l < lDim; ++l)
					results[(size_t)l * rowCount * width + r] = sumReal[l] * sumReal[l] + sumImag[l] * sumImag[l];
			}
		}
	}

	// 260610Cl 追加 (Phase2 候補B-native): 等間隔グリッド版 Transmission cross coefficient solver。
	// 旧 _HRTEMSolverTcc の「直前と同じ gVec のとき cos/sin を再利用する」最適化は、位相回転再帰では
	// エントリごとの phasor 配列に置き換わる (重複エントリも乗算1回ずつ、数学的には同一)。
	HRTEM_API void _HRTEMSolverTccGrid(
		int gDim, int lDim, int width, int rowCount,
		double startX, double startY, double stepX, double stepY,
		double gPsi[], double gVec[], double gLenz[], double results[])
	{
		vector<complex<double>> phasor(gDim), step(gDim);
		vector<double> sumReal(lDim);
		for (auto g = 0; g < gDim; ++g)
			step[g] = polar(1.0, two_pi * gVec[g * 2] * stepX);

		for (auto row = 0; row < rowCount; ++row)
		{
			const auto rY = (startY + row * stepY) * two_pi;
			for (auto x = 0; x < width; ++x)
			{
				if (x % PHASE_REANCHOR == 0)
				{
					const auto rX = (startX + x * stepX) * two_pi;
					for (auto g = 0; g < gDim; ++g)
						phasor[g] = polar(1.0, gVec[g * 2] * rX + gVec[g * 2 + 1] * rY);
				}
				fill(sumReal.begin(), sumReal.end(), 0.0);
				for (auto g = 0; g < gDim; ++g)
				{
					const auto real = phasor[g].real();
					const auto imag = phasor[g].imag();
					const auto fReal = gPsi[g * 2] * real - gPsi[g * 2 + 1] * imag;
					const auto fImag = gPsi[g * 2] * imag + gPsi[g * 2 + 1] * real;
					for (auto l = 0; l < lDim; ++l)
						sumReal[l] += fReal * gLenz[(g * lDim + l) * 2] - fImag * gLenz[(g * lDim + l) * 2 + 1];
					phasor[g] *= step[g];
				}
				const auto r = row * width + x;
				for (auto l = 0; l < lDim; ++l)
					results[(size_t)l * rowCount * width + r] = abs(sumReal[l]);
			}
		}
	}

} // extern "C"