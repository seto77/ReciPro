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

// Include header file
#include "CUDAMatrix.cuh"

// KERNELS

__global__ void cudaAdd(thrust::complex<double>* A, thrust::complex<double>* B, thrust::complex<double>* R, int n) {
	int row = blockIdx.y * blockDim.y + threadIdx.y;
	int col = blockIdx.x * blockDim.x + threadIdx.x;
	if (row < n && col < n) {
		R[row * n + col] = A[row * n + col] + B[row * n + col];
	}
	__syncthreads();
}

__global__ void cudaAddScalar(thrust::complex<double>* A, thrust::complex<double> scalar, thrust::complex<double>* R, int n) {
	int row = blockIdx.y * blockDim.y + threadIdx.y;
	int col = blockIdx.x * blockDim.x + threadIdx.x;
	if (row < n && col < n) {
		R[row * n + col] = A[row * n + col] + scalar;
	}
	__syncthreads();
}

__global__ void cudaSub(thrust::complex<double>* A, thrust::complex<double>* B, thrust::complex<double>* R, int n) {
	int row = blockIdx.y * blockDim.y + threadIdx.y;
	int col = blockIdx.x * blockDim.x + threadIdx.x;
	if (row < n && col < n) {
		R[row * n + col] = A[row * n + col] - B[row * n + col];
	}
	__syncthreads();
}

__global__ void cudaSubScalar(thrust::complex<double>* A, thrust::complex<double> scalar, thrust::complex<double>* R, int n) {
	int row = blockIdx.y * blockDim.y + threadIdx.y;
	int col = blockIdx.x * blockDim.x + threadIdx.x;
	if (row < n && col < n) {
		R[row * n + col] = A[row * n + col] - scalar;
	}
	__syncthreads();
}

__global__ void cudaMul(thrust::complex<double>* A, thrust::complex<double>* B, thrust::complex<double>* R, int n) {
	thrust::complex<double> sum = 0;
	int row = blockIdx.y * blockDim.y + threadIdx.y;
	int col = blockIdx.x * blockDim.x + threadIdx.x;
	if (row < n && col < n) {
		for (int i = 0; i < n; i++) {
			sum += A[row * n + i] * B[i * n + col];
		}
	}
	R[row * n + col] = sum;
	__syncthreads();
}

__global__ void cudaMulScalar(thrust::complex<double>* A, thrust::complex<double> scalar, thrust::complex<double>* R, int n) {
	int row = blockIdx.y * blockDim.y + threadIdx.y;
	int col = blockIdx.x * blockDim.x + threadIdx.x;
	if (row < n && col < n) {
		R[row * n + col] = A[row * n + col]  * scalar;
	}
	__syncthreads();
}

__global__ void cudaAbs(thrust::complex<double>* A, thrust::complex<double>* R, int n) {
	int row = blockIdx.y * blockDim.y + threadIdx.y;
	int col = blockIdx.x * blockDim.x + threadIdx.x;
	if (row < n && col < n) {
		R[row * n + col] = abs(A[row * n + col]);
	}
	__syncthreads();
}

// MEMORY HANDLERS

void CUDAMatrix::alloc() {
	h_matrix = (std::complex<double>*) malloc(size);
	cudaError_t result = cudaMalloc((void**) &d_matrix, size);
	if (result != cudaSuccess) {
		throw std::runtime_error("Failed to allocate device memory");
	}
}

void CUDAMatrix::dealloc() {
	free(h_matrix);
	cudaError_t result = cudaFree(d_matrix);
	if (result != cudaSuccess) {
		throw std::runtime_error("Failed to free device memory");
	}
}
 
// CUDA STUFF

void CUDAMatrix::syncHost() {
	if (isInitialised()) {
		cudaError_t result = cudaMemcpy(h_matrix, d_matrix, size, cudaMemcpyDeviceToHost);
		if (result != cudaSuccess) {
			throw std::runtime_error("Failed to allocate device memory");
		}
	} else {
		throw std::runtime_error("Cannot perform matrix operations before initialisation");
	}
}

void CUDAMatrix::syncDevice() {
	if (isInitialised()) {
		cudaError_t result = cudaMemcpy(d_matrix, h_matrix, size, cudaMemcpyHostToDevice);
		if (result != cudaSuccess) {
			throw std::runtime_error("Failed to allocate device memory");
		}
	} else {
		throw std::runtime_error("Cannot perform matrix operations before initialisation");
	}
}

CUDAMatrix::cudaParams CUDAMatrix::getCUDAParams(int rows, int cols) {
	cudaParams cp;
	cp.tpb = dim3(rows, cols);
	cp.bpg = dim3(1, 1);
	
	//以下をコメントアウトしてみた
	if (rows*cols > 512*512) {
		cp.tpb.x = 512;
		cp.tpb.y = 512;
		cp.bpg.x = (int) (ceil(double(rows) / double(cp.tpb.x)));
		cp.bpg.y = (int) (ceil(double(cols) / double(cp.tpb.y)));
	}
	return cp;
}

// INTERNAL PADE APPROXIMATION CODE

int CUDAMatrix::ell(CUDAMatrix& A, double coef, int m) {
	CUDAMatrix sA(A.getNumRows());
	CUDAMatrix::abs(A, sA);
	double scale = std::pow(coef, (1 / (double) (2 * m + 1)));
	CUDAMatrix::mul(sA, scale, sA);
	//double alpha = sA.getNormAm(2 * m + 1) / A.getNorm(1);     2 LINES BELOW ARE TEMPORARY REPLACEMENT
	CUDAMatrix::pow(sA, (2 * m + 1), sA);
	double alpha = sA.getNorm(1) / (double) (A.getNorm(1));
	/////
	return utils::max((int) (ceil(log2(2 * alpha / std::numeric_limits<double>::epsilon()) / (2 * m))), 0);
}

CUDAMatrix::padeParams CUDAMatrix::getPadeParams(CUDAMatrix& A) {
	// Init
	double d4, d6, d8, d10, eta1, eta3, eta4, eta5;
	int ar = A.getNumRows();
	int ac = A.getNumCols();
	std::vector<double> theta;
	std::vector<double> coef;
	// Init P;
	padeParams p;
	p.pow.resize(11);
	p.scale = 0;
	// Get coefficients and theta values
	coef = {
		(1 / 100800.0),
		(1 / 10059033600.0),
		(1 / 4487938430976000.0),
		(1 / 5914384781877411840000.0),
		(1 / 113250775606021113483283660800000000.0)
	};
	theta = {
		1.495585217958292e-002,
		2.539398330063230e-001,
		9.504178996162932e-001,
		2.097847961257068e+000,
		5.371920351148152e+000
	};
	// Get powers of A
	p.pow[2] = new CUDAMatrix(ar, ac);
	p.pow[4] = new CUDAMatrix(ar, ac);
	p.pow[6] = new CUDAMatrix(ar, ac);
	p.pow[8] = new CUDAMatrix(ar, ac);
	p.pow[10] = new CUDAMatrix(ar, ac);
	cudaParams cp = getCUDAParams(A.getNumRows(), A.getNumCols());
	cudaMul KERNEL_ARGS2(cp.bpg, cp.tpb) (A.d_matrix, A.d_matrix, p.pow[2]->d_matrix, ar);
	cudaMul KERNEL_ARGS2(cp.bpg, cp.tpb) (p.pow[2]->d_matrix, p.pow[2]->d_matrix, p.pow[4]->d_matrix, ar);
	cudaMul KERNEL_ARGS2(cp.bpg, cp.tpb) (p.pow[2]->d_matrix, p.pow[4]->d_matrix, p.pow[6]->d_matrix, ar);
	cudaMul KERNEL_ARGS2(cp.bpg, cp.tpb) (p.pow[4]->d_matrix, p.pow[4]->d_matrix, p.pow[8]->d_matrix, ar);
	cudaMul KERNEL_ARGS2(cp.bpg, cp.tpb) (p.pow[4]->d_matrix, p.pow[6]->d_matrix, p.pow[10]->d_matrix, ar);

	// NOT IDEAL .. PERFORM GETNORM ON DEVICE IF POSSIBLE. THIS MEANS SYNCING BETWEEN HOST AND DEVICE IS UNNECESSARY
	p.pow[2]->syncHost();
	p.pow[4]->syncHost();
	p.pow[6]->syncHost();
	p.pow[8]->syncHost();
	p.pow[10]->syncHost();
	////

	// Find mVal
	d4 = std::pow(p.pow[4]->getNorm(1), (1.0 / 4));
	d6 = std::pow(p.pow[6]->getNorm(1), (1.0 / 6));
	eta1 = utils::max(d4, d6);
	if ((eta1 <= theta[0]) && (ell(A, coef[0], 3) == 0)) {
		p.mVal = 3;
		return p;
	}
	if ((eta1 <= theta[1]) && (ell(A, coef[1], 5) == 0)) {
		p.mVal = 5;
		return p;
	}
	if (true) { //(A.isSmall()) {
		d8 = std::pow(p.pow[8]->getNorm(1), (1.0 / 8));
	} else {
		//d8 = pow(p.pow[4]->getNormAm(2), (1.0 / 8));
	}
	eta3 = utils::max(d6, d8);
	if ((eta3 <= theta[2]) && (ell(A, coef[2], 7) == 0)) {
		p.mVal = 7;
		return p;
	}
	if ((eta3 <= theta[3]) && (ell(A, coef[3], 9) == 0)) {
		p.mVal = 9;
		return p;
	}
	if (true) { //(A.isSmall()) {
		d10 = std::pow(p.pow[10]->getNorm(1), (1.0 / 10));
	} else {
		//d10 = std::pow(p.pow[2]->getNormAm(5), (1.0 / 10));
	}
	// Find scaling factor
	eta4 = utils::max(d8, d10);
	eta5 = utils::min(eta3, eta4);
	p.scale = utils::max((int) (ceil(log2(eta5 / theta[4]))), 0);
	CUDAMatrix sA(ar, ac);
	double multiplier = 1.0 / std::pow(2, p.scale);
	CUDAMatrix::mul(A, multiplier, sA);
	p.scale += ell(sA, coef[4], 13);
	if (std::isinf((double) p.scale)) {
		std::cout << "S = INF" << std::endl;
		int exp;																		// THIS CODE IS NOT ERROR CHECKED!!!!!
		double t = std::frexp(A.getNorm(1) / theta[4], &exp);
		p.scale = exp - (t == 0.5);
	}
	p.mVal = 13;
	return p;
}

std::vector<double> CUDAMatrix::getPadeCoefficients(int m) {
	switch (m) {
		case 3:
			return { 120, 60, 12, 1 };
		case 5:
			return { 30240, 15120, 3360, 420, 30, 1 };
		case 7:
			return { 17297280, 8648640, 1995840, 277200, 25200, 1512, 56, 1 };
		case 9:
			return { 17643225600, 8821612800, 2075673600, 302702400, 30270240, 2162160, 110880, 3960, 90, 1 };
		case 13:
			return { 64764752532480000, 32382376266240000, 7771770303897600, 1187353796428800, 129060195264000, 10559470521600, 670442572800, 33522128640, 1323241920, 40840800, 960960, 16380, 182, 1 };
		default:
			throw std::runtime_error("Invalid m value");
	}
}

// CONSTRUCTORS

CUDAMatrix::CUDAMatrix() {
	initialised = false;
}

CUDAMatrix::CUDAMatrix(int inNumRowsCols) {
	init(inNumRowsCols, inNumRowsCols);
	setMatrix(0.0);
}

CUDAMatrix::CUDAMatrix(int inNumRows, int inNumCols) {
	init(inNumRows, inNumCols);
	setMatrix(0.0);
}

CUDAMatrix::CUDAMatrix(int inNumRowsCols, std::initializer_list<std::complex<double>> inMatrix) {
	if (inMatrix.size() == inNumRowsCols*inNumRowsCols) {
		init(inNumRowsCols, inNumRowsCols);
		setMatrix(inMatrix);
	} else {
		throw std::runtime_error("Initialiser-list size does not match matrix size");
	}
}

CUDAMatrix::CUDAMatrix(int inNumRows, int inNumCols, std::initializer_list<std::complex<double>> inMatrix) {
	if (inMatrix.size() == inNumRows*inNumCols) {
		init(inNumRows, inNumCols);
		setMatrix(inMatrix);
	} else {
		throw std::runtime_error("Initialiser-list size does not match matrix size");
	}
}

CUDAMatrix::CUDAMatrix(const CUDAMatrix &obj) {
	if (obj.initialised) {
		h_matrix = obj.h_matrix;
		d_matrix = obj.d_matrix;
		numRows = obj.numRows;
		numCols = obj.numCols;
		numEls = obj.numEls;
		size = obj.size;
		initialised = obj.initialised;
	} else {
		throw std::runtime_error("Cannot copy uninitialised matrix");
	}
}

void CUDAMatrix::init(int inNumRows, int inNumCols) {
	numRows = inNumRows;
	numCols = inNumCols;
	numEls = inNumRows*inNumCols;
	size = sizeof(std::complex<double>) * numEls;
	alloc();
	initialised = true;
}

CUDAMatrix::~CUDAMatrix() {
	dealloc();
}
 
// MATRIX OPERATIONS

CUDATimer CUDAMatrix::add(CUDAMatrix& A, CUDAMatrix& B, CUDAMatrix& R) {
	if (A.isInitialised() && B.isInitialised() && R.isInitialised()) {
		int ar = A.getNumRows();
		int ac = A.getNumCols();
		int br = B.getNumRows();
		int bc = B.getNumCols();
		int rr = R.getNumRows();
		int rc = R.getNumCols();
		if (ar == ac && ac == br && br == bc && bc == rr && rr == rc) {
			A.syncDevice();
			B.syncDevice();

			cudaParams cp = getCUDAParams(ar, ac);
			CUDATimer t;

			t.start();
			cudaAdd KERNEL_ARGS2(cp.bpg, cp.tpb) (A.d_matrix, B.d_matrix, R.d_matrix, A.getNumRows());
			t.stop();

			R.syncHost();
			return t;
		} else {
			throw std::runtime_error("Matrix sizes do not match");
		}
	} else {
		throw std::runtime_error("Cannot perform matrix operations before initialisation");
	}
}

CUDATimer CUDAMatrix::add(CUDAMatrix& A, std::complex<double> scalar, CUDAMatrix& R) {
	if (A.isInitialised() && R.isInitialised()) {
		int ar = A.getNumRows();
		int ac = A.getNumCols();
		int rr = R.getNumRows();
		int rc = R.getNumCols();
		if (ar == ac && ac == rr && rr == rc) {
			A.syncDevice();

			cudaParams cp = getCUDAParams(ar, ac);
			CUDATimer t;
			
			t.start();
			cudaAddScalar KERNEL_ARGS2(cp.bpg, cp.tpb) (A.d_matrix, scalar, R.d_matrix, A.getNumRows());
			t.stop();

			R.syncHost();
			return t;
		} else {
			throw std::runtime_error("Matrix sizes do not match");
		}
	} else {
		throw std::runtime_error("Cannot perform matrix operations before initialisation");
	}
}

CUDATimer CUDAMatrix::sub(CUDAMatrix& A, CUDAMatrix& B, CUDAMatrix& R) {
	if (A.isInitialised() && B.isInitialised() && R.isInitialised()) {
		int ar = A.getNumRows();
		int ac = A.getNumCols();
		int br = B.getNumRows();
		int bc = B.getNumCols();
		int rr = R.getNumRows();
		int rc = R.getNumCols();
		if (ar == ac && ac == br && br == bc && bc == rr && rr == rc) {
			A.syncDevice();
			B.syncDevice();

			cudaParams cp = getCUDAParams(ar, ac);
			CUDATimer t;
			
			t.start();
			cudaSub KERNEL_ARGS2(cp.bpg, cp.tpb) (A.d_matrix, B.d_matrix, R.d_matrix, A.getNumRows());
			t.stop();

			R.syncHost();
			return t;
		} else {
			throw std::runtime_error("Matrix sizes do not match");
		}
	} else {
		throw std::runtime_error("Cannot perform matrix operations before initialisation");
	}
}

CUDATimer CUDAMatrix::sub(CUDAMatrix& A, std::complex<double> scalar, CUDAMatrix& R) {
	if (A.isInitialised() && R.isInitialised()) {
		int ar = A.getNumRows();
		int ac = A.getNumCols();
		int rr = R.getNumRows();
		int rc = R.getNumCols();
		if (ar == ac && ac == rr && rr == rc) {
			A.syncDevice();

			cudaParams cp = getCUDAParams(ar, ac);
			CUDATimer t;

			t.start();
			cudaSubScalar KERNEL_ARGS2(cp.bpg, cp.tpb) (A.d_matrix, scalar, R.d_matrix, A.getNumRows());
			t.stop();

			R.syncHost();
			return t;
		} else {
			throw std::runtime_error("Matrix sizes do not match");
		}
	} else {
		throw std::runtime_error("Cannot perform matrix operations before initialisation");
	}
}

CUDATimer CUDAMatrix::mul(CUDAMatrix& A, CUDAMatrix& B, CUDAMatrix& R) {
	if (A.isInitialised() && B.isInitialised() && R.isInitialised()) {
		int ar = A.getNumRows();
		int ac = A.getNumCols();
		int br = B.getNumRows();
		int bc = B.getNumCols();
		int rr = R.getNumRows();
		int rc = R.getNumCols();
		if (ar == ac && ac == br && br == bc && bc == rr && rr == rc) {
			A.syncDevice();
			B.syncDevice();

			cudaParams cp = getCUDAParams(ar, ac);
			CUDATimer t;
			
			t.start();
			cudaMul KERNEL_ARGS2(cp.bpg, cp.tpb) (A.d_matrix, B.d_matrix, R.d_matrix, A.getNumRows());
			t.stop();

			R.syncHost();
			return t;
		} else {
			throw std::runtime_error("Matrix sizes do not match");
		}
	} else {
		throw std::runtime_error("Cannot perform matrix operations before initialisation");
	}
}

CUDATimer CUDAMatrix::mul(CUDAMatrix& A, std::complex<double> scalar, CUDAMatrix& R) {
	if (A.isInitialised() && R.isInitialised()) {
		int ar = A.getNumRows();
		int ac = A.getNumCols();
		int rr = R.getNumRows();
		int rc = R.getNumCols();
		if (ar == ac && ac == rr && rr == rc) {
			A.syncDevice();

			cudaParams cp = getCUDAParams(ar, ac);
			CUDATimer t;
			
			t.start();
			cudaMulScalar KERNEL_ARGS2(cp.bpg, cp.tpb) (A.d_matrix, scalar, R.d_matrix, A.getNumRows());
			t.stop();

			R.syncHost();
			return t;
		} else {
			throw std::runtime_error("Matrix sizes do not match");
		}
	} else {
		throw std::runtime_error("Cannot perform matrix operations before initialisation");
	}
}

CUDATimer CUDAMatrix::pow(CUDAMatrix& A, int pow, CUDAMatrix& R) {
	if (A.isInitialised() && R.isInitialised()) {
		int ar = A.getNumRows();
		int ac = A.getNumCols();
		int rr = R.getNumRows();
		int rc = R.getNumCols();
		if (ar == ac && ac == rr && rr == rc) {
			A.syncDevice();
			CUDAMatrix T(ar);
			T.setIdentity();
			T.syncDevice();

			cudaParams cp = getCUDAParams(ar, ac);
			CUDATimer t;

			t.start();
			for (int c1 = 0; c1 < pow; c1++) {
				cudaMul KERNEL_ARGS2(cp.bpg, cp.tpb) (A.d_matrix, T.d_matrix, T.d_matrix, ar);
			}
			t.stop();

			T.syncHost();
			R.setMatrix(T.getMatrix());
			return t;
		} else {
			throw std::runtime_error("Matrix sizes do not match");
		}
	} else {
		throw std::runtime_error("Cannot perform matrix operations before initialisation");
	}
}

CUDATimer CUDAMatrix::inv(CUDAMatrix& A, CUDAMatrix& R) {
	if (A.isInitialised() && R.isInitialised()) {
		int ar = A.getNumRows();
		int ac = A.getNumCols();
		int rr = R.getNumRows();
		int rc = R.getNumCols();
		if (ar == ac && ac == rr && rr == rc) {
			
			CUDATimer t;
			CUDAMatrix L = CUDAMatrix(ar, ac);
			CUDAMatrix U = CUDAMatrix(ar, ac);
			CUDAMatrix Z = CUDAMatrix(ar, ac);
			CUDAMatrix I = CUDAMatrix(ar, ac);
			I.setIdentity();

			t.start();

			int n = ar;
			int i, j, k;
			// LU Decomposition
			for (i = 0; i < n; i++) {
				for (j = 0; j < n; j++) {
					if (j < i) {
						U.setCell(i, j, 0);
					} else {
						U.setCell(i, j, A.getCell(i, j));
						for (k = 0; k < i; k++) {
							U.setCell(i, j, (U.getCell(i, j) - U.getCell(k, j) * L.getCell(i, k)));
						}
					}
				}
				for (j = 0; j < n; j++) {
					if (j < i) {
						L.setCell(j, i, 0);
					} else if (j == i) {
						L.setCell(j, i, 1);
					} else {
						L.setCell(j, i, (A.getCell(j, i) / U.getCell(i, i)));
						for (k = 0; k < i; k++) {
							L.setCell(j, i, (L.getCell(j, i) - ((U.getCell(k, i) * L.getCell(j, k)) / U.getCell(i, i))));
						}
					}
				}
			}
			for (i = 0; i < n; i++) {
				// Find Z (L^-1) with Forward Substitution
				for (j = 0; j < n; j++) {
					Z.setCell(j, i, I.getCell(j, i));
					for (k = 0; k < n; k++) {
						if (k != j) {
							Z.setCell(j, i, (Z.getCell(j, i) - (L.getCell(j, k) * Z.getCell(k, i))));
						}
					}
				}
				// Find X (A^-1) with Backward Substitution
				for (j = n - 1; j >= 0; j--) {
					R.setCell(j, i, Z.getCell(j, i));
					for (k = 0; k < n; k++) {
						if (k != j) {
							R.setCell(j, i, (R.getCell(j, i) - (U.getCell(j, k) * R.getCell(k, i))));
						}
					}
					R.setCell(j, i, R.getCell(j, i) / U.getCell(j, j));
				}
			}

			t.stop();
			return t;
		} else {
			throw std::runtime_error("Matrix sizes do not match");
		}
	} else {
		throw std::runtime_error("Cannot perform matrix operations before initialisation");
	}
}

CUDATimer CUDAMatrix::tra(CUDAMatrix& A, CUDAMatrix& R) {
	if (A.isInitialised() && R.isInitialised()) {
		int ar = A.getNumRows();
		int ac = A.getNumCols();
		int rr = R.getNumRows();
		int rc = R.getNumCols();
		if (ac == rr) {
			A.syncDevice();

			int c1, c2;
			CUDATimer t;
			
			t.start();
			for (c1 = 0; c1 < A.getNumRows(); c1++) {
				for (c2 = 0; c2 < A.getNumCols(); c2++) {
					R.setCell(c1, c2, A.getCell(c2, c1));
				}
			}
			t.stop();

			R.syncDevice();
			return t;
		} else {
			throw std::runtime_error("Transpose matrix is the wrong size");
		}
	} else {
		throw std::runtime_error("Cannot perform matrix operations before initialisation");
	}
}

CUDATimer CUDAMatrix::exp(CUDAMatrix& A, CUDAMatrix& R) {
	if (A.isInitialised() && R.isInitialised()) {
		int ar = A.getNumRows();
		int ac = A.getNumCols();
		int rr = R.getNumRows();
		int rc = R.getNumCols();
		if (ar == ac && ac == rr && rr == rc) {
			A.syncDevice();
			CUDATimer t;
			int c1, c2;
			int n = utils::max(ar, ac);
			// Special Cases
			if (A.isDiagonal()) {
				t.start();
				for (c1 = 0; c1 < n; c1++) {
					R.setCell(c1, c1, std::exp(A.getCell(c1, c1)));
				}
				t.stop();
				R.syncDevice();
			} else if (A.isZero()) {
				t.start();
				R.setMatrix(0);
				t.stop();
				R.syncDevice();
			// Normal Case
			} else {
				// Create Matrices
				CUDAMatrix U(ar, ac);
				CUDAMatrix V(ar, ac);
				CUDAMatrix I(ar, ac); // Identity
				CUDAMatrix T(ar, ac); // Tally
				CUDAMatrix TMP(ar, ac); // Temporary
				I.setIdentity();
				I.syncDevice();
				// Get CUDA params
				cudaParams cp = getCUDAParams(ar, ac);
				// Get Pade params
				padeParams p = getPadeParams(A);
				int s = p.scale;
				int m = p.mVal;
				std::vector<CUDAMatrix*> pow = p.pow;
				// Get Pade coefficients
				std::vector<double> c = getPadeCoefficients(m);
				// Start timer
				t.start();
				// Scaling
				if (s != 0) {
					double multiplier;
					multiplier = 1.0 / std::pow(2, s);
					cudaMulScalar KERNEL_ARGS2(cp.bpg, cp.tpb) (A.d_matrix, multiplier, A.d_matrix, n);
					for (c1 = 2; c1 <= 6; c1 += 2) {
						multiplier = 1.0 / std::pow(2, (s * c1));
						cudaMulScalar KERNEL_ARGS2(cp.bpg, cp.tpb) (pow[c1]->d_matrix, multiplier, pow[c1]->d_matrix, n);
					}
				}
				// Approximation
				if (m == 3 || m == 5 || m == 7 || m == 9) {
					for (c1 = (int) (pow.size()) + 2; c1 < m - 1; c1 += 2) { //for (k = strt:2:m-1)
						cudaMul KERNEL_ARGS2(cp.bpg, cp.tpb) (pow[c1 - 2]->d_matrix, pow[2]->d_matrix, pow[c1]->d_matrix, n);
					}
					cudaMulScalar KERNEL_ARGS2(cp.bpg, cp.tpb) (I.d_matrix, c[1], U.d_matrix, n);
					cudaMulScalar KERNEL_ARGS2(cp.bpg, cp.tpb) (I.d_matrix, c[0], V.d_matrix, n);
					for (c2 = m; c2 >= 3; c2 -= 2) { //for (j = m : -2 : 3)
						cudaMulScalar KERNEL_ARGS2(cp.bpg, cp.tpb) (pow[c2 - 1]->d_matrix, c[c2], TMP.d_matrix, n);
						cudaAdd KERNEL_ARGS2(cp.bpg, cp.tpb) (U.d_matrix, TMP.d_matrix, U.d_matrix, n);
						cudaMulScalar KERNEL_ARGS2(cp.bpg, cp.tpb) (pow[c2 - 1]->d_matrix, c[c2-1], TMP.d_matrix, n);
						cudaAdd KERNEL_ARGS2(cp.bpg, cp.tpb) (V.d_matrix, TMP.d_matrix, V.d_matrix, n);
					}
					cudaMul KERNEL_ARGS2(cp.bpg, cp.tpb) (U.d_matrix, A.d_matrix, U.d_matrix, n);
				} else if (m == 13) {
					// This is the equivellent of .. 
					// U = A * (p[6] * (c[13] * p[6] + c[11] * p[4] + c[9] * p[2]) + c[7] * p[6] + c[5] * p[4] + c[3] * p[2] + c[1] * I);		RUN IN STREAM 1
					cudaMulScalar KERNEL_ARGS2(cp.bpg, cp.tpb) (pow[6]->d_matrix, c[13], T.d_matrix, n);		// p[6] * c[13] -> T			Needs new TMP var
					cudaMulScalar KERNEL_ARGS2(cp.bpg, cp.tpb) (pow[4]->d_matrix, c[11], TMP.d_matrix, n);		// p[4] * c[11] -> TMP			(Cannot be used in multiple streams)
					cudaAdd KERNEL_ARGS2(cp.bpg, cp.tpb) (T.d_matrix, TMP.d_matrix, T.d_matrix, n);				// T + TMP      -> T
					cudaMulScalar KERNEL_ARGS2(cp.bpg, cp.tpb) (pow[2]->d_matrix, c[9], TMP.d_matrix, n);		// p[2] * c[9]  -> TMP
					cudaAdd KERNEL_ARGS2(cp.bpg, cp.tpb) (T.d_matrix, TMP.d_matrix, T.d_matrix, n);				// T + TMP      -> T
					cudaMul KERNEL_ARGS2(cp.bpg, cp.tpb) (pow[6]->d_matrix, T.d_matrix, T.d_matrix, n);			// p[6] * T     -> T
					cudaMulScalar KERNEL_ARGS2(cp.bpg, cp.tpb) (pow[6]->d_matrix, c[7], TMP.d_matrix, n);		// p[6] * c[7]  -> TMP
					cudaAdd KERNEL_ARGS2(cp.bpg, cp.tpb) (T.d_matrix, TMP.d_matrix, T.d_matrix, n);				// T + TMP      -> T
					cudaMulScalar KERNEL_ARGS2(cp.bpg, cp.tpb) (pow[4]->d_matrix, c[5], TMP.d_matrix, n);		// p[4] * c[5]  -> TMP
					cudaAdd KERNEL_ARGS2(cp.bpg, cp.tpb) (T.d_matrix, TMP.d_matrix, T.d_matrix, n);				// T + TMP      -> T
					cudaMulScalar KERNEL_ARGS2(cp.bpg, cp.tpb) (pow[2]->d_matrix, c[3], TMP.d_matrix, n);		// p[2] * c[3]  -> TMP
					cudaAdd KERNEL_ARGS2(cp.bpg, cp.tpb) (T.d_matrix, TMP.d_matrix, T.d_matrix, n);				// T + TMP      -> T
					cudaMulScalar KERNEL_ARGS2(cp.bpg, cp.tpb) (I.d_matrix, c[1], TMP.d_matrix, n);				// I * c[1]     -> TMP
					cudaAdd KERNEL_ARGS2(cp.bpg, cp.tpb) (T.d_matrix, TMP.d_matrix, T.d_matrix, n);				// T + TMP      -> T
					cudaMul KERNEL_ARGS2(cp.bpg, cp.tpb) (A.d_matrix, T.d_matrix, U.d_matrix, n);				// A * T        -> U
					// This is the equivellent of ..
					//V = p[6] * (c[12] * p[6] + c[10] * p[4] + c[8] * p[2]) + c[6] * p[6] + c[4] * p[4] + c[2] * p[2] + c[0] * I;				RUN IN STREAM 2
					cudaMulScalar KERNEL_ARGS2(cp.bpg, cp.tpb) (pow[6]->d_matrix, c[12], T.d_matrix, n);		// p[6] * c[12] -> T
					cudaMulScalar KERNEL_ARGS2(cp.bpg, cp.tpb) (pow[4]->d_matrix, c[10], TMP.d_matrix, n);		// p[4] * c[10] -> TMP
					cudaAdd KERNEL_ARGS2(cp.bpg, cp.tpb) (T.d_matrix, TMP.d_matrix, T.d_matrix, n);				// T + TMP      -> T
					cudaMulScalar KERNEL_ARGS2(cp.bpg, cp.tpb) (pow[2]->d_matrix, c[8], TMP.d_matrix, n);		// p[2] * c[8]  -> TMP
					cudaAdd KERNEL_ARGS2(cp.bpg, cp.tpb) (T.d_matrix, TMP.d_matrix, T.d_matrix, n);				// T + TMP      -> T
					cudaMul KERNEL_ARGS2(cp.bpg, cp.tpb) (pow[6]->d_matrix, T.d_matrix, T.d_matrix, n);			// p[6]			-> T
					cudaMulScalar KERNEL_ARGS2(cp.bpg, cp.tpb) (pow[6]->d_matrix, c[6], TMP.d_matrix, n);		// p[6] * c[6]  -> TMP
					cudaAdd KERNEL_ARGS2(cp.bpg, cp.tpb) (T.d_matrix, TMP.d_matrix, T.d_matrix, n);				// T + TMP      -> T
					cudaMulScalar KERNEL_ARGS2(cp.bpg, cp.tpb) (pow[4]->d_matrix, c[4], TMP.d_matrix, n);		// p[4] * c[4]  -> TMP
					cudaAdd KERNEL_ARGS2(cp.bpg, cp.tpb) (T.d_matrix, TMP.d_matrix, T.d_matrix, n);				// T + TMP      -> T
					cudaMulScalar KERNEL_ARGS2(cp.bpg, cp.tpb) (pow[2]->d_matrix, c[2], TMP.d_matrix, n);		// p[2] * c[2]  -> TMP
					cudaAdd KERNEL_ARGS2(cp.bpg, cp.tpb) (T.d_matrix, TMP.d_matrix, T.d_matrix, n);				// T + TMP      -> T
					cudaMulScalar KERNEL_ARGS2(cp.bpg, cp.tpb) (I.d_matrix, c[0], TMP.d_matrix, n);				// I * c[0]     -> TMP
					cudaAdd KERNEL_ARGS2(cp.bpg, cp.tpb) (T.d_matrix, TMP.d_matrix, V.d_matrix, n);				// T + TMP      -> V
				}
				// This is the equivellent of ..
				// R = (V - U) / (2 * U) + I;  ||?? R = (-U + V) / (U + V);
				cudaSub KERNEL_ARGS2(cp.bpg, cp.tpb) (V.d_matrix, U.d_matrix, T.d_matrix, n);
				cudaMulScalar KERNEL_ARGS2(cp.bpg, cp.tpb) (U.d_matrix, 2, TMP.d_matrix, n);
				//cudaInv KERNEL_ARGS2(cp.bpg, cp.tpb) (TMP.d_matrix, TMP.d_matrix, n); // TEMP CODE BELOW
				T.syncHost();
				CUDAMatrix::inv(T, T);
				T.syncDevice();
				//
				cudaMul KERNEL_ARGS2(cp.bpg, cp.tpb) (T.d_matrix, TMP.d_matrix, T.d_matrix, n);
				cudaAdd KERNEL_ARGS2(cp.bpg, cp.tpb) (T.d_matrix, I.d_matrix, R.d_matrix, n);
				// Squaring
				for (int k = 0; k < s; k++) {
					cudaMul KERNEL_ARGS2(cp.bpg, cp.tpb) (R.d_matrix, R.d_matrix, R.d_matrix, n);
				}
				cudaThreadSynchronize();
				t.stop();
				R.syncHost();
			}
			return t;
		} else {
			throw std::runtime_error("Matrix sizez do not match");
		}
	} else {
		throw std::runtime_error("Cannot perform matrix operations before initialisation");
	}
}

CUDATimer CUDAMatrix::abs(CUDAMatrix& A, CUDAMatrix& R) {
	if (A.isInitialised() && R.isInitialised()) {
		int ar = A.getNumRows();
		int ac = A.getNumCols();
		int rr = R.getNumRows();
		int rc = R.getNumCols();
		if (ar == ac && ac == rr && rr == rc) {
			A.syncDevice();

			cudaParams cp = getCUDAParams(ar, ac);
			CUDATimer t;

			t.start();
			cudaAbs KERNEL_ARGS2(cp.bpg, cp.tpb) (A.d_matrix, R.d_matrix, ar);
			t.stop();

			R.syncHost();
			return t;
		} else {
			throw std::runtime_error("Matrix sizes do not match");
		}
	} else {
		throw std::runtime_error("Cannot perform matrix operations before initialisation");
	}
}

// BOOLEANS

bool CUDAMatrix::isInitialised() {
	return initialised;
}

bool CUDAMatrix::isSquare() {
	if (initialised) {
		if (numCols == numRows) {
			return true;
		} else {
			return false;
		}
	} else {
		throw std::runtime_error("Cannot perform matrix operations before initialisation");
	}
}

bool CUDAMatrix::isDiagonal() {
	if (initialised) {
		if (!isSquare()) {
			return false;
		}
		for (int c1 = 0; c1 < numRows; c1++) {
			for (int c2 = 0; c2 < numCols; c2++) {
				if (c1 != c2 && getCell(c1, c2) != 0.0) {
					return false;
				}
			}
		}
		return true;
	} else {
		throw std::runtime_error("Cannot perform matrix operations before initialisation");
	}
}

bool CUDAMatrix::isIdentity() {
	if (initialised) {
		for (int c1 = 0; c1 < numRows; c1++) {
			for (int c2 = 0; c2 < numCols; c2++) {
				if ((c1 != c2 && getCell(c1, c2) != 0.0) || (c1 == c2 && getCell(c1, c2) != 1.0)) {
					return false;
				}
			}
		}
		return true;
	} else {
		throw std::runtime_error("Cannot perform matrix operations before initialisation");
	}
}

bool CUDAMatrix::isZero() {
	if (initialised) {
		for (int c1 = 0; c1 < numRows; c1++) {
			for (int c2 = 0; c2 < numCols; c2++) {
				if (getCell(c1, c2) != 0.0) {
					return false;
				}
			}
		}
		return true;
	} else {
		throw std::runtime_error("Cannot perform matrix operations before initialisation");
	}
}

bool CUDAMatrix::isSmall() {
	return utils::max(numRows, numCols) < 150;
}

bool CUDAMatrix::isComplex() {
	std::complex<double> cell;
	for (int c1 = 0; c1 < numEls; c1++) {
		cell = getCell(c1);
		if (cell.imag() != 0.0) {
			return true;
		}
	}
	return false;
}

// SETTERS

void CUDAMatrix::setCell(int row, int col, std::complex<double> val) {
	if (isInitialised()) {
		h_matrix[numCols * row + col] = val;
	} else {
		throw std::runtime_error("Cannot perform matrix operations before initialisation");
	}
}

void CUDAMatrix::setCell(int i, std::complex<double> val) {
	if (isInitialised()) {
		h_matrix[i] = val;
	} else {
		throw std::runtime_error("Cannot perform matrix operations before initialisation");
	}
}

void CUDAMatrix::setMatrix(std::complex<double> val) {
	if (isInitialised()) {
		for (int c1 = 0; c1 < getNumEls(); c1++) {
			h_matrix[c1] = val;
		}
	} else {
		throw std::runtime_error("Cannot perform matrix operations before initialisation");
	}
}

void CUDAMatrix::setMatrix(std::complex<double>* inMatrix) {
	if (isInitialised()) {
		for (int c1 = 0; c1 < numEls; c1++) {
			h_matrix[c1] = inMatrix[c1];
		}
	} else {
		throw std::runtime_error("Cannot perform matrix operations before initialisation");
	}
}

void CUDAMatrix::setMatrix(std::initializer_list<std::complex<double>> inMatrix) {
	if (isInitialised()) {
		if (inMatrix.size() == getNumEls()) {
			std::copy(inMatrix.begin(), inMatrix.end(), h_matrix);
		} else {
			throw std::runtime_error("Initialiser-list size does not match matrix size");
		}
	} else {
		throw std::runtime_error("Cannot perform matrix operations before initialisation");
	}
}

void CUDAMatrix::setIdentity() {
	if (isInitialised()) {
		int row, col;
		for (int c1 = 0; c1 < getNumEls(); c1++) {
			row = getCurRow(c1);
			col = getCurCol(c1);
			if (row == col) {
				h_matrix[c1] = 1;
			} else {
				h_matrix[c1] = 0;
			}
		}
	} else {
		throw std::runtime_error("Cannot perform matrix operations before initialisation");
	}
}

void CUDAMatrix::setRandomDouble(double min, double max) {
	if (isInitialised()) {
		double r;
		std::default_random_engine rng((unsigned int) (time(0)));
		std::uniform_real_distribution<double> gen(min, max);
		for (int c1 = 0; c1 < numEls; c1++) {
			r = gen(rng);
			setCell(c1, r);
		}
	} else {
		throw std::runtime_error("Cannot perform matrix operations before initialisation");
	}
}

void CUDAMatrix::setRandomInt(int min, int max) {
	if (isInitialised()) {
		int r;
		std::default_random_engine rng((unsigned int) (time(0)));
		std::uniform_int_distribution<int> gen(min, max);
		for (int c1 = 0; c1 < numEls; c1++) {
			r = gen(rng);
			setCell(c1, r);
		}
	} else {
		throw std::runtime_error("Cannot perform matrix operations before initialisation");
	}
}

// GETTERS

double CUDAMatrix::getNorm(int n) {
	int c1, c2;
	double sum, max = 0;
	if (n == 1) {
		// 1 Norm
		for (c1 = 0; c1 < numCols; c1++) {
			sum = 0;
			for (c2 = 0; c2 < numRows; c2++) {
				sum += std::abs(getCell(c2, c1));
			}
			if (std::norm(sum) > std::norm(max)) {
				max = sum;
			}
		}
		return max;
	} else if (n == INFINITY) {
		// Inf Norm
		for (c1 = 0; c1 < numRows; c1++) {
			sum = 0;
			for (c2 = 0; c2 < numCols; c2++) {
				sum += std::abs(getCell(c2, c1));
			}
			if (std::norm(sum) > std::norm(max)) {
				max = sum;
			}
		}
		return max;
	} else {
		//// Euclidian									Not called from anywhere. Requires SVD implementation to work.
		//sum = 0;
		//for (c1 = 0; c1 < numEls; c1++) {
		//	sum += std::pow(getCell(c1), n);
		//}
		//return std::pow(sum, 1.0 / n);
		return -1;
	}
}

int CUDAMatrix::getCurRow(int i) {
	if (isInitialised()) {
		return (int) (floor(i / numCols));
	} else {
		throw std::runtime_error("Cannot perform matrix operations before initialisation");
	}
}

int CUDAMatrix::getCurCol(int i) {
	if (isInitialised()) {
		return (int) (i - (numCols*getCurRow(i)));
	} else {
		throw std::runtime_error("Cannot perform matrix operations before initialisation");
	}
}

std::complex<double> CUDAMatrix::getCell(int row, int col) {
	if (isInitialised()) {
		return h_matrix[row*numCols + col];
	} else {
		throw std::runtime_error("Cannot perform matrix operations before initialisation");
	}
}

std::complex<double> CUDAMatrix::getCell(int i) {
	if (isInitialised()) {
		return h_matrix[i];
	} else {
		throw std::runtime_error("Cannot perform matrix operations before initialisation");
	}
}

std::complex<double>* CUDAMatrix::getMatrix() {
	if (isInitialised()) {
		return h_matrix;
	} else {
		throw std::runtime_error("Cannot perform matrix operations before initialisation");
	}
}

int CUDAMatrix::getNumRows() {
	if (isInitialised()) {
		return numRows;
	} else {
		throw std::runtime_error("Cannot perform matrix operations before initialisation");
	}
}

int CUDAMatrix::getNumCols() {
	if (isInitialised()) {
		return numCols;
	} else {
		throw std::runtime_error("Cannot perform matrix operations before initialisation");
	}
}

int CUDAMatrix::getNumEls() {
	if (isInitialised()) {
		return numEls;
	} else {
		throw std::runtime_error("Cannot perform matrix operations before initialisation");
	}
}

size_t CUDAMatrix::getSize() {
	if (isInitialised()) {
		return size;
	} else {
		throw std::runtime_error("Cannot perform matrix operations before initialisation");
	}
}

// UTILS

int utils::getNumDigits(double x) {
	if (x > 1.0 || x < -1.0) {
		return (int) (floor(log10(abs(x))) + 1);
	}
	return 1;
}

int utils::max(int x, int y) {
	if (x > y) {
		return x;
	} else {
		return y;
	}
}

double utils::max(double x, double y) {
	if (x > y) {
		return x;
	} else {
		return y;
	}
}

int utils::min(int x, int y) {
	if (x < y) {
		return x;
	} else {
		return y;
	}
}

double utils::min(double x, double y) {
	if (x < y) {
		return x;
	} else {
		return y;
	}
}

// OPERATOR OVERRIDES


std::ostream& operator<<(std::ostream& oStream, CUDAMatrix& A) {
	if (A.isInitialised()) {
		// Init
		std::complex<double> cell;
		bool isComplex = A.isComplex();
		bool scientific = false;
		int c1, c2, r, i;
		int realLength = 0, imagLength = 0, exp = 0;
		double divider;
		int precision = 0;
		int maxFixedDigits = 4;
		// Get info
		for (c1 = 0; c1 < A.getNumEls(); c1++) {
			cell = A.getCell(c1);
			// Check if it's decimal
			if ((cell.real() - (int) (cell.real())) != 0.0 ||
				(cell.imag() - (int) (cell.imag())) != 0.0) {
				precision = 4;
			}
			// Get maximum exponent
			r = utils::getNumDigits(cell.real());
			i = utils::getNumDigits(cell.imag());
			if (r - 1 > exp) {
				exp = r - 1;
				realLength = r;
			}
			if (i - 1 > exp) {
				exp = i - 1;
				imagLength = i;
				if (abs(cell.imag() == 1.0)) {
					imagLength++;
				}
			}
		}
		// Check if the output should be in fixed or scientific form
		if (exp >= maxFixedDigits) {
			scientific = true;
		}
		// Get divider for scientific form
		divider = std::pow(10, exp);

		// Output name and multiplier
		oStream << " = ";
		if (scientific) {
			oStream << "(10 ^ " << exp << ") *";
		}
		// Output cell
		oStream << std::endl << std::setprecision(precision) << std::fixed;
		for (c1 = 0; c1 < A.getNumEls(); c1++) {
			cell = A.getCell(c1);
			oStream << "| ";
			// Spacing and formatting for scientific/fixed
			if (scientific) {
				cell /= divider;
			} else {
				r = utils::getNumDigits(cell.real());
				for (c2 = 0; c2 < (realLength - r); c2++) {
					oStream << " ";
				}
			}
			// Output real
			oStream << cell.real() << " ";
			// Output complex
			if (isComplex) {
				if (cell.imag() != 0.0) {
					if (cell.imag() > 0.0) {
						oStream << "+ ";
					} else {
						oStream << "- ";
					}
					if (abs(cell.imag()) != 1.0) {
						oStream << std::abs(cell.imag());
					} else {
						oStream << " ";
					}
					oStream << "i ";
				} else {
					i = utils::getNumDigits(cell.imag());
					for (c2 = 0; c2 < imagLength + 3; c2++) {
						oStream << " ";
					}
				}
			}
			// Output new line if row end reached
			if (A.getCurRow(c1 + 1) > A.getCurRow(c1)) {
				oStream << "|";
				if (A.getCurRow(c1 + 1) < A.getNumRows()) {
					oStream << std::endl;
				}
			}
		}
		oStream << std::endl;
		return oStream;
		//	// Get precision
		//	cell = A.getCell(c1);
		//	if ((cell - (int) (cell)) != 0.0) {
		//		precision = 5;
		//	}
		//	// Get maximum number length
		//	length = utils::getNumDigits(cell);
		//	if (length > maxLength) {
		//		maxLength = length;
		//	}
		//}
		//for (c1 = 0; c1 < A.getNumEls(); c1++) {
		//	cell = A.getCell(c1);
		//	// Remove negative zeros
		//	if (cell == 0.0) {
		//		cell = 0;
		//	}
		//	oStream << "| ";
		//	// Add whitespace if shorter than maxLength
		//	length = utils::getNumDigits(cell);
		//	for (c2 = 0; c2 < (maxLength - length); c2++) {
		//		oStream << " ";
		//	}
		//	// Output number
		//	oStream << std::setprecision(precision) << std::fixed << cell << " ";
		//	// Output new line if row end reached
		//	if (A.getCurRow(c1 + 1) > A.getCurRow(c1)) {
		//		oStream << "|";
		//		if (A.getCurRow(c1 + 1) < A.getNumRows()) {
		//			oStream << std::endl;
		//		}
		//	}
	} else {
		throw std::runtime_error("Cannot perform matrix operations before initialisation");
	}
}