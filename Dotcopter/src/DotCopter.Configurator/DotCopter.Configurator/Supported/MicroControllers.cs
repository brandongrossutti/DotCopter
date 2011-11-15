using System;
using System.Collections.Generic;

namespace DotCopter.Configurator.Supported
{
    [Serializable]
    public class MicroControllers 
    {
        public List<HardwareItem> Boards = new List<HardwareItem>();

        public MicroControllers(List<HardwareItem> boards)
        {
            Boards = boards;
        }

        public MicroControllers()
        {
            
        }
    }
}