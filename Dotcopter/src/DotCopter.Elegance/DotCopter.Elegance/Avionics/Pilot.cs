using System;
using DotCopter.Elegance.Commands;

namespace DotCopter.Elegance.Avionics
{
    public class Pilot
    {
        private readonly PilotCommandProvider _pilotCommandProvider;

        public Pilot(PilotCommandProvider pilotCommandProvider)
        {
            _pilotCommandProvider = pilotCommandProvider;
        }

        public PilotCommand GetCommand()
        {
            return _pilotCommandProvider.GetCommand();
        }

        public void Update()
        {
            _pilotCommandProvider.Update();
        }
    }
}
