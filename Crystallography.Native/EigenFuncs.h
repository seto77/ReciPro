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
	EIGEN_FUNCS_API void _STEM_TDS(int dim, double B[], double U[], double C_k[], double C_kq[], double result[]);
	EIGEN_FUNCS_API void _PointwiseMultiply(int dim, double mat1[], double mat2[], double result[]);
	EIGEN_FUNCS_API void _AdjointAndMultiply(int dim, double mat1[], double mat2[], double result[]);
	EIGEN_FUNCS_API void _Multiply(int dim, double mat1[], double mat2[], double result[]);
	EIGEN_FUNCS_API void _Inverse(int dim, double mat[], double inverse[]);
	EIGEN_FUNCS_API void _EigenSolver(int dim, double mat[], double eigenValues[], double eigenVectors[]);
	EIGEN_FUNCS_API void _MatrixExponential(int dim, double mat[], double results[]);
	EIGEN_FUNCS_API void _CBEDSolver_Eigen(int gDim, double _potential[], double _phi0[], int tDim, double thickness[], double cosTau, double result[]);
	EIGEN_FUNCS_API void _CBEDSolver_MatExp(int gDim, double _potential[], double _phi0[], int tDim, double tStart, double tStep, double result[]);

} // extern "C"