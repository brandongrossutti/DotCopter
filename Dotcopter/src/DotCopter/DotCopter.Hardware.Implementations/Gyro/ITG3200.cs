using System.Threading;
using DotCopter.Avionics;
using DotCopter.Commons.Utilities;
using DotCopter.Hardware.Gyro;
using DotCopter.Hardware.Implementations.Bus;
using Microsoft.SPOT.Hardware;

namespace DotCopter.Hardware.Implementations.Gyro
{
    /// <summary>
    /// Axis scale is +-28,750
    /// </summary>
    public class ITG3200 : Hardware.Gyro.Gyro
    {
        private byte[] _buffer = new byte[6];
        private float _zeroPitch;
        private float _zeroRoll;
        private float _zeroYaw;
        private I2CBus _twiBus;
        private I2CDevice.Configuration _i2cConfiguration = new I2CDevice.Configuration(0x69,400);
        private GyroSettings _gyroSettings;

        public ITG3200(I2CBus twiBus, GyroSettings gyroSettings) : base(new AircraftPrincipalAxes())
        {
            _twiBus = twiBus;
            _gyroSettings = gyroSettings;
            Initialize();
        }

        private void Initialize()
        {
            //Initialize the gyro
            _twiBus.Write(_i2cConfiguration, new byte[] { 0x3E, 0x80 }, 100); // Reset
            _twiBus.Write(_i2cConfiguration, new byte[] { 0x15, 0x00 }, 100); // Sample Rate Divider
            _twiBus.Write(_i2cConfiguration, new byte[] { 0x16, 0x18 }, 100); // Full Scale
            _twiBus.Write(_i2cConfiguration, new byte[] { 0x16, 0x06 }, 100); // Band Pass Filter
            _twiBus.Write(_i2cConfiguration, new byte[] { 0x3E, 0x03 }, 100); // Oscillator
            _twiBus.Write(_i2cConfiguration, new byte[] { 0x17, 0x05 }, 100); // Enable Send Raw Values
            Thread.Sleep(500);
        }

        public override void Update()
        {
            _twiBus.ReadRegister(_i2cConfiguration, 0x1D, _buffer, 100);
            float pitch = _gyroSettings.GyroScale.Calculate((short)(_buffer[0] << 8) | _buffer[1]);
            float roll = _gyroSettings.GyroScale.Calculate((short)(_buffer[2] << 8) | _buffer[3]);
            float yaw = _gyroSettings.GyroScale.Calculate((short)(_buffer[4] << 8) | _buffer[5]);

            Axes.Pitch = pitch - _zeroPitch;
            Axes.Roll = roll - _zeroRoll;
            Axes.Yaw = yaw - _zeroYaw;
        }

        public override void Zero()
        {
            //Average the gyro for 50 readings to create zero
            float[] pitchData = new float[50];
            float[] rollData = new float[50];
            float[] yawData = new float[50];
            for (int i = 0; i < 50; i++)
            {
                _twiBus.ReadRegister(_i2cConfiguration, 0x1D, _buffer, 100);
                pitchData[i] = _gyroSettings.GyroScale.Calculate((short)(_buffer[0] << 8) | _buffer[1]);
                rollData[i] = _gyroSettings.GyroScale.Calculate((short)(_buffer[2] << 8) | _buffer[3]);
                yawData[i] = _gyroSettings.GyroScale.Calculate((short)(_buffer[4] << 8) | _buffer[5]);
                Thread.Sleep(10);
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
