/*
 * Physics_Vector3D.h
 *
 * Created: 3/4/2011 3:30:16 PM
 *  Author: luke
 */ 


#ifndef PHYSICS_VECTOR3D_H_
#define PHYSICS_VECTOR3D_H_

class Vector3D
{
public:
	double X;
	double Y;
	double Z;
	Vector3D operator+(Vector3D &other);
	Vector3D operator-(Vector3D &other);
	Vector3D();
	Vector3D(double x, double y, double z);
};	


#endif /* PHYSICS_VECTOR3D_H_ */