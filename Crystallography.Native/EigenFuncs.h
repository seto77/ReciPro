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
	EIGEN_FUNCS_API void _SqMat_ColVec(int dim, double sqMat[], double colVec[], double _result[]);

	EIGEN_FUNCS_API void _STEM_INEL1(int dim, double rowVec[], int n[], double r[], double sqMat[], double colVec[], double _result[]);

	// EBSD強度計算ソルバー (原子位置でのブロッホ波場に基づく)
	EIGEN_FUNCS_API void _EBSDSolver(
		int bLen, int nAtoms, int tLen,
		double eigenValues[],    // bLen個の complex
		double eigenVectors[],   // bLen*bLen個の complex (column-major)
		double alpha[],          // bLen個の complex
		double phaseNG[],        // nAtoms*bLen個の complex (column-major)
		double sigma[],          // nAtoms個の double
		double thicknesses[],    // tLen個の double
		double intensity[]       // tLen個の double (出力)
	);

	// セル中心のフル固有値分解 (右+左固有ベクトル + α)
	// CBED, LACBED, EBSD, STEM 共通
	EIGEN_FUNCS_API void _EigenSolverFull(
		int dim,
		double eigenMatrix[],    // A行列 (入力, dim×dim complex)
		double eigenValues[],    // γ_j (出力, dim complex)
		double rightVectors[],   // C_g^(j) (出力, dim×dim complex, column-major)
		double leftVectors[],    // L_g^(j) (出力, dim×dim complex, column-major)
		double alpha[]           // α_j (出力, dim complex)
	);

	// 1次摂動による近似計算
	// CBED, LACBED, EBSD, STEM 共通
	EIGEN_FUNCS_API void _EigenPerturb(
		int dim,
		double eigenValues0[],    // 基準の γ_j (入力)
		double rightVectors0[],   // 基準の C (入力)
		double leftVectors0[],    // 基準の L (入力)
		double eigenMatrix0[],    // 基準の A (入力)
		double eigenMatrix1[],    // 新しい A (入力)
		double eigenValues1[],    // 補正後の γ (出力)
		double rightVectors1[],   // 補正後の C (出力)
		double alpha1[]           // 補正後の α (出力)
	);

	

} // extern "C"