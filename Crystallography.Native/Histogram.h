/*--------------*/
/* Histogram.h */
/*--------------*/
#pragma once

extern "C" {
#ifdef HISTOGRAM_EXPORTS
#define HISTOGRAM_API __declspec(dllexport)
#else
#define HISTOGRAM_API __declspec(dllimport)
#endif
	HISTOGRAM_API
		void _Histogram(
            int width, int height,
            double centerX, double centerY,
            double pixSizeX, double pixSizeY,
            double fd,
            double ksi, double tau, double phi,
            double SpericalRadiusInverse,
            double Intensity[], BYTE IsValid[],
            int yMin, int yMax,
            double startAngle, double stepAngle,
            double r2[], int r2len,
            double profile[], double pixels[]);
} // extern "C"

