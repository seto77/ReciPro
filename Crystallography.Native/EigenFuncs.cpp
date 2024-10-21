// EigenFuncs.cpp : DLL アプリケーション用にエクスポートされる関数を定義します。
//

#include "stdafx.h"

/*----------------*/
/* EigenFuncs.cpp */
/*----------------*/
// EigenFuncs.cpp : DLL アプリケーション用にエクスポートされる関数を定義します。
//

#define EIGEN_NO_DEBUG // コード内のassertを無効化．
#define EIGEN_DONT_PARALLELIZE // 並列を無効化．
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
#include <../unsupported/Eigen/MatrixFunctions>

#include "EigenFuncs.h"

using namespace Eigen;
using namespace std;

const std::complex<double> two_pi_i = complex<double>(0, 2 * 3.141592653589793238462643383279);
//const size_t sizeComplex = sizeof(complex<double>);

#define Mat MatrixXcd
//縦ベクトル
#define Vec VectorXcd
//横ベクトル
#define VecR RowVectorXcd


extern "C" {
	//行列c0~c3をr0~r3の割合でブレンドする
	EIGEN_FUNCS_API void _Blend(int dim, double c0[], double c1[], double c2[], double c3[], double r0, double r1, double r2, double r3, double result[])
	{
		auto _c0 = Map<VectorXd>(c0, dim);
		auto _c1 = Map<VectorXd>(c1, dim);
		auto _c2 = Map<VectorXd>(c2, dim);
		auto _c3 = Map<VectorXd>(c3, dim);
		Map<VectorXd>(result, dim).noalias() = r0 * _c0 + r1 * _c1 + r2 * _c2 + r3 * _c3;
	}

	//行列c0~c3をr0~r3の割合でブレンドし、自己共役を得る
	EIGEN_FUNCS_API void _BlendAndConjugate(int dim, double c0[], double c1[], double c2[], double c3[], double r0, double r1, double r2, double r3, double result[])
	{
		auto _c0 = Map<Vec>((dcomplex*)c0, dim);
		auto _c1 = Map<Vec>((dcomplex*)c1, dim);
		auto _c2 = Map<Vec>((dcomplex*)c2, dim);
		auto _c3 = Map<Vec>((dcomplex*)c3, dim);
		Map<Vec>((dcomplex*)result, dim).noalias() = r0 * _c0.conjugate() + r1 * _c1.conjugate() + r2 * _c2.conjugate() + r3 * _c3.conjugate();

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

	//STEM用の特殊関数. 透過係数を求める
	EIGEN_FUNCS_API void _GenerateTC1(int dim, double thickness, double _kg_z[], double _val[], double _vec[], double _tc_k[])
	{
		auto val = (dcomplex*)_val;

		Vec exp_kgz = Vec(dim);
		Vec exp_val = Vec(dim);

		for (int i = 0; i < dim; ++i)
		{
			exp_kgz[i] = exp(two_pi_i * thickness * _kg_z[i]);
			exp_val[i] = exp(two_pi_i * thickness * val[i]);
		}
		auto m = Map<Mat>((dcomplex*)_vec, dim, dim);
		Map<Vec>((dcomplex*)_tc_k, dim).noalias() = exp_kgz.asDiagonal() * m * exp_val;
	}

	//STEM用の特殊関数. 透過係数を求める
	EIGEN_FUNCS_API void _GenerateTC2(int dim, double thickness, double _kg_z[], double _val[], double _vec[], double _tc_k[], double _tc_kq[])
	{
		auto val = (dcomplex*)_val;

		Vec exp_kgz = Vec(dim);
		Vec exp_val = Vec(dim);

		for (int i = 0; i < dim; ++i)
		{
			exp_kgz[i] = exp(two_pi_i * thickness * _kg_z[i]);
			exp_val[i] = exp(two_pi_i * thickness * val[i]);
		}
		auto m = Map<Mat>((dcomplex*)_vec, dim, dim);
		auto res = exp_kgz.asDiagonal() * m * exp_val;
		
		Map<Vec>((dcomplex*)_tc_k, dim).noalias() = res;
		Map<Vec>((dcomplex*)_tc_kq, dim).noalias() = res.conjugate();
	}

	//横ベクトル×正方行列×縦ベクトルの掛算. STEMの非弾性散乱を求めるときに使用
	EIGEN_FUNCS_API void _RowVec_SqMat_ColVec(int dim, double rowVec[],  double sqMat[], double colVec[], double _result[])
	{
		auto rV = Map<VecR>((dcomplex*)rowVec, dim);
		auto m = Map<Mat>((dcomplex*)sqMat, dim, dim);
		auto cV = Map<Vec>((dcomplex*)colVec, dim);
		Map<Mat>((dcomplex*)_result, 1, 1).noalias() = rV * m * cV;
	}

	//横ベクトル×正方行列×縦ベクトルの掛算. STEMの非弾性散乱を求めるときに使用
	EIGEN_FUNCS_API void _SqMat_ColVec(int dim, double sqMat[], double colVec[], double _result[])
	{
		auto m = Map<Mat>((dcomplex*)sqMat, dim, dim);
		auto cV = Map<Vec>((dcomplex*)colVec, dim);
		Map<Vec>((dcomplex*)_result, dim).noalias() = m * cV;
	}

	EIGEN_FUNCS_API void _STEM_INEL1(int dim, double rowVec[], int n[], double r[], double sqMat[], double colVec[], double _result[])
	{
		auto rV1 = Map<VecR>((dcomplex*)(rowVec + n[0] * dim * 2), dim);
		auto rV2 = Map<VecR>((dcomplex*)(rowVec + n[1] * dim * 2), dim);
		auto rV3 = Map<VecR>((dcomplex*)(rowVec + n[2] * dim * 2), dim);
		auto rV4 = Map<VecR>((dcomplex*)(rowVec + n[3] * dim * 2), dim);
		auto m = Map<Mat>((dcomplex*)sqMat, dim, dim);
		auto cV = Map<Vec>((dcomplex*)colVec, dim);
		Map<Mat>((dcomplex*)_result, 1, 1).noalias() = (r[0] * rV1 + r[1] * rV2 + r[2] * rV3 + r[3] * rV4) * m * cV;
	}

	//複素非対称行列のmat1とmat2の要素ごとの掛算(アダマール積)を取る
	EIGEN_FUNCS_API void _PointwiseMultiply(int dim, double mat1[], double mat2[], double result[])
	{
		auto m1 = Map<Mat>((dcomplex*)mat1, dim, dim);
		auto m2 = Map<Mat>((dcomplex*)mat2, dim, dim);
		Map<Mat>((dcomplex*)result, dim, dim).noalias() = m1.cwiseProduct(m2);
	}

	//複素行列のmat1を共役転値して、mat2に掛ける
	EIGEN_FUNCS_API void _AdjointAndMultiply(int dim, double mat1[], double mat2[], double result[])
	{
		auto m1 = Map<Mat>((dcomplex*)mat1, dim, dim);
		auto m2 = Map<Mat>((dcomplex*)mat2, dim, dim);
		Map<Mat>((dcomplex*)result, dim, dim).noalias() = m1.adjoint() * m2;
	}

	//複素行列同士の乗算を求める
	EIGEN_FUNCS_API void _MultiplyMM(int dim, double mat1[], double mat2[], double result[])
	{
		auto m1 = Map<Mat>((dcomplex*)mat1, dim, dim);
		auto m2 = Map<Mat>((dcomplex*)mat2, dim, dim);
		Map<Mat>((dcomplex*)result, dim, dim).noalias() = m1 * m2;
	}

	//複素行列同士の乗算を求める
	EIGEN_FUNCS_API void _MultiplyMMM(int dim, double mat1[], double mat2[], double mat3[], double result[])
	{
		auto m1 = Map<Mat>((dcomplex*)mat1, dim, dim);
		auto m2 = Map<Mat>((dcomplex*)mat2, dim, dim);
		auto m3 = Map<Mat>((dcomplex*)mat3, dim, dim);
		Map<Mat>((dcomplex*)result, dim, dim).noalias() = m1 * m2 * m3;
	}

	//実数行列同士の乗算を求める
	EIGEN_FUNCS_API void _MultiplyMM_Real(int dim, double mat1[], double mat2[], double result[])
	{
		auto m1 = Map<MatrixXd>((double*)mat1, dim, dim);
		auto m2 = Map<MatrixXd>((double*)mat2, dim, dim);
		Map<MatrixXd>((double*)result, dim, dim).noalias() = m1 * m2;
	}

	//複素行列と複素ベクトルの乗算を求める
	EIGEN_FUNCS_API void _MultiplyMV(int dim, double mat[], double vec[], double result[])
	{
		auto m = Map<Mat>((dcomplex*)mat, dim, dim);
		auto v = Map<Vec>((dcomplex*)vec, dim);
		Map<Vec>((dcomplex*)result, dim).noalias() = m * v;
	}

	//複素ベクトル同士の乗算を求める
	EIGEN_FUNCS_API void _MultiplyVV(int dim, double vec1[], double vec2[], double result[])
	{
		auto v1 = Map<VecR>((dcomplex*)vec1, dim);
		auto v2 = Map<Vec>((dcomplex*)vec2, dim);
		Map<Mat>((dcomplex*)result, 1, 1).noalias() = v1 * v2;
	}

	//複素数と複素ベクトルの乗算を求める
	EIGEN_FUNCS_API void _MultiplySV(int dim, double real, double imag, double vec[], double result[])
	{
		auto s = dcomplex(real, imag);
		auto v = Map<Vec>((dcomplex*)vec, dim);
		Map<Vec>((dcomplex*)result, dim).noalias() = s * v;
	}

	//ベクトル同士の加算を求める. 実数と複素数の両方いける
	EIGEN_FUNCS_API void _AddVV(int dim, double vec1[], double vec2[], double result[])
	{
		auto v1 = Map<VectorXd>(vec1, dim);
		auto v2 = Map<VectorXd>(vec2, dim);
		Map<VectorXd>(result, dim).noalias() = v1 + v2;
	}

	//複素ベクトル同士の要素ごとの除算を求める
	EIGEN_FUNCS_API void _DivideVV(int dim, double vec1[], double vec2[], double result[])
	{
		auto v1 = Map<Vec>((dcomplex*)vec1, dim);
		auto v2 = Map<Vec>((dcomplex*)vec2, dim);

		auto res = (dcomplex*)result;
		
		for (int i = 0; i < dim; i++)
			res[i] = v1[i] / v2[i];
	}

	//複素ベクトル同士の減算を求める. 実数と複素数の両方いける
	EIGEN_FUNCS_API void _SubtractVV(int dim, double vec1[], double vec2[], double result[])
	{
		auto v1 = Map<VectorXd>(vec1, dim);
		auto v2 = Map<VectorXd>(vec2, dim);
		Map<VectorXd>(result, dim).noalias() = v1 - v2;
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
	EIGEN_FUNCS_API void _CBEDSolver_Eigen(int dim, double potential[], double psi0[], int tDim, double thickness[], double result[])
	{
		ComplexEigenSolver<Mat> solver(Map<Mat>((dcomplex*)potential, dim, dim));
		auto values = solver.eigenvalues();
		Mat vectors = solver.eigenvectors() * solver.eigenvectors().partialPivLu().solve(Map<Vec>((dcomplex*)psi0, dim)).asDiagonal();

		Mat gamma = Mat(dim, tDim);
		for (int t = 0; t < tDim; ++t)
		{
			const auto coeff2 = two_pi_i * thickness[t];
			for (int g = 0; g < dim; ++g)
				gamma(g,t) = exp(values[g] * coeff2);
		}
		
		Map<Mat>((dcomplex*)result, dim, tDim).noalias() = vectors * gamma;
	}

	//CBEDソルバー. 固有値、固有ベクトル、αも返す
	EIGEN_FUNCS_API void _CBEDSolver_Eigen2(int dim, double potential[], double psi0[], int tDim, double thickness[], double Values[], double Vectors[], double Alphas[], double Tg[])
	{
		auto vals = Map<Vec>((dcomplex*)Values, dim);
		auto vecs = Map<Mat>((dcomplex*)Vectors, dim, dim);
		auto alphas = Map<Vec>((dcomplex*)Alphas, dim);
		auto tg = Map<Mat>((dcomplex*)Tg, dim, tDim);

		ComplexEigenSolver<Mat> solver(Map<Mat>((dcomplex*)potential, dim, dim));
		vals.noalias() = solver.eigenvalues();
		vecs.noalias() = solver.eigenvectors();
		alphas.noalias() = vecs.partialPivLu().solve(Map<Vec>((dcomplex*)psi0, dim));
		Vec gamma_alpha = Vec(dim);
		for (int t = 0; t < tDim; ++t)
		{
			const auto coeff2 = two_pi_i * thickness[t];
			for (int g = 0; g < dim; ++g)
				gamma_alpha[g] = exp(vals[g] * coeff2) * alphas[g];
			tg.col(t).noalias() = vecs * gamma_alpha;
		}
	}

	//CBEDソルバー
	EIGEN_FUNCS_API void _CBEDSolver_MtxExp(int dim, double potential[], double psi0[], int tDim, double tStart, double tStep, double result[])
	{
		auto res = Map<Mat>((dcomplex*)result, dim, tDim);
		Mat matExp = (two_pi_i * tStart * Map<Mat>((dcomplex*)potential, dim, dim)).exp().eval();
		Vec vec = matExp * Map<Vec>((dcomplex*)psi0, dim);
		res.col(0).noalias() = vec;
		if (tStep == 0)
			return;

		if (tStart != tStep)
			matExp = (two_pi_i * tStep * Map<Mat>((dcomplex*)potential, dim, dim)).exp().eval();
		for (int t = 1; t < tDim; ++t)
		{
			vec = matExp * vec;
			res.col(t).noalias() = vec;
		}
	}

} // extern "C"