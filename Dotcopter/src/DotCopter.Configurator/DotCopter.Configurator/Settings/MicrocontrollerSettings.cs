using System;

namespace DotCopter.Configurator.Settings
{
    [Serializable]
    public class MicrocontrollerSettings
    {
        public string ControllerType= " No Microcontroller";

        public MicrocontrollerSettings(string  controllerType)
        {
            ControllerType = controllerType;
        }

        public MicrocontrollerSettings()
        {
            
        }
    }
}