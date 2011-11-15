/*
 * DotCopter_AVR.c
 *
 * Created: 3/4/2011 3:29:00 PM
 *  Author: luke
 */ 

#include <avr/io.h>
#include "Physics/Physics.h"

int main(void)
{
	
	Vector3D a(0,0,0);
	Vector3D b(1,1,1);
	Vector3D c;
	
	c = a + b;
	
    while(1)
    {
    }
}