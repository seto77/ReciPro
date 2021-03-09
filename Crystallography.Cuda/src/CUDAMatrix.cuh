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

// Precompiler include check
#ifndef cudamatrix_h
#define cudamatrix_h
// Include C/C++ stuff
#include <vector>
#include <iostream>
#include <math.h>
#include <iomanip>
#include <random>
#include <complex>
// Include CUDA stuff
#include "cuda.h"
#include "cuda_runtime.h"
#include "device_launch_parameters.h"
#include <thrust/complex.h>
#include "cuda_intellisense.h"
#include "CUDATimer.cuh"

#include "cuda_profiler_api.h"

// KERNELS
__global__ void cudaAdd(thrust::complex<double>* A, thrust::complex<double>* B, thrust::complex<double>* R, int n);
__global__ void cudaAddScalar(thrust::complex<double>* A, thrust::complex<double> scalar, thrust::complex<double>* R, int n);
__global__ void cudaSub(thrust::complex<double>* A, thrust::complex<double>* B, thrust::complex<double>* R, int n);
__global__ void cudaSubScalar(thrust::complex<double>* A, thrust::complex<double> scalar, thrust::complex<double>* R, int n);
__global__ void cudaMul(thrust::complex<double>* A, thrust::complex<double>* B, thrust::complex<double>* R, int n);
__global__ void cudaMulScalar(thrust::complex<double>* A, thrust::complex<double> scalar, thrust::complex<double>* R, int n);
__global__ void cudaAbs(thrust::complex<double>* A, thrust::complex<double>* R, int n);

class CUDAMatrix {
private:
	// STRUCTURES
	struct cudaParams {
		dim3 tpb; // Threads per block
		dim3 bpg; // Blocks per grid
	};
	struct padeParams {
		int scale;
		int mVal;
		std::vector<CUDAMatrix*> pow;
	};
	// VARIABLES
	std::complex<double>* h_matrix;
	thrust::complex<double>* d_matrix;
	int numRows, numCols, numEls;
	size_t size;
	bool initialised;
	// MEMORY HANDLERS
	void alloc();
	void dealloc();
	// CUDA STUFF
	void syncHost();
	void syncDevice();
	static cudaParams getCUDAParams(int rows, int cols);
	// INTERNAL PADE APPROXIMATION CODE
	static padeParams getPadeParams(CUDAMatrix& A);
	static int ell(CUDAMatrix& A, double coef, int m);			// Switch to CUDA calls not C++
	static std::vector<double> getPadeCoefficients(int m);
public:
	// CONSTRUCTORS & DESTRUCTOR
	CUDAMatrix();
	CUDAMatrix(int inNumRowsCols);
	CUDAMatrix(int inNumRows, int inNumCols);
	CUDAMatrix(int inNumRowsCols, std::initializer_list<std::complex<double>> inMatrix);
	CUDAMatrix(int inNumRows, int inNumCols, std::initializer_list<std::complex<double>> inMatrix);
	CUDAMatrix(const CUDAMatrix &obj);
	void init(int inNumRows, int inNumCols);
	~CUDAMatrix();
	// MATRIX OPERATIONS
	static CUDATimer add(CUDAMatrix& A, CUDAMatrix& B, CUDAMatrix& R);
	static CUDATimer add(CUDAMatrix& A, std::complex<double> scalar, CUDAMatrix& R);
	static CUDATimer sub(CUDAMatrix& A, CUDAMatrix& B, CUDAMatrix& R);
	static CUDATimer sub(CUDAMatrix& A, std::complex<double> scalar, CUDAMatrix& R);
	static CUDATimer mul(CUDAMatrix& A, CUDAMatrix& B, CUDAMatrix& R);
	static CUDATimer mul(CUDAMatrix& A, std::complex<double> scalar, CUDAMatrix& R);
	static CUDATimer pow(CUDAMatrix& A, int pow, CUDAMatrix& R);
	static CUDATimer tra(CUDAMatrix& A, CUDAMatrix& R);			// REWRITE FOR CUDA
	static CUDATimer inv(CUDAMatrix& A, CUDAMatrix& R);			// REWRITE FOR CUDA    // Special case for scalar matrices (1/diags)
	static CUDATimer exp(CUDAMatrix& A, CUDAMatrix& R);
	static CUDATimer abs(CUDAMatrix& A, CUDAMatrix& R);			// WRITE
	// BOOLEANS
	//bool isEqual(CUDAMatrix& B);								// WRITE
	//bool isScalar();											// WRITE
	bool isInitialised();
	bool isSquare();
	bool isDiagonal();
	bool isIdentity();
	bool isZero();
	bool isSmall();
	bool isComplex();
	// SETTERS
	void setCell(int row, int col, std::complex<double> val);
	void setCell(int i, std::complex<double> val);
	void setMatrix(std::complex<double> val);
	void setMatrix(std::complex<double>* inMatrix);
	void setMatrix(std::initializer_list<std::complex<double>> inMatrix);	// Complex     ||	Better function than copy?
	void setIdentity();
	void setRandomDouble(double min = 0, double max = 1);
	void setRandomInt(int min = 0, int max = 1);
	// GETTERS
	double getNorm(int n);
	//double getNormAm(int n);									// WRITE (Maybe)
	int getCurRow(int i);
	int getCurCol(int i);
	std::complex<double> getCell(int row, int col);
	std::complex<double> getCell(int i);
	std::complex<double>* getMatrix();
	int getNumRows();
	int getNumCols();
	int getNumEls();
	size_t getSize();
};

// OPERATOR OVERRIDES
std::ostream& operator<<(std::ostream& oStream, CUDAMatrix& A);			// Do exponents properly (like MATLAB)

// UTILS
namespace utils {
	int getNumDigits(double x);
	int max(int x, int y);
	double max(double x, double y);
	int min(int x, int y);
	double min(double x, double y);
}

#endif