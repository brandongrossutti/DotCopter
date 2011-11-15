using System;
using DotCopter.Avionics;
using DotCopter.Commons.Utilities;
using DotCopter.Hardware.Accelerometer;
using DotCopter.Hardware.Gyro;
using DotCopter.Hardware.Implementations.Bus;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;

namespace DotCopter.Hardware.Implementations.Accelerometer
{
    public class BMA180 : Hardware.Accelerometer.Accelerometer
    {
        private byte[] _buffer = new byte[6];
        private float _zeroPitch;
        private float _zeroRoll;
        private float _zeroYaw;
        private I2CBus _twiBus;
        private I2CDevice.Configuration _i2cConfiguration = new I2CDevice.Configuration(0x40, 400);
        private AccelerometerSettings _accelerometerSettings;

        public BMA180(I2CBus twiBus, AccelerometerSettings accelerometerSettings)
            : base(new AircraftPrincipalAxes())
        {
            _twiBus = twiBus;
            _accelerometerSettings = accelerometerSettings;
            Initialize();
        }

        private void Initialize()
        {
            byte[] buffer = new byte[]{0};
            _twiBus.Write(_i2cConfiguration, new byte[] {0xD1, 0x10 }, 100);            //enable writing to control registers
            _twiBus.ReadRegister(_i2cConfiguration, 0x20, buffer, 100);                 //get the value of the bandwidth and temperature compensation register
            buffer[0] &= 0x0F;
            _twiBus.WriteRegister(_i2cConfiguration, 0x20, buffer, 100);                //set the low pass filter to 10 Hz
            _twiBus.ReadRegister(_i2cConfiguration, 0x35, buffer, 100);                 //get the value of the full scale range register
            buffer[0] &= 0xF1;
            _twiBus.WriteRegister(_i2cConfiguration, 0x35, buffer, 100);                //set the full scale range to +/- 1g
        }

        public override void Update()
        {
            _twiBus.ReadRegister(_i2cConfiguration, 0x02, _buffer, 100);
            float pitch = BitConverter.ToShort(_buffer, 2) >> 2;
            float roll = BitConverter.ToShort(_buffer, 0) >> 2;
            float yaw = BitConverter.ToShort(_buffer, 4) >> 2;


            pitch = _accelerometerSettings.AccelerometerScale.Calculate(pitch) - _zeroPitch;
            roll = _accelerometerSettings.AccelerometerScale.Calculate(roll) - _zeroRoll;
            yaw = _accelerometerSettings.AccelerometerScale.Calculate(yaw) - _zeroYaw;

            Axes.Pitch = _accelerometerSettings.AccelerometerAverage.Average(pitch);
            Axes.Roll = _accelerometerSettings.AccelerometerAverage.Average(roll);
            Axes.Yaw = _accelerometerSettings.AccelerometerAverage.Average(yaw);
        }

        public override void Zero()
        {
            //Average the accelerometer for 50 readings to create zero
            float[] pitchData = new float[50];
            float[] rollData = new float[50];
            float[] yawData = new float[50];
            for (int i = 0; i < 50; i++)
            {
                _twiBus.ReadRegister(_i2cConfiguration, 0x02, _buffer, 100);
                pitchData[i] = _accelerometerSettings.AccelerometerScale.Calculate(BitConverter.ToShort(_buffer, 2) >> 2);
                rollData[i] = _accelerometerSettings.AccelerometerScale.Calculate(BitConverter.ToShort(_buffer, 0) >> 2);
                yawData[i] = _accelerometerSettings.AccelerometerScale.Calculate(BitConverter.ToShort(_buffer, 4) >> 2);
            }

            Sort.BubbleSort(ref pitchData);
            Sort.BubbleSort(ref rollData);
            Sort.BubbleSort(ref yawData);

            _zeroPitch = pitchData[25];
            _zeroRoll = rollData[25];
            _zeroYaw = yawData[25];
        }
    }
}
