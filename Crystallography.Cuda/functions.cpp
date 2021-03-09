//
// Cardiff University | Computer Science
// Module:     CM3203 One Semester Project (40 Credits)
// Title:      Parallelisation of Matrix Exponentials in C++/CUDA for Quantum Control
// Date:       2016
//
// Author:     Peter Davison
// Supervisor: Dr. Frank C Langbein
// Moderator:  Dr. Irena Spasic
//

#define CUDAFUNCS_EXPORTS

#include <stdio.h>
#include <string.h>
#include <vector>
#include "math.h"

#include "functions.h"

#include <Eigen/Core>
#include <Eigen/Dense>
#include <Eigen/Geometry>
#include <Eigen/LU>
#include <Eigen/Eigenvalues>
#include "EigenFuncs.h"

using namespace std;
using namespace Eigen;

int main1(int argc, char** argv) {

	try {

		// Set matrix size
		int size = 5;

		// Input variables
		std::complex<double> i = std::complex<double>(0, 1);
		CUDAMatrix A(size, {
			1, 0, 0, 0, 0,
			0, 2, 0, 0, 0,
			0, 0, 3, 0, 0,
			0, 0, 0, 4, 0,
			0, 0, 0, 0, 5
			});

		// Result variables
		CUDAMatrix eA(size);
		CUDAMatrix eAi(size);

		CUDAMatrix::exp(A, eA);
		CUDAMatrix::mul(eA, i, eAi);

		// Create timers
		//CUDATimer t1, t2;

		// Calculations
		//t1 = CUDAMatrix::exp(A, eA);
		//t2 = CUDAMatrix::mul(eA, i, eAi);

		// Output
		//std::cout << "A" << A << std::endl;
		//std::cout << "e^A" << eA << t1 << std::endl;
		//std::cout << "e^A * i" << eAi << t2 << std::endl;

	}
	catch (std::exception e) {
		std::cout << std::endl << e.what() << std::endl;
	}

	return 0;

}

const std::complex<double> two_pi_i = std::complex<double>(0, 2 * 3.141592653589793238462643383279);

CUDA_FUNCS_API CUDAMatrix toCudaMatrix(int dim, double mat[])
{
	auto m = mat;
	CUDAMatrix result(dim);
	int n = 0;

	for (int r = 0; r < dim; r++)
		for (int c = 0; c < dim; c++, n += 2)
		{
			result.setCell(c, r, std::complex<double>(mat[n], mat[n + 1]));
		}
	return result;
}

CUDA_FUNCS_API MatrixXcd toMatrixXcd(int dim, CUDAMatrix mat)
{
	MatrixXcd result = MatrixXcd::Zero(dim, dim);

	auto p = mat.getMatrix();
	for (int c = 0; c < dim; ++c) {
		for (int r = 0; r < dim; r++, p++) {
			result(c, r)._Val[0] = (*p).real();
			result(c, r)._Val[1] = (*p).imag();
		}
	}
	return result;
}

CUDA_FUNCS_API VectorXcd toVectorXcd(int dim, double vec[])
{
	auto v = vec;
	VectorXcd result = VectorXcd::Zero(dim);
	for (int c = 0; c < dim; ++c) {
		result[c]._Val[0] = *(v++);
		result[c]._Val[1] = *(v++);
	}
	return result;
}



extern "C" {

	//複素非対称行列の行列指数を求める. dimの数が大きくなると、上手く計算できない.
	CUDA_FUNCS_API void MatrixExponential_Cuda(int dim, double mat[], double results[])
	{
		auto A = toCudaMatrix(dim, mat);

		// Result variables
		CUDAMatrix eA(dim);

		CUDAMatrix::exp(A, eA);

		auto r = eA.getMatrix();
		int n = 0;
		for (int col = 0; col < dim; col++)
			for (int row = 0; row < dim; row++)
			{
			
				results[n++] = eA.getCell(row, col)._Val[0];
				results[n++] = eA.getCell(row, col)._Val[1];
				//results[n++] = (*r).real(); //eA.getCell(row, col)._Val[0];
				//results[n++] = (*r).imag();//eA.getCell(row, col)._Val[1];
				//r++;
			}
	}

	//CBEDソルバー
	CUDA_FUNCS_API void CBEDSolver_MtxExp_Cuda(int dim, double potential[], double psi0[], int tDim, double tStart, double tStep, double coeff, double result[])
	{

		CUDAMatrix matStart;
		CUDAMatrix::mul(toCudaMatrix(dim, potential), two_pi_i * tStart * coeff, matStart);
		CUDAMatrix matExpStart_cuda;
		CUDAMatrix::exp(matStart, matExpStart_cuda);
		auto matExpStart = toMatrixXcd(dim, matExpStart_cuda);

		
		CUDAMatrix matStep;
		CUDAMatrix::mul(toCudaMatrix(dim, potential), two_pi_i * tStep * coeff, matStep);
		CUDAMatrix matExpStep_cuda;
		CUDAMatrix::exp(matStart, matExpStep_cuda);
		auto matExpStep = toMatrixXcd(dim, matExpStep_cuda);

		auto vec = toVectorXcd(dim, psi0);

		auto r = &result[0];
		for (int t = 0; t < tDim; ++t)
		{
			if (t == 0)
				vec = matExpStart * vec;
			else
				vec = matExpStep * vec;
			for (int g = 0; g < dim; ++g) {
				*(r++) = real(vec[g]);
				*(r++) = imag(vec[g]);
			}
		}

	}

} // extern "C"