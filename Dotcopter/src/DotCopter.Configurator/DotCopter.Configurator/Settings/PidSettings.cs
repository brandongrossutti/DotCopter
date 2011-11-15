using System;

namespace DotCopter.Configurator.Settings
{
    [Serializable]
    public class PidSettings
    {
        public Vector3D ProportionalGain { get; set; }
        public Vector3D IntegralGain { get; set; }
        public Vector3D DerivativeGain { get; set; }
        public Vector3D WindupLimit { get; set; }
        public string Strategy="No Strategy";

        public PidSettings(Vector3D proportionalGain, Vector3D integralGain, Vector3D derivativeGain, Vector3D windupLimit, string strategy)
        {
            ProportionalGain = proportionalGain;
            IntegralGain = integralGain;
            DerivativeGain = derivativeGain;
            WindupLimit = windupLimit;
            Strategy = strategy;
        }

        public PidSettings()
        {
            
        }
    }
}