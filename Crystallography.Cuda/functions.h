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

#pragma once

#include "src/CUDAMatrix.cuh"
int main(int argc, char** argv);

extern "C" {
#ifdef CUDAFUNCS_EXPORTS
#define CUDA_FUNCS_API __declspec(dllexport)
#else
#define CUDA_FUNCS_API __declspec(dllimport)
#endif

	CUDA_FUNCS_API void MatrixExponential_Cuda(int dim, double mat[], double result[]);
	CUDA_FUNCS_API void CBEDSolver_MatExp_Cuda(int gDim, double _potential[], double _phi0[], int tDim, double tStart, double tStep, double result[]);



} // extern "C"
