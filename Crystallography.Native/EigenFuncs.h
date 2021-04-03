/*--------------*/
/* EigenFuncs.h */
/*--------------*/
#pragma once

using namespace Eigen;
using namespace std;

extern "C" {
#ifdef EIGENFUNCS_EXPORTS
#define EIGEN_FUNCS_API __declspec(dllexport)
#else
#define EIGEN_FUNCS_API __declspec(dllimport)
#endif
	//EIGEN_FUNCS_API void _InverseMat_FullPivLU(int dim, float a[], float ans[]);

	EIGEN_FUNCS_API std::complex<double>* _Inverse(
		int dim, 
		std::complex<double>* mat);
	
	EIGEN_FUNCS_API std::complex<double>** _EigenSolver(
		int dim, 
		complex<double>* mat);// , double eigenValues[], double eigenVectors[]);
	
	EIGEN_FUNCS_API std::complex<double>* _MatrixExponential(
		int dim, 
		complex<double>* mat);
	
	EIGEN_FUNCS_API std::complex<double>*  _CBEDSolver_Eigen(
		int gDim, 
		std::complex<double>* _potential, 
		std::complex<double>* _phi0, 
		int tDim, 
		double thickness[], 
		double cosTau);
	
	EIGEN_FUNCS_API std::complex<double>* _CBEDSolver_MatExp(
		int gDim, 
		std::complex<double>* _potential, 
		std::complex<double>* _phi0, 
		int tDim, 
		double tStart, 
		double tStep);

} // extern "C"