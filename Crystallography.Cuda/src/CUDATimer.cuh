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
#ifndef timer_h
#define timer_h
// Inlcude C/C++ stuff
#include <iostream>
#include <iomanip>
// Include CUDA stuff
#include "cuda.h"
#include "cuda_runtime.h"
#include "device_launch_parameters.h"
#include "cuda_intellisense.h"

class CUDATimer {
private:
	float time;
	cudaEvent_t t1, t2;
public:
	void start();
	void stop();
	void clear();
	float getTime();
};

// OPERATOR OVERRIDES
std::ostream& operator<<(std::ostream& oStream, CUDATimer& A);

#endif