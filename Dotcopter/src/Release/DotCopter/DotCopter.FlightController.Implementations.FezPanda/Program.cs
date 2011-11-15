using System.Threading;
using DotCopter.Commons.Utilities;
using DotCopter.ControlAlgorithms.Implementations.Mixing;
using DotCopter.ControlAlgorithms.Implementations.PID;
using DotCopter.ControlAlgorithms.Mixing;
using DotCopter.ControlAlgorithms.PID;
using DotCopter.Hardware.Gyro;
using DotCopter.Hardware.Implementations.Bus;
using DotCopter.Hardware.Implementations.Gyro;
using DotCopter.Hardware.Implementations.Motor;
using DotCopter.Hardware.Implementations.Radio;
using DotCopter.Hardware.Motor;
using DotCopter.Hardware.Radio;
using DotCopter.Physics;
using GHIElectronics.NETMF.FEZ;
using Microsoft.SPOT.Hardware;
using PWM = DotCopter.Hardware.Implementations.GHIElectronics.PWM.PWM;

namespace DotCopter.FlightController.Implementations.FezPanda
{
    public class Program
    {
        public static void Main()
        {
            MotorSettings motorSettings = GetMotorSettings();
            Motor frontMotor = new PWMMotor(new PWM((Cpu.Pin)FEZ_Pin.PWM.Di6, 4000000), motorSettings);
            Motor rearMotor = new PWMMotor(new PWM((Cpu.Pin)FEZ_Pin.PWM.Di9, 4000000), motorSettings);
            Motor rightMotor = new PWMMotor(new PWM((Cpu.Pin)FEZ_Pin.PWM.Di10, 4000000), motorSettings);
            Motor leftMotor = new PWMMotor(new PWM((Cpu.Pin)FEZ_Pin.PWM.Di8, 4000000), motorSettings);
            MotorMixer mixer = new QuadMixer(frontMotor, rearMotor, leftMotor, rightMotor);
            
            //Sensors
            I2CBus twiBus = new I2CBus();
            Gyro gyro = new ITG3200(twiBus);
            Thread.Sleep(2000);
            gyro.Zero();
            Radio radio = new DotCopterRadio(twiBus, GetRadioSettings());

            //Control Algoriths
            PID pid = new PIDWindup(GetPIDSettings());
            ControllerLoopSettings loopSettings = GetLoopSettings();
            new Controller(mixer, pid, gyro, radio, loopSettings);

        }

        private static PIDSettings GetPIDSettings()
        {
            return new PIDSettings
                       {
                           ProportionalGain =   new Vector3D {X = .2F,  Y = .2F,    Z = .5F},
                           IntegralGain =       new Vector3D {X = 0F,   Y = 0F,     Z = 0F},
                           DerivativeGain =     new Vector3D {X = .4F,  Y = .4F,    Z = .0F},
                           WindupLimit =        new Vector3D {X = 10F,  Y = 10F,    Z = 10F},
                       };
        }

        private static ControllerLoopSettings GetLoopSettings()
        {
            return new ControllerLoopSettings(
                10,        //sensorLoopFrequency
                50,          //telemetryLoopFrequency
                10000000,
                20);  //loopUnit
        }

        private static MotorSettings GetMotorSettings()
        {
            return new MotorSettings(
                new Scale(0, 10000, 1000000), //motor scale
                0,                      //safeOutput
                20,                     //minimumOutput
                80);                    //maximimOutput
        }

        private static RadioSettings GetRadioSettings()
        {
            return new RadioSettings(
                new Scale(-1000, 1 / 10F, 0), //throttle scale (ax + b)
                new Scale(-1500, 1 / 1250000F, 0, 0, 0), //Axes Scale (ax3 + bx2 + cx + d)
                0.7F); //RadioSensitivityFactor
        }
    }
}