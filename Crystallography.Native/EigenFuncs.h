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
	EIGEN_FUNCS_API void _Blend(int dim, double c0[], double c1[], double c2[], double c3[], double r0, double r1, double r2, double r3, double result[]);
	EIGEN_FUNCS_API void _BlendAndConjugate(int dim, double c0[], double c1[], double c2[], double c3[], double r0, double r1, double r2, double r3, double result[]);

	EIGEN_FUNCS_API void _AdJointMul_Mul_Mul(int dim, double U[], double mat1[], double mat2[], double mat3[]);
	EIGEN_FUNCS_API void _BlendAdJointMul_Mul_Mul(int dim, double c0[], double c1[], double c2[], double c3[], double r0, double r1, double r2, double r3, double mat2[], double mat3[], double result[]);

	EIGEN_FUNCS_API void _PointwiseMultiply(int dim, double mat1[], double mat2[], double result[]);
	EIGEN_FUNCS_API void _AdjointAndMultiply(int dim, double mat1[], double mat2[], double result[]);
	
	EIGEN_FUNCS_API void _MultiplyMM(int dim, double mat1[], double mat2[], double result[]);
	EIGEN_FUNCS_API void _MultiplyMMM(int dim, double mat1[], double mat2[], double mat3[], double result[]);
	EIGEN_FUNCS_API void _MultiplyMV(int dim, double mat[], double vec[], double result[]);
	EIGEN_FUNCS_API void _MultiplyVV(int dim, double vec1[], double vec2[], double result[]);
	EIGEN_FUNCS_API void _MultiplySV(int dim, double real, double imag, double vec[], double result[]);

	EIGEN_FUNCS_API void _DivideVV(int dim, double vec1[], double vec2[], double result[]);

	EIGEN_FUNCS_API void _AddVV(int dim, double vec1[], double vec2[], double result[]);
	EIGEN_FUNCS_API void _SubtractVV(int dim, double vec1[], double vec2[], double result[]);


	EIGEN_FUNCS_API void _Inverse(int dim, double mat[], double inverse[]);
	EIGEN_FUNCS_API void _Inverse_Real(int dim, double mat[], double inverse[]);
	
	EIGEN_FUNCS_API void _EigenSolver(int dim, double mat[], double eigenValues[], double eigenVectors[]);
	EIGEN_FUNCS_API void _MatrixExponential(int dim, double mat[], double results[]);
	EIGEN_FUNCS_API void _CBEDSolver_Eigen(int gDim, double _potential[], double _phi0[], int tDim, double thickness[], double result[]);
	EIGEN_FUNCS_API void _CBEDSolver_Eigen2(int dim, double potential[], double psi0[], int tDim, double thickness[], double Values[], double Vectors[], double Alphas[], double Tg[]);
	EIGEN_FUNCS_API void _CBEDSolver_MatExp(int gDim, double _potential[], double _phi0[], int tDim, double tStart, double tStep, double result[]);

	EIGEN_FUNCS_API void _PartialPivLuSolve(int dim, double mat[], double vec[], double result[]);

	EIGEN_FUNCS_API void _GenerateTC1(int dim, double thickness, double _kg_z[], double _val[], double _vec[], double _tc_k[]);
	EIGEN_FUNCS_API void _GenerateTC2(int dim, double thickness, double _kg_z[], double _val[], double _vec[], double _tc_k[], double _tc_kq[]);
	EIGEN_FUNCS_API void _RowVec_SqMat_ColVec(int dim, double rowVec[], double sqMat[], double colVec[], double _result[]);

	EIGEN_FUNCS_API void _STEM_INEL1(int dim, double rowVec[], int n[], double r[], double sqMat[], double colVec[], double _result[]);


} // extern "C"