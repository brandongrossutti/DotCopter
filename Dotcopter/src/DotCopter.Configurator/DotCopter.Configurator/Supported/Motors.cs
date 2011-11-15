using System;
using System.Collections.Generic;

namespace DotCopter.Configurator.Supported
{
    [Serializable]
    public class Motors
    {
        public List<HardwareItem> AllMotors= new List<HardwareItem>();

        public Motors(List<HardwareItem> allMotors)
        {
            AllMotors = allMotors;
        }

        public Motors()
        {
            
        }

        public void AddMotors(HardwareItem item)
        {
            AllMotors.Add(item);
        }
    }
}