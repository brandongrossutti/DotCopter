using System;
using System.Collections.Generic;

namespace DotCopter.Configurator.Supported
{
    [Serializable]
    public class PidAlgorithm {
        public readonly List<string> Strategies = new List<string>();

        public void AddAlgorithm(string strategy)
        {
            Strategies.Add(strategy);
        }

        public PidAlgorithm()
        {
            
        }
    }
}