using DotCopter.Commons.Utilities;
using DotCopter.ControlAlgorithms.PID;
using DotCopter.Physics;

namespace DotCopter.ControlAlgorithms.Implementations.PID
{
    public class PIDWindup : ControlAlgorithms.PID.PID
    {
        private Vector3D _lastActual = new Vector3D();
        private Vector3D _proportionalError = new Vector3D();
        private Vector3D _proportionalErrorOutput = new Vector3D();

        private Vector3D _integralErrorStep = new Vector3D();
        private Vector3D _integralError = new Vector3D();
        private Vector3D _integralErrorOutput = new Vector3D();

        private Vector3D _derivativeError = new Vector3D();
        private Vector3D _derivativeErrorOutput = new Vector3D();

        private Vector3D _negativeWindupLimit = new Vector3D();

        public PIDWindup(PIDSettings settings) : base(settings)
        {
            Vector3D.Multiply(settings.WindupLimit, -1F, _negativeWindupLimit);
        }

        public override void Update(Vector3D setpoint, Vector3D actual, float deltaTimeGain)
        {
            //Proportional Error
            Vector3D.Subtract(setpoint, actual, _proportionalError);
            Vector3D.Multiply(_proportionalError,Settings.ProportionalGain,_proportionalErrorOutput);
            
            //Intergral Error
            Vector3D.Multiply(_proportionalError, deltaTimeGain, _integralErrorStep);
            Vector3D.Add(_integralErrorStep, _integralError, _integralError);
            Vector3D.Constrain(_integralError, _negativeWindupLimit, Settings.WindupLimit);
            Vector3D.Multiply(_integralError, Settings.IntegralGain, _integralErrorOutput);

            //Derivative Error
            Vector3D.Subtract(actual, _lastActual, _derivativeError);
            Vector3D.Divide(_derivativeError, deltaTimeGain * 100, _derivativeError);
            Vector3D.Multiply(_derivativeError, Settings.DerivativeGain, _derivativeErrorOutput);

            //Store last actual
            Vector3D.Copy(actual, _lastActual);

            //Store Output
            Vector3D.Copy(_proportionalErrorOutput, Output);
            Vector3D.Add(Output,_integralErrorOutput, Output);
            Vector3D.Add(Output, _derivativeErrorOutput, Output);

        }

        public override void ZeroIntegral()
        {
            _integralError.X = 0;
            _integralError.Y = 0;
            _integralError.Z = 0;
        }
    }
}
