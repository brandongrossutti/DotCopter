using System;
using DotCopter.Avionics;
using DotCopter.Hardware.Accelerometer;
using DotCopter.Hardware.Gyro;

namespace DotCopter.FlightController.TestHarness
{
    public class TestAccelerometer : Accelerometer
    {
        private readonly Random _random;
        public TestAccelerometer() : base(new AircraftPrincipalAxes {Pitch = 0, Roll = 0, Yaw = 0})
        {
            _random = new Random();
        }
        
        public override void Update()
        {
            Axes.Pitch = _random.Next();
            Axes.Roll = _random.Next();
            Axes.Yaw = _random.Next();   
        }

        public override void Zero()
        {
            throw new NotImplementedException();
        }
    }
}