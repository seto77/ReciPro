/*--------------*/
/* EigenFuncs.h */
/*--------------*/
#pragma once

extern "C" {
#ifdef EIGENFUNCS_EXPORTS
#define EIGEN_FUNCS_API __declspec(dllexport)
#else
#define EIGEN_FUNCS_API __declspec(dllimport)
#endif
	//EIGEN_FUNCS_API void _InverseMat_FullPivLU(int dim, float a[], float ans[]);

	EIGEN_FUNCS_API void _Inverse(int dim, double mat[], double inverse[]);
	EIGEN_FUNCS_API void _EigenSolver(int dim, double mat[], double eigenValues[], double eigenVectors[]);
	EIGEN_FUNCS_API void _CBEDSolver(int gDim, double _potential[], double _phi0[], int tDim, double thickness[], double cosTau, double result[]);



} // extern "C"