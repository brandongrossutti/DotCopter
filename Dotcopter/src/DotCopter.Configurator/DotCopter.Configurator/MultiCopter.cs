using System;
using System.Configuration;

using DotCopter.Configurator.Settings;

namespace DotCopter.Configurator
{
    [Serializable]
    public class MultiCopter
    {
        public string MulticopterType;
        public MotorSettings MotorSettings;
        public ControllerLoopSettings ControllerLoopSettings;
        public SensorSettings SensorSettings;
        public PidSettings PidSettings;
        public MicrocontrollerSettings MicrocontrollerSettings;
        public RadioSettings RadioSettings;

        public MultiCopter(string multicopterType, MotorSettings motorSettings, ControllerLoopSettings controllerLoopSettings, SensorSettings sensorSettings, PidSettings pidSettings, MicrocontrollerSettings microcontrollerSettings, RadioSettings radioSettings)
        {
            MulticopterType = multicopterType;
            MotorSettings = motorSettings;
            ControllerLoopSettings = controllerLoopSettings;
            SensorSettings = sensorSettings;
            PidSettings = pidSettings;
            MicrocontrollerSettings = microcontrollerSettings;
            RadioSettings = radioSettings;
        }

        public MultiCopter()
        {
            MotorSettings = new MotorSettings();
            ControllerLoopSettings = new ControllerLoopSettings();
            SensorSettings = new SensorSettings();
            PidSettings = new PidSettings();
            MicrocontrollerSettings = new MicrocontrollerSettings();
            RadioSettings = new RadioSettings();
        }


    }
}
