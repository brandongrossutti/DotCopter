using System;
using System.Collections.Generic;

namespace DotCopter.Configurator.Supported
{
    [Serializable]
    public class Sensors {
        public readonly List<HardwareItem> Multifunctional = new List<HardwareItem>();
        public readonly List<HardwareItem> Gyros = new List<HardwareItem>();
        public readonly List<HardwareItem> Accelleromters = new List<HardwareItem>();

        public Sensors()
        {
            
        }

        public void AddMultiFunctional(HardwareItem item)
        {
            Multifunctional.Add(item);
        }

        public void AddGyro(HardwareItem gyro)
        {
            Gyros.Add(gyro);
        }

        public void AddAccellerometer(HardwareItem bma180)
        {
            Accelleromters.Add(bma180);
        }
    }
}