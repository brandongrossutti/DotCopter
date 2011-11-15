using DotCopter.Commons.Utilities;

namespace DotCopter.Hardware.Radio
{
    public class RadioSettings
    {
        public readonly float RadioSensitivityFactor;
        public readonly Scale AxesScale;
        public readonly Scale ThrottleScale;

        public RadioSettings(Scale throttleScale, Scale axesScale, float radioSensitivityFactor)
        {
            ThrottleScale = throttleScale;
            AxesScale = axesScale;
            RadioSensitivityFactor = radioSensitivityFactor;
        }
    }
}
