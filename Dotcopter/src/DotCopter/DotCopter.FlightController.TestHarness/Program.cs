﻿using System.IO;
using DotCopter.Commons.Logging;
using DotCopter.Commons.Serialization;
using DotCopter.Commons.Utilities;
using DotCopter.ControlAlgorithms.Implementations.Mixing;
using DotCopter.ControlAlgorithms.Implementations.PID;
using DotCopter.ControlAlgorithms.Mixing;
using DotCopter.ControlAlgorithms.PID;
using DotCopter.Hardware.Accelerometer;
using DotCopter.Hardware.Gyro;
using DotCopter.Hardware.Motor;
using DotCopter.Hardware.Radio;

namespace DotCopter.FlightController.TestHarness
{
    public class Program
    {
        public static void Main()
        {
            Motor testMotor = new TestMotor();
            MotorMixer mixer = new QuadMixer(testMotor, testMotor, testMotor, testMotor);
            ILogger logger = new PersistenceWriter(new MemoryStream(), new TelemetryFormatter());
            Gyro gyro = new TestGyro();
            Accelerometer accelerometer = new TestAccelerometer(); 
            Radio radio = new TestRadio();
            PIDSettings[] pidSettings = GetPIDSettings();
            AxesController axesController = new AxesController(pidSettings[0], pidSettings[1], pidSettings[2], true);
            ControllerLoopSettings loopSettings = GetLoopSettings();
            Controller controller = new Controller(mixer, axesController, gyro, accelerometer, radio, loopSettings, GetMotorSettings(), logger);
        }

        private static PIDSettings[] GetPIDSettings()
        {
 
            PIDSettings pitchSettings = new PIDSettings
            {
                DerivativeGain = 3.2F,
                IntegralGain = 0F,
                ProportionalGain = -.5F,
                WindupLimit = 2000F
            };

            PIDSettings rollSettings = new PIDSettings
            {
                DerivativeGain = 3.2F,
                IntegralGain = 0F,
                ProportionalGain = -.5F,
                WindupLimit = 2000F
            };

            PIDSettings yawSettings = new PIDSettings
            {
                DerivativeGain = 3.2F,
                IntegralGain = 0F,
                ProportionalGain = -.5F,
                WindupLimit = 2000F
            };
            return new[] { pitchSettings, rollSettings, yawSettings };
        }

        private static ControllerLoopSettings GetLoopSettings()
        {
            return new ControllerLoopSettings(
                200,         //radioLoopFrequency
                200,        //sensorLoopFrequency
                200,        //controlAlgorithmFrequency
                200,        //motorLoopFrequency
                200,          //telemetryLoopFrequency
                10000000);  //loopUnit
        }

        private static MotorSettings GetMotorSettings()
        {
            return new MotorSettings(
                new Scale(0, 10000, 1000000), //motor scale
                0,                      //safeOutput
                15,                     //minimumOutput
                95);                    //maximimOutput
        }

        private static RadioSettings GetRadioSettings()
        {
            return new RadioSettings(
                new Scale(-1000, 0.1F, 0), //throttle scale (ax + b)
                new Scale(-1500, 0.0000008F, 0, 0, 0), //Axes Scale (ax3 + bx2 + cx + d)
                0.5F); //RadioSensitivityFactor
        }

        private static float GetGyroFactor()
        {
            return 287.5F;
        }
    }
}