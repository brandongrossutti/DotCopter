using System;
using System.Collections.Generic;

namespace DotCopter.Configurator.Settings
{
    [Serializable]
    public class SensorSettings 
    {
        public string Gyro = "No Gyro Sensor";
        public string MultiFunctional = "No MultiFunctional Sensor";
        public string Accellerometer = "No Accellerometer Sensor";

        public void AddGyro(string gyro)
        {
            Gyro = gyro;
            MultiFunctional = "No MultiFunctional Sensor";
        }

        public void AddMultiFunctional(string item)
        {
            MultiFunctional = item;
            Gyro = "No Gyro Sensor";
            Accellerometer = "No Accellerometer Sensor";
        }

        public void AddAccellerometer(string item)
        {
            Accellerometer = item;
            MultiFunctional = "No MultiFunctional Sensor";
        }
    }
}