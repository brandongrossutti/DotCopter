using System;
using System.Collections.Generic;

namespace DotCopter.Configurator.Supported
{
    [Serializable]
    public class Radio {
        public List<HardwareItem> Radios = new List<HardwareItem>();

        public void AddRadio(HardwareItem hardwareItem)
        {
            Radios.Add(hardwareItem);
        }

        public Radio()
        {
            
        }
    }
}