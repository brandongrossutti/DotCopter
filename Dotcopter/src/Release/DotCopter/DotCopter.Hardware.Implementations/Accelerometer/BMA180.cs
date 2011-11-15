using System.Threading;
using DotCopter.Commons.Utilities;
using DotCopter.Hardware.Implementations.Bus;
using DotCopter.Physics;
using Microsoft.SPOT.Hardware;

namespace DotCopter.Hardware.Implementations.Accelerometer
{
    public class BMA180 : Hardware.Accelerometer.Accelerometer
    {
        private byte[] _buffer = new byte[6];
        private readonly Vector3D _rawAcceleration = new Vector3D();
        private readonly Vector3D _zeroAcceleration = new Vector3D();
        private readonly I2CBus _twiBus;
        private readonly I2CDevice.Configuration _twiConfiguration = new I2CDevice.Configuration(0x40, 400);
        private Scale _scale = new Scale(0, 1/0.25F, 0);//Must be set to match range selection

        public BMA180(I2CBus twiBus): base(new Vector3D())
        {
            _twiBus = twiBus;
            Initialize();
        }

        private void Initialize()
        {
            byte[] buffer = new byte[]{0};
            _twiBus.Write(_twiConfiguration, new byte[] {0x10, 0xB6}, 100);             //issue a reset
            Thread.Sleep(10);
            _twiBus.Write(_twiConfiguration, new byte[] {0x0D, 0x10}, 100);             //enable writing to control registers
            _twiBus.ReadRegister(_twiConfiguration, 0x20, buffer, 100);                 //get the value of the bandwidth and temperature compensation register
            buffer[0] &= 0x0F;
            _twiBus.WriteRegister(_twiConfiguration, 0x20, buffer, 100);                //set the low pass filter to 10 Hz
            _twiBus.ReadRegister(_twiConfiguration, 0x35, buffer, 100);                 //get the value of the full scale range register
            buffer[0] &= 0xF5;
            _twiBus.WriteRegister(_twiConfiguration, 0x35, buffer, 100);                //set the full scale range to +/- 2g  .25mg /LSB ensure scale matches
        }

        public override void Update()
        {

            UpdateRaw();
            Vector3D.Subtract(_rawAcceleration, _zeroAcceleration, Acceleration);
            Acceleration.ApplyScale(_scale);
        }

        public override void Zero()
        {
            const int requiredReadings = 50;
            float[] xData = new float[requiredReadings];
            float[] yData = new float[requiredReadings];
            float[] zData = new float[requiredReadings];
            for (int i = 0; i < requiredReadings; i++)
            {
                _twiBus.ReadRegister(_twiConfiguration, 0x02, _buffer, 100);
                xData[i] = _rawAcceleration.X;
                yData[i] = _rawAcceleration.Y;
                zData[i] = _rawAcceleration.Z;
            }

            Sort.BubbleSort(ref xData);
            Sort.BubbleSort(ref yData);
            Sort.BubbleSort(ref zData);

            _zeroAcceleration.X = xData[requiredReadings/2];
            _zeroAcceleration.Y = yData[requiredReadings/2];
            _zeroAcceleration.Z = zData[requiredReadings/2];
        }

        private void UpdateRaw()
        {
            _twiBus.ReadRegister(_twiConfiguration, 0x02, _buffer, 100);
            _rawAcceleration.X = ((short) (_buffer[0] << 8) | _buffer[1]) >> 2;
            _rawAcceleration.Y = ((short) (_buffer[2] << 8) | _buffer[3]) >> 2;
            _rawAcceleration.Z = ((short) (_buffer[4] << 8) | _buffer[5]) >> 2;
        }
    }
}
