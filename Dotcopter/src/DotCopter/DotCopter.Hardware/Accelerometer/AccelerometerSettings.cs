using System;
using DotCopter.Commons.Utilities;
using Microsoft.SPOT;

namespace DotCopter.Hardware.Accelerometer
{
    public class AccelerometerSettings
    {
        public readonly Scale AccelerometerScale;
        public readonly RunningAverage AccelerometerAverage;
        public AccelerometerSettings(Scale accelerometerScale, RunningAverage accelerometerAverage)
        {
            AccelerometerScale = accelerometerScale;
            AccelerometerAverage = accelerometerAverage;
        }
    }
}
