using System;

namespace DotCopter.Configurator.Settings
{
    [Serializable]
    public class MotorSettings
    {
        public float SafeOutput;
        public float MinimumOutput;
        public float MaximumOutput;
        public string Motor1Pin;
        public string Motor2Pin;
        public string Motor3Pin;
        public string Motor4Pin;
        public string Motor5Pin;
        public string Motor6Pin;
        public string Motor7Pin;
        public string Motor8Pin;
        public string Name= "No Motor";


        public MotorSettings(float safeOutput, float minimumOutput, float maximumOutput, string motor1Pin, string motor2Pin, string motor3Pin, string motor4Pin, string motor5Pin, string motor6Pin, string motor7Pin, string motor8Pin, string name)
        {
            SafeOutput = safeOutput;
            MinimumOutput = minimumOutput;
            MaximumOutput = maximumOutput;
            Motor1Pin = motor1Pin;
            Motor2Pin = motor2Pin;
            Motor3Pin = motor3Pin;
            Motor4Pin = motor4Pin;
            Motor5Pin = motor5Pin;
            Motor6Pin = motor6Pin;
            Motor7Pin = motor7Pin;
            Motor8Pin = motor8Pin;
            Name = name;
        }

        public MotorSettings()
        {
            
        }

    }
}