using System;

namespace DotCopter.Configurator.Settings
{
    [Serializable]
    public class RadioSettings
    {
        private float _sensitivityFactor;
        public string Name = "No Radio";
        public RadioSettings(float sensitivityFactor, string name)
        {
            _sensitivityFactor = sensitivityFactor;
            Name = name;
        }

        public RadioSettings() {}

        public float SensitivityFactor
        {
            get { return _sensitivityFactor; }
            set { _sensitivityFactor = value; }
        }

        
    }
}

