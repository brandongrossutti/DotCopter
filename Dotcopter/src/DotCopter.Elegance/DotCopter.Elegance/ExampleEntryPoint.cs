using DotCopter.Elegance.Avionics;
using DotCopter.Elegance.Controller;
using DotCopter.Elegance.Hardware;
using DotCopter.Elegance.Physics;

namespace DotCopter.Elegance
{
    public class ExampleEntryPoint
    {
        Controller.ControllerLoop loop = new ControllerLoop(new InertialMeasurement(new ExampleMagnetometer(),new ExampleGyro(), new ExampleAccelerometer()),new Pilot(new ExampleRadio()), new MotorMixer(),new PIDVectorTranslationalCommandProvider(),new LoopSettings() );
    }
}
