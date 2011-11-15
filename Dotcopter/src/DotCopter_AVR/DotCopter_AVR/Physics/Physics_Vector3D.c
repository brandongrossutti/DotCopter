/*
 * Physics_Vector3D.c
 *
 * Created: 3/4/2011 3:30:48 PM
 *  Author: luke
 */ 

#include <math.h>
#include "Physics_Vector3D.h"

Vector3D::Vector3D(){}
	
Vector3D::Vector3D( double x, double y, double z ) : X(x), Y(y), Z(z){}

Vector3D Vector3D::operator+( Vector3D &other )
{
	Vector3D output;
	output.X = X + other.X;
	output.Y = Y + other.Y;
	output.Z = Z + other.Z;
	return output;
}

Vector3D Vector3D::operator-( Vector3D &other )
{
	Vector3D output;
	output.X = X + other.X;
	output.Y = Y + other.Y;
	output.Z = Z + other.Z;
	return output;
}
