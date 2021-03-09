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
#include "CUDATimer.cuh"

void CUDATimer::start() {
	clear();
	cudaEventCreate(&t1);
	cudaEventCreate(&t2);
	cudaEventRecord(t1, 0);
}

void CUDATimer::stop() {
	cudaEventRecord(t2, 0);
	cudaEventSynchronize(t2);
	cudaEventElapsedTime(&time, t1, t2);
	cudaEventDestroy(t1);
	cudaEventDestroy(t2);
}

void CUDATimer::clear() {
	time = 0;
}

float CUDATimer::getTime() {
	return time;
}

std::ostream& operator<<(std::ostream& oStream, CUDATimer& t) {
	oStream << std::setprecision(10) << std::fixed << t.getTime()/1000 << "s" << std::endl;
	return oStream;
}