using System.Threading;
using DotCopter.Commons.Utilities;
using DotCopter.Hardware.Implementations.Bus;
using DotCopter.Physics;
using Microsoft.SPOT.Hardware;

namespace DotCopter.Hardware.Implementations.Gyro
{

    public class ITG3200 : Hardware.Gyro.Gyro
    {
        private readonly byte[] _buffer = new byte[6];
        private readonly Vector3D _rawAngularVelocity = new Vector3D();
        private readonly Vector3D _zeroAngularVelocity = new Vector3D();
        private readonly I2CBus _twiBus;
        private readonly I2CDevice.Configuration _twiConfiguration = new I2CDevice.Configuration(0x69,400);
        private readonly Scale _scale = new Scale(0, 1/14.375F, 0);

        public ITG3200(I2CBus twiBus) : base(new Vector3D())
        {
            _twiBus = twiBus;
            Initialize();
        }

        private void Initialize()
        {
            //Initialize the gyro
            _twiBus.Write(_twiConfiguration, new byte[] { 0x3E, 0x80 }, 100); // Reset
            _twiBus.Write(_twiConfiguration, new byte[] { 0x16, 0x1D }, 100); // Set Full Scale, Band Pass Filter
            _twiBus.Write(_twiConfiguration, new byte[] { 0x3E, 0x03 }, 100); // Oscillator
            Thread.Sleep(2000);
        }

        public override void Update()
        {
            UpdateRaw();
            Vector3D.Subtract(_rawAngularVelocity,_zeroAngularVelocity,AngularVelocity);
            AngularVelocity.ApplyScale(_scale);
        }

        public override void Zero()
        {
            const int requiredReadings = 50;

            float[] xData = new float[requiredReadings];
            float[] yData = new float[requiredReadings];
            float[] zData = new float[requiredReadings];
            for (int i = 0; i < requiredReadings; i++)
            {
                UpdateRaw();
                xData[i] = _rawAngularVelocity.X;
                yData[i] = _rawAngularVelocity.Y;
                zData[i] = _rawAngularVelocity.Z;
                Thread.Sleep(10);
            }

            Sort.BubbleSort(ref xData);
            Sort.BubbleSort(ref yData);
            Sort.BubbleSort(ref zData);

            _zeroAngularVelocity.X = xData[requiredReadings/2];
            _zeroAngularVelocity.Y = yData[requiredReadings/2];
            _zeroAngularVelocity.Z = zData[requiredReadings/2];
        }

        private void UpdateRaw()
        {
            _twiBus.ReadRegister(_twiConfiguration, 0x1D, _buffer, 100);
            _rawAngularVelocity.X = (short)(_buffer[0] << 8) | _buffer[1];
            _rawAngularVelocity.Y = (short)(_buffer[2] << 8) | _buffer[3];
            _rawAngularVelocity.Z = (short)(_buffer[4] << 8) | _buffer[5];
        }
    }
}
