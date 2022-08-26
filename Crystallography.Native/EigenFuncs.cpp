// EigenFuncs.cpp : DLL アプリケーション用にエクスポートされる関数を定義します。
//

#include "stdafx.h"

/*----------------*/
/* EigenFuncs.cpp */
/*----------------*/
// EigenFuncs.cpp : DLL アプリケーション用にエクスポートされる関数を定義します。
//

#define EIGEN_NO_DEBUG // コード内のassertを無効化．
//#define EIGEN_NO_STATIC_ASSERT
//#define EIGEN_STACK_ALLOCATION_LIMIT 0
//#define EIGEN_RUNTIME_NO_MALLOC

//#define EIGEN_DONT_VECTORIZE // SIMDを無効化．
//#define EIGEN_DONT_PARALLELIZE // 並列を無効化．
//#define EIGEN_MALLOC_ALREADY_ALIGNED 1
//#define EIGEN_FAST_MATH 1
#define EIGEN_STRONG_INLINE
//#define EIGEN_INITIALIZE_MATRICES_BY_ZERO
//#define EIGEN_USE_MKL_ALL

#define EIGENFUNCS_EXPORTS

//#include <complex>
#include <stdio.h>
#include <string.h>
#include <vector>
#include "math.h"

#include <../Eigen/Core>
#include <../Eigen/Dense>
#include <../Eigen/Geometry>
#include <../Eigen/LU>
#include <../Eigen/Eigenvalues>
#include "EigenFuncs.h"
#include <../unsupported/Eigen/MatrixFunctions>

using namespace Eigen;
using namespace std;

const std::complex<double> two_pi_i = complex<double>(0, 2 * 3.141592653589793238462643383279);
const size_t sizeComplex = sizeof(complex<double>);

#define Mat MatrixXcd
#define Vec VectorXcd

extern "C" {

	//行列c0~c3をr0~r3の割合でブレンドする. 実数ベクトルとして取り扱うのでdimを二倍していることに注意
	EIGEN_FUNCS_API void _Blend(int dim, double c0[], double c1[], double c2[], double c3[], double r0, double r1, double r2, double r3, double result[])
	{
		auto _c0 = Map<VectorXd>(c0, dim * 2);
		auto _c1 = Map<VectorXd>(c1, dim * 2);
		auto _c2 = Map<VectorXd>(c2, dim * 2);
		auto _c3 = Map<VectorXd>(c3, dim * 2);
		Map<VectorXd>(result, dim * 2).noalias() = r0 * _c0 + r1 * _c1 + r2 * _c2 + r3 * _c3;
	}

	//行列c0~c3をr0~r3の割合でブレンドし、自己共役を得る
	EIGEN_FUNCS_API void _BlendAndConjugate(int dim, double c0[], double c1[], double c2[], double c3[], double r0, double r1, double r2, double r3, double result[])
	{
		auto _c0 = Map<Vec>((dcomplex*)c0, dim);
		auto _c1 = Map<Vec>((dcomplex*)c1, dim);
		auto _c2 = Map<Vec>((dcomplex*)c2, dim);
		auto _c3 = Map<Vec>((dcomplex*)c3, dim);
		Map<Vec>((dcomplex*)result, dim).noalias() = (r0 * _c0 + r1 * _c1 + r2 * _c2 + r3 * _c3).conjugate();
	}

	//STEMの非弾性散乱電子強度の計算用の特殊関数
	EIGEN_FUNCS_API void _BlendAdJointMul_Mul_Mul(int dim, double c0[], double c1[], double c2[], double c3[], double r0, double r1, double r2, double r3, double mat2[], double mat3[], double result[])
	{
		auto _c0 = Map<Mat>((dcomplex*)c0, dim, dim);
		auto _c1 = Map<Mat>((dcomplex*)c1, dim, dim);
		auto _c2 = Map<Mat>((dcomplex*)c2, dim, dim);
		auto _c3 = Map<Mat>((dcomplex*)c3, dim, dim);
		auto _m2 = Map<Mat>((dcomplex*)mat2, dim, dim);
		auto _m3 = Map<Mat>((dcomplex*)mat3, dim, dim);
		Map<Mat>((dcomplex*)result, dim, dim).noalias() = (r0 * _c0 + r1 * _c1 + r2 * _c2 + r3 * _c3).adjoint() * _m2 * _m3;
	}

	//STEMの非弾性散乱電子強度の計算用の特殊関数
	EIGEN_FUNCS_API void _AdJointMul_Mul_Mul(int dim, double mat1[], double mat2[], double mat3[], double result[])
	{
		auto _mat1 = Map<Mat>((dcomplex*)mat1, dim, dim);
		auto _mat2 = Map<Mat>((dcomplex*)mat2, dim, dim);
		auto _mat3 = Map<Mat>((dcomplex*)mat3, dim, dim);
		Map<Mat>((dcomplex*)result, dim, dim).noalias() = _mat1.adjoint() * _mat2 * _mat3;
	}

	//複素非対称行列のmat1とmat2の要素ごとの掛算(アダマール積)を取る
	EIGEN_FUNCS_API void _PointwiseMultiply(int dim, double mat1[], double mat2[], double result[])
	{
		auto m1 = Map<Mat>((dcomplex*)mat1, dim, dim);
		auto m2 = Map<Mat>((dcomplex*)mat2, dim, dim);
		Map<Mat>((dcomplex*)result, dim, dim).noalias() = m1.cwiseProduct(m2);
	}

	//複素非対称行列のmat1を共役転値して、mat2に掛ける
	EIGEN_FUNCS_API void _AdjointAndMultiply(int dim, double mat1[], double mat2[], double result[])
	{
		auto m1 = Map<Mat>((dcomplex*)mat1, dim, dim);
		auto m2 = Map<Mat>((dcomplex*)mat2, dim, dim);
		Map<Mat>((dcomplex*)result, dim, dim).noalias() = m1.adjoint() * m2;
	}

	//複素非対称行列の乗算を求める
	EIGEN_FUNCS_API void _Multiply(int dim, double mat1[], double mat2[], double result[])
	{
		auto m1 = Map<Mat>((dcomplex*)mat1, dim, dim);
		auto m2 = Map<Mat>((dcomplex*)mat2, dim, dim);
		Map<Mat>((dcomplex*)result, dim, dim).noalias() = m1 * m2;
	}

	EIGEN_FUNCS_API void _MultiplyVec(int dim, double mat[], double vec[], double result[])
	{
		auto m = Map<Mat>((dcomplex*)mat, dim, dim);
		auto v = Map<Vec>((dcomplex*)vec, dim);
		Map<Vec>((dcomplex*)result, dim).noalias() = m * v;
	}

	//複素非対称行列の逆行列を求める
	EIGEN_FUNCS_API void _Inverse(int dim, double mat[], double inverse[])
	{
		Map<Mat>((dcomplex*)inverse, dim, dim).noalias() = Map<Mat>((dcomplex*)mat, dim, dim).inverse();
	}

	//PartialPivLuSolve
	EIGEN_FUNCS_API void _PartialPivLuSolve(int dim, double mat[], double vec[], double result[])
	{
		auto m = Map<Mat>((dcomplex*)mat, dim, dim);
		auto v = Map<Vec>((dcomplex*)vec, dim);
		Map<Vec>((dcomplex*)result, dim).noalias() = m.partialPivLu().solve(v);
	}

	//複素非対称行列の固有値、固有ベクトルを求める
	EIGEN_FUNCS_API void _EigenSolver(int dim, double mat[], double eigenValues[], double eigenVectors[])
	{
		ComplexEigenSolver<Mat> solver(Map<Mat>((dcomplex*)mat, dim, dim));
		Map<Vec>((dcomplex*)eigenValues, dim).noalias() = solver.eigenvalues();
		Map<Mat>((dcomplex*)eigenVectors, dim, dim).noalias() = solver.eigenvectors();
	}

	//複素非対称行列の行列指数を求める
	EIGEN_FUNCS_API void _MatrixExponential(int dim, double mat[], double results[])
	{
		Map<Mat>((dcomplex*)results, dim, dim).noalias() = Map<MatrixXcd>((dcomplex*)mat, dim, dim).exp();
		//memcpy(results, Map<MatrixXcd>((dcomplex*)mat, dim, dim).exp().eval().data(), sizeComplex * dim * dim);//上の書き方でいいのか、未検証
	}

	//CBEDソルバー
	EIGEN_FUNCS_API void _CBEDSolver_Eigen(int dim, double potential[], double psi0[], int tDim, double thickness[], double coeff, double result[])
	{
		auto res = Map<Mat>((dcomplex*)result, dim, tDim);

		ComplexEigenSolver<Mat> solver(Map<Mat>((dcomplex*)potential, dim, dim));
		Vec values = solver.eigenvalues();
		Mat vectors = solver.eigenvectors();
		Vec alpha = vectors.partialPivLu().solve(Map<Vec>((dcomplex*)psi0, dim));
		Vec gammma_alpha = Vec(dim);
		for (int t = 0; t < tDim; ++t)
		{
			const auto coeff2 = two_pi_i * thickness[t] * coeff;
			for (int g = 0; g < dim; ++g)
				gammma_alpha[g] = exp(values[g] * coeff2) * alpha[g];
			res.col(t).noalias() = vectors * gammma_alpha;
		}
	}

	//CBEDソルバー
	EIGEN_FUNCS_API void _CBEDSolver_MtxExp(int dim, double potential[], double psi0[], int tDim, double tStart, double tStep, double coeff, double result[])
	{
		auto res = Map<Mat>((dcomplex*)result, dim, tDim);
		Mat matExp = (two_pi_i * tStart * coeff * Map<Mat>((dcomplex*)potential, dim, dim)).exp().eval();
		Vec vec = matExp * Map<Vec>((dcomplex*)psi0, dim);
		res.col(0).noalias() = vec;
		if (tStep == 0)
			return;

		if (tStart != tStep)
			matExp = (two_pi_i * tStep * coeff * Map<Mat>((dcomplex*)potential, dim, dim)).exp().eval();
		for (int t = 1; t < tDim; ++t)
		{
			vec = matExp * vec;
			res.col(t).noalias() = vec;
		}
	}

} // extern "C"