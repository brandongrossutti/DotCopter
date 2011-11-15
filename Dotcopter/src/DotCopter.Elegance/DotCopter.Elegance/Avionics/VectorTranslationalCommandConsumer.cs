using DotCopter.Elegance.Commands;

namespace DotCopter.Elegance.Avionics
{
    public abstract class VectorTranslationalCommandConsumer
    {
        public abstract void ProcessCommand(VectorTranslationalCommand vectorTranslationalCommand);
    }
}
