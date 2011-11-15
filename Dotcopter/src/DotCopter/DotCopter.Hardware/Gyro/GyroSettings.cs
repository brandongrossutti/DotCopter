using DotCopter.Commons.Utilities;

namespace DotCopter.Hardware.Gyro
{
    public class GyroSettings
    {
        public readonly Scale GyroScale;
        public readonly RunningAverage GyroAverage;
        public GyroSettings(Scale gyroScale, RunningAverage gyroAverage)
        {
            GyroScale = gyroScale;
            GyroAverage = gyroAverage;
        }
    }
}
