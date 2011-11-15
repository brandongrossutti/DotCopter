using DotCopter.ControlAlgorithms.Mixing;
using DotCopter.Hardware.Motor;
using DotCopter.Physics;
using Microsoft.SPOT;

namespace DotCopter.ControlAlgorithms.Implementations.Mixing
{
    public class QuadMixer : MotorMixer
    {
        private readonly Motor _frontMotor;
        private readonly Motor _rearMotor;
        private readonly Motor _leftMotor;
        private readonly Motor _rightMotor;

        public QuadMixer(Motor frontMotor, Motor rearMotor, Motor leftMotor, Motor rightMotor)
        {
            _frontMotor = frontMotor;
            _rearMotor = rearMotor;
            _leftMotor = leftMotor;
            _rightMotor = rightMotor;
            SetSafe();
        }

        public override sealed void SetSafe()
        {
            _frontMotor.SetSafe();
            _rearMotor.SetSafe();
            _leftMotor.SetSafe();
            _rightMotor.SetSafe();
        }

        public override void Update(float throttle, Vector3D output)
        {
            _frontMotor.Update(throttle - output.X + output.Z);
            _rearMotor.Update(throttle + output.X + output.Z);
            _leftMotor.Update(throttle - output.Y - output.Z);
            _rightMotor.Update(throttle + output.Y - output.Z);
            Debug.Print(
                "F: " + (throttle - output.X + output.Z) + 
                ", Rr: " + (throttle + output.X + output.Z) +  
                ", L: " + (throttle - output.Y - output.Z) +  
                ", R: " + (throttle + output.Y - output.Z));
        }
    }
}
