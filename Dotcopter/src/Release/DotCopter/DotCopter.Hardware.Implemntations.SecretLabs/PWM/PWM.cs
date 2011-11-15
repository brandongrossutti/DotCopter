
using Microsoft.SPOT.Hardware;

namespace DotCopter.Hardware.Implementations.SecretLabs.PWM
{
    public class PWM : Hardware.PWM.PWM
    {
        private readonly global::SecretLabs.NETMF.Hardware.PWM _pwm;

        public PWM(Cpu.Pin pin, uint period) : base(period)
        {
            _pwm = new global::SecretLabs.NETMF.Hardware.PWM(pin);
        }

        public override void SetPulse(uint highTime)
        {
            _pwm.SetPulse(_period,highTime);
        }

    }
}
