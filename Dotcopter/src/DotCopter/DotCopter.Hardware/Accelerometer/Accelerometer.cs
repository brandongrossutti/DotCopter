using System;
using DotCopter.Avionics;
using Microsoft.SPOT;

namespace DotCopter.Hardware.Accelerometer
{
    public abstract class Accelerometer
    {
        public AircraftPrincipalAxes Axes;
        public abstract void Update();
        public abstract void Zero();

        public Accelerometer(AircraftPrincipalAxes axes)
        {
            Axes = axes;
        }

    }
}
