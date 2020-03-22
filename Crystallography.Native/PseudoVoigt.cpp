// PseudoVoigt.cpp : DLL アプリケーション用にエクスポートされる関数を定義します。
//

#include "stdafx.h"

/*----------------*/
/* PseudoVoigt.cpp */
/*----------------*/
// PseudoVoigt.cpp : DLL アプリケーション用にエクスポートされる関数を定義します。
//

//#include <complex>
#include "stdafx.h"
#include <stdio.h>
#include <string.h>
#include <vector>
#define PseudoVoigt_EXPORTS
#include "PseudoVoigt.h"

extern "C" {
	const double c = 0.58740105196819947475170563927231;// = 4^(1/3) - 1
	const double ln2 = 0.69314718055994530941723212145818;
	const double piInv = 0.31830988618379067153776752674503;

	PseudoVoigt_API void _PseudoVoigtDiff(double x[], double x0, double y0, double hx, double hy, double t, double eta, double a, double results[])
	{
		double cosine = cos(t), sine = sin(t);
		double hxInv = 1 / hx, hyInv = 1 / hy;
		double hx2Inv = hxInv * hxInv, hy2Inv = hyInv * hyInv;

		double xShift = x[0] - x0, yShift = x[1] - y0; //中心へシフト
		double xRot = xShift * cosine + yShift * sine, yRot = -xShift * sine + yShift * cosine;
		double xRot2hx2Inv = xRot * xRot * hx2Inv, yRot2hy2Inv = yRot * yRot * hy2Inv;
		double x2pY2 = xRot2hx2Inv + yRot2hy2Inv;

		double lo = 1 + c * x2pY2, sqLo = sqrt(lo);
		double l = 1 / (lo * sqLo * 2);
		double g = ln2 * exp(-ln2 * x2pY2);

		double d1 = piInv * hxInv * hyInv;
		double d2 = d1 * (eta * l + (1 - eta) * g);
		double d3 = a * d1 * (3 * eta * l * c / lo + 2 * ln2 * (1 - eta) * g);

		results[0] = d3 * (xRot * cosine * hx2Inv - yRot * sine * hy2Inv);//x0
		results[1] = d3 * (xRot * sine * hx2Inv + yRot * cosine * hy2Inv);//y0
		results[2] = (d3 * xRot2hx2Inv - a * d2) * hxInv;//hx
		results[3] = (d3 * yRot2hy2Inv - a * d2) * hyInv;//hy
		results[4] = d3 * (hy2Inv - hx2Inv) * xRot * yRot;//theta
		results[5] = a * d1 * (l - g); //eta
		results[6] = d2; //a
	}

	PseudoVoigt_API void _PseudoVoigt(double x[], double x0, double y0, double hx, double hy, double t, double eta, double a, double results[])
	{
		double xShift = x[0] - x0, yShift = x[1] - y0; //中心へシフト
		double cosine = cos(t), sine = sin(t);
		double xRot = xShift * cosine + yShift * sine, yRot = -xShift * sine + yShift * cosine;
		double hxInv = 1 / hx, hyInv = 1 / hy;
		double x2plusY2 = xRot * xRot * hxInv * hxInv + yRot * yRot * hyInv * hyInv;
		double g = ln2 * exp(-ln2 * x2plusY2);
		double l = 1 / (2 * (1 + c * x2plusY2) * sqrt(1 + c * x2plusY2));
		results[0] = a * (eta * l + (1 - eta) * g) * piInv * hxInv * hyInv;
	}
} // extern "C"