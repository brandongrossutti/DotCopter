using System.IO;
using System.Threading;
using DotCopter.Commons.Logging;
using DotCopter.Commons.Serialization;
using DotCopter.Commons.Utilities;
using DotCopter.ControlAlgorithms.Implementations.Mixing;
using DotCopter.ControlAlgorithms.Mixing;
using DotCopter.ControlAlgorithms.PID;
using DotCopter.Hardware.Accelerometer;
using DotCopter.Hardware.Gyro;
using DotCopter.Hardware.Implementations.Accelerometer;
using DotCopter.Hardware.Implementations.Bus;
using DotCopter.Hardware.Implementations.GHIElectronics.FEZPanda;
using DotCopter.Hardware.Implementations.GHIElectronics.Storage;
using DotCopter.Hardware.Implementations.Gyro;
using DotCopter.Hardware.Implementations.Motor;
using DotCopter.Hardware.Implementations.Radio;
using DotCopter.Hardware.Implementations.Storage;
using DotCopter.Hardware.Motor;
using DotCopter.Hardware.Radio;
using DotCopter.Hardware.Storage;
using GHIElectronics.NETMF.Hardware;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using PWM = DotCopter.Framework.Implementations.GHIElectronics.PWM.PWM;

namespace DotCopter.FlightController.Implementations.FezPanda
{
    public class Program
    {
        public static void Main()
        {
            OutputPort led = new OutputPort((Cpu.Pin)FezPin.Digital.LED, false);
            Debug.EnableGCMessages(false);
            //Initialize motors first
            MotorSettings motorSettings = GetMotorSettings();
            Motor frontMotor = new PWMMotor(new PWM((Cpu.Pin)FezPin.PWM.Di6, 4000000), motorSettings);
            Motor rearMotor = new PWMMotor(new PWM((Cpu.Pin)FezPin.PWM.Di9, 4000000), motorSettings);
            Motor rightMotor = new PWMMotor(new PWM((Cpu.Pin)FezPin.PWM.Di10, 4000000), motorSettings);
            Motor leftMotor = new PWMMotor(new PWM((Cpu.Pin)FezPin.PWM.Di8, 4000000), motorSettings);
            MotorMixer mixer = new QuadMixer(frontMotor, rearMotor, leftMotor, rightMotor);

            //Telemetry
            /*
            ISDCard sdCard = (ISDCard) new SDCard();
            if (Configuration.DebugInterface.GetCurrent() == Configuration.DebugInterface.Port.USB1)
                sdCard.MountFileSystem();
            else
                new TelemetryPresenter(sdCard,(Cpu.Pin) FezPin.Digital.LED);
             */
            ILogger logger = new NullLogger();//PersistenceWriter(new FileStream(@"\SD\telemetry.bin",FileMode.OpenOrCreate), new TelemetryFormatter());

            //Sensors
            I2CBus twiBus = new I2CBus();
            Gyro gyro = new ITG3200(twiBus, GetGyroSettings());
            Thread.Sleep(2000);
            gyro.Zero();
            Accelerometer accelerometer = new BMA180(twiBus, GetAccelerometerSettings());
            Radio radio = new DefaultRadio(twiBus, GetRadioSettings());

            //Control Algoriths
            PIDSettings[] pidSettings = GetPIDSettings();
            AxesController axesController = new AxesController(pidSettings[0], pidSettings[1], pidSettings[2], false);
            ControllerLoopSettings loopSettings = GetLoopSettings();
            led.Write(true);
            var controller = new Controller(mixer, axesController, gyro, accelerometer, radio, loopSettings, GetMotorSettings(),logger);

            //controller.Start();
        }

        private static PIDSettings[] GetPIDSettings()
        {
            
            PIDSettings pitchSettings = new PIDSettings
                                            {
                                                ProportionalGain = .2F,
                                                IntegralGain = .0F,
                                                DerivativeGain = .4F,
                                                WindupLimit = 10F
                                            };

            PIDSettings rollSettings = new PIDSettings
                                           {
                                               ProportionalGain = .2F,
                                               IntegralGain = .0F,
                                               DerivativeGain = .4F,
                                               WindupLimit = 10F
                                           };

            PIDSettings yawSettings = new PIDSettings
                                          {
                                              ProportionalGain = .5F,
                                              IntegralGain = 0F,
                                              DerivativeGain = 0F,
                                              WindupLimit = 10F
                                          };
            return new[] { pitchSettings, rollSettings, yawSettings };
        }

        private static ControllerLoopSettings GetLoopSettings()
        {
            return new ControllerLoopSettings(
                10,         //radioLoopFrequency
                50,        //sensorLoopFrequency
                50,        //controlAlgorithmFrequency
                50,        //motorLoopFrequency
                10,          //telemetryLoopFrequency
                10000000);  //loopUnit
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

        private static GyroSettings GetGyroSettings()
        {
            return new GyroSettings(
                new Scale(0, 1/287.5F, 0),
                new RunningAverage(3)
                );
        }

        private static AccelerometerSettings GetAccelerometerSettings()
        {
            return new AccelerometerSettings(
                new Scale(0,1,0),
                new RunningAverage(3)
                );
        }
    }
}