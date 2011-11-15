using DotCopter.Commons.Utilities;
using DotCopter.Hardware.Implementations.Bus;
using DotCopter.Hardware.Radio;
using DotCopter.Physics;
using Microsoft.SPOT.Hardware;

namespace DotCopter.Hardware.Implementations.Radio
{
    public class DotCopterRadio : Hardware.Radio.Radio
    {
        private readonly RadioSettings _settings;
        private readonly byte[] _buffer = new byte[10];
        private readonly I2CBus _twiBus;
        private readonly I2CDevice.Configuration _i2cConfiguration = new I2CDevice.Configuration(0x42, 400);

        public DotCopterRadio(I2CBus twiBus, RadioSettings settings) : 
            base(0,new Vector3D(),false)
        {
            _twiBus = twiBus;
            _settings = settings;
        }

        public override void Update()
        {
            _twiBus.Read(_i2cConfiguration, _buffer, 100);

            float throttle = BitConverter.ToShort(_buffer, 0);
            float x = BitConverter.ToShort(_buffer, 2);
            float y = BitConverter.ToShort(_buffer, 4);
            float z = BitConverter.ToShort(_buffer, 6);

            Throttle = _settings.ThrottleScale.Calculate(throttle);
            AngularVelocity.X = _settings.AxesScale.Calculate(y) * _settings.RadioSensitivityFactor;
            AngularVelocity.Y = _settings.AxesScale.Calculate(x) * _settings.RadioSensitivityFactor;
            AngularVelocity.Z = _settings.AxesScale.Calculate(z) * _settings.RadioSensitivityFactor;
            Gear = BitConverter.ToShort(_buffer, 8) > 1500;
        }
        
    }

    
}
