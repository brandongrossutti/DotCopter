using DotCopter.Elegance.Commands;
using DotCopter.Elegance.Physics;

namespace DotCopter.Elegance.Avionics
{
    public abstract class VectorTranslationalCommandProvider
    {
        public abstract VectorTranslationalCommand GetCommand(Pilot pilot, InertialMeasurement inertialMeasurement,
                                                              double time);
    }
}
