using System;

namespace DotCopter.Configurator.Supported
{
    [Serializable]
    public class HardwareItem 
    {
        public string Name;

        public HardwareItem(string name)
        {
            Name = name;
        }

        public HardwareItem()
        {
            
        }
    }
}