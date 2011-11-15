using DotCopter.Elegance.Commands;

namespace DotCopter.Elegance.Avionics
{
    public abstract class PilotCommandProvider
    {
        public abstract void Update();
        public abstract PilotCommand GetCommand();
    }
}
