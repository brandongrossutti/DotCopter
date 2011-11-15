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
using DotCopter.Hardware.Implementations.SecretLabs.PWM;
using DotCopter.Hardware.Motor;
using DotCopter.Hardware.Radio;
using DotCopter.Physics;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

namespace DotCopter.FlightController.Implementations.Netduino
{
    public class Program
    {
        public static void Main()
        {

            MotorSettings motorSettings = GetMotorSettings();
            Motor frontMotor = new PWMMotor(new PWM(Pins.GPIO_PIN_D10, 2 * 1000), motorSettings);
            Motor rearMotor = new PWMMotor(new PWM(Pins.GPIO_PIN_D5, 2 * 1000), motorSettings);
            Motor rightMotor = new PWMMotor(new PWM(Pins.GPIO_PIN_D9, 2 * 1000), motorSettings);
            Motor leftMotor = new PWMMotor(new PWM(Pins.GPIO_PIN_D6, 2 * 1000), motorSettings);
            MotorMixer mixer = new QuadMixer(frontMotor, rearMotor, leftMotor, rightMotor);

            //Sensors
            I2CBus twiBus = new I2CBus();
            Gyro gyro = new ITG3200(twiBus);
            gyro.Zero();
            Radio radio = new DotCopterRadio(twiBus, GetRadioSettings());

            //Control Algoriths
            PID pid = new PIDWindup(GetPIDSettings());
            ControllerLoopSettings loopSettings = GetLoopSettings();
            OutputPort led = new OutputPort(Pins.ONBOARD_LED,true);
            new Controller(mixer, pid, gyro, radio, loopSettings);

        }

        private static PIDSettings GetPIDSettings()
        {
            return new PIDSettings
                       {
                           ProportionalGain =   new Vector3D { X = .1F,     Y = .1F,    Z = .2F },
                           IntegralGain =       new Vector3D { X = .0F,      Y = .0F,     Z = .0F },
                           DerivativeGain =     new Vector3D { X = -.02F,     Y = -.02F,    Z = .0F },
                           WindupLimit =        new Vector3D { X = 10F,     Y = 10F,    Z = 10F },
                       };
        }

        private static ControllerLoopSettings GetLoopSettings()
        {
            return new ControllerLoopSettings(
                10,        //sensorLoopFrequency
                1000,          //telemetryLoopFrequency
                1000,
                20);  //loopUnit
        }

        private static MotorSettings GetMotorSettings()
        {
            return new MotorSettings(
                new Scale(0, 10, 1000), //motor scale
                0,                      //safeOutput
                20,                     //minimumOutput
                80);                    //maximimOutput
        }

        private static RadioSettings GetRadioSettings()
        {
            return new RadioSettings(
                new Scale(-1000, 1 / 10F, 0), //throttle scale (ax + b)
                //new Scale(-1500, 1 / 1250000F, 0, 0, 0), //Axes Scale (ax3 + bx2 + cx + d)
                new Scale(-1500, 1 / 10F, 0), //Axes Scale (ax3 + bx2 + cx + d)
                .75F); //RadioSensitivityFactor
        }
    }
}